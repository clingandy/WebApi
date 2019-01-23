/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 16:50:40
** desc：    NpoiExcelHelper类
** Ver.:     V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Infrastructure.Common
{
    public class NpoiExcelHelper
    {
        #region 导出数据到Excel

        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static void TableToExcel(DataTable dt, string file)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else { workbook = null; }
            if (workbook == null) { return; }
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// 保存成Excel - 公共
        /// </summary>
        /// <param name="arrTitle"></param>
        /// <param name="arrField">导出的字段，和标题数量和顺序一致</param>
        /// <param name="list"></param>
        /// <param name="fileName">文件名</param>
        /// <returns>返回不带域名的下载路径</returns>
        public static string TableToExcel<T>(string[] arrTitle, string[] arrField, IList<T> list, string fileName) where T : class, new()
        {
            if (list == null || list.Count == 0) return string.Empty;
            fileName = fileName + ".xlsx";
            var folder = "\\Temp\\Export\\";
            var dir = AppDomain.CurrentDomain.BaseDirectory + folder;
            var file = dir + fileName;
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Sheet1");

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < arrTitle.Length; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(arrTitle[i]);
            }
            //数据
            var type = typeof(T);
            var dic = type.GetProperties().ToDictionary(t => t.Name, t => t);
            for (int i = 0; i < list.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < arrField.Length; j++)
                {
                    if (!dic.ContainsKey(arrField[j])) continue;
                    ICell cell = row1.CreateCell(j);
                    var field = dic[arrField[j]];
                    switch (field.PropertyType.Name)
                    {
                        case "Int32":
                        case "Int64":
                            cell.SetCellValue(long.Parse(dic[arrField[j]].GetValue(list[i]).ToString()));
                            break;
                        case "DateTime":
                            cell.SetCellValue(DateTime.Parse(dic[arrField[j]].GetValue(list[i]).ToString()).ToString("yyyy-MM-dd HH:mm;ss"));
                            break;
                        default:
                            cell.SetCellValue(dic[arrField[j]].GetValue(list[i]).ToString());
                            break;
                    }
                }
            }
            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }

            return (folder + fileName).Replace("\\","/");
        }

        #endregion

        #region Excel导入Table

        /// <summary>
        /// Excel导入成Datable
        /// </summary>
        /// <param name="file">导入路径(包含文件名与扩展名)</param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file)
        {
            var dt = new DataTable();
            var fileExt = Path.GetExtension(file).ToLower();
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                IWorkbook workbook = fileExt == ".xlsx" ? new XSSFWorkbook(fs) : null;
                if (workbook == null) { return null; }
                var sheet = workbook.GetSheetAt(0);

                //表头  
                var header = sheet.GetRow(sheet.FirstRowNum);
                var columns = new List<int>();
                for (var i = 0; i < header.LastCellNum; i++)
                {
                    var obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据  
                for (var i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    var dr = dt.NewRow();
                    var hasValue = false;
                    foreach (var j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        #endregion

        #region 单元格操作

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue; 
                default:
                    return "=" + cell.CellFormula;
            }
        }

        #endregion

        #region Table转Entity

        /// <summary>
        /// 将DataTable转为实体对象
        /// T需要加特性TabaleColNameAttribute
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="sourceDt">原DT</param>
        /// <returns>转换后的实体列表</returns>
        public static List<T> GetEntityFromDataTable<T>(DataTable sourceDt) where T : class
        {
            List<T> list = new List<T>();
            // 获取需要转换的目标类型
            Type type = typeof(T);

            foreach (DataRow dRow in sourceDt.Rows)
            {
                // 实体化目标类型对象
                object obj = Activator.CreateInstance(type);
                try
                {
                    foreach (var prop in type.GetProperties())
                    {
                        var attribute = (TabaleColNameAttribute)Attribute.GetCustomAttribute(prop, typeof(TabaleColNameAttribute));
                        if (attribute == null) continue;
                        // 给目标类型对象的各个属性值赋值
                        var value = dRow[attribute.TabaleColName];
                        if (value == null) continue;
                        prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType, null), null);
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
                list.Add(obj as T);
            }
            return list;
        }

        #endregion
    }
}
