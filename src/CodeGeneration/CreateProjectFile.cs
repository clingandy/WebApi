/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/2/14 18:02:44
** desc：    CreateProjectFile类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeGeneration
{
    public static class CreateProjectFile
    {
        #region 属性

        private static string _saveFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Today.ToString("yyyyMMdd"));

        #endregion

        #region 工具方法

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strings"></param>
        private static void WriteFile(string path, string strings)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream f = File.Create(path);
            f.Close();
            f.Dispose();
            StreamWriter w = new StreamWriter(path, true, Encoding.UTF8);
            w.WriteLine(strings);
            w.Close();
            w.Dispose();
        }

        /// <summary>
        /// 转化大驼峰格式
        /// 首字母大写，替换的字符后一个改大写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parttern"></param>
        /// <returns></returns>
        private static string ConvertUpper(this string str, string parttern = "_")
        {
            if (str == null || str.Length < 2)
            {
                return str;
            }
            str = Regex.Replace(str, @"("+ parttern + ")([a-zA-Z0-9])", r => r.Value.Replace("_","").ToUpper());
            return str.Substring(0,1).ToUpper() + str.Substring(1, str.Length -1);
        }

        /// <summary>
        /// mysql类型转换
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static string ConvertType(this string dbType)
        {
            switch (dbType)
            {
                case "tinyint":
                case "smallint":
                case "mediumint":
                case "int":
                case "integer":
                    return "int";
                case "double":
                    return "double";
                case "float":
                    return "float";
                case "decimal":
                    return "decimal";
                case "numeric":
                case "real":
                    return "decimal";
                case "bit":
                    return "bool";
                case "date":
                case "time":
                case "year":
                case "datetime":
                case "timestamp":
                    return "DateTime";
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblog":
                case "binary":
                case "varbinary":
                    return "byte[]";
                case "char":
                case "varchar":
                case "tinytext":
                case "text":
                case "mediumtext":
                case "longtext":
                    return "string";
                case "point":
                case "linestring":
                case "polygon":
                case "geometry":
                case "multipoint":
                case "multilinestring":
                case "multipolygon":
                case "geometrycollection":
                case "enum":
                case "set":
                    return dbType;
                default:
                    return dbType;
            }
        }

        #endregion

        #region 设置保存路径

        /// <summary>
        /// 默认 根目录+时间
        /// </summary>
        /// <param name="filePath"></param>
        public static void SetSaveFile(string filePath)
        {
            _saveFile = filePath;
        }

        #endregion

        #region 生成Model

        /// <summary>
        /// 生成Model项目
        /// </summary>
        /// <param name="dicTable"></param>
        /// <param name="tableSchema"></param>
        /// <returns></returns>
        public static void ToModel(Dictionary<string, List<ColumnEntity>> dicTable, string tableSchema)
        {
            tableSchema = tableSchema.ConvertUpper();
            foreach (var table in dicTable)
            {
                var columnList = table.Value;

                if (columnList == null || columnList.Count == 0) return;
                var first = columnList[0];

                var stringBuilder = new StringBuilder();
                stringBuilder.Append("using System;\n");
                stringBuilder.Append("using SqlSugar;\n");
                stringBuilder.Append("\n");
                stringBuilder.AppendFormat("namespace {0}.Model.Entity\n", tableSchema);
                stringBuilder.Append("{\n");

                stringBuilder.AppendFormat("    [SugarTable(\"{0}\")]\n", first.TableName);
                stringBuilder.AppendFormat("    public class {0}Entity\n", first.TableName.ConvertUpper());
                stringBuilder.Append("    {\n");
                foreach (var current in columnList)
                {
                    stringBuilder.Append("        /// <summary>\n");
                    stringBuilder.AppendFormat("        /// Desc:{0}\n", current.ColumnComment);
                    stringBuilder.AppendFormat("        /// Default:{0}\n", current.Default);
                    stringBuilder.AppendFormat("        /// Nullable:{0}\n", current.Nullable);
                    stringBuilder.Append("        /// </summary>\n");
                    stringBuilder.AppendFormat("        [SugarColumn({0}{1}ColumnName = \"{2}\")]\n", current.Key == "PRI"? "IsPrimaryKey = true, " : "", current.Extra=="auto_increment"? "IsIdentity = true, " : "", current.ColumnName);
                    var dataType = current.DataType.ConvertType();
                    stringBuilder.AppendFormat("        public {0}{1} {2} {3}\n", dataType, dataType!="string"&& current.Nullable.ToLower()=="yes" ? "?":"", current.ColumnName.ConvertUpper(), "{ get; set; }");
                    stringBuilder.Append("\n");
                }
                stringBuilder.Append("    }\n");
                stringBuilder.Append("}");
                var text = Path.Combine(_saveFile, tableSchema + ".Model", "Entity");
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, first.TableName.ConvertUpper() + "Entity.cs");
                WriteFile(text, stringBuilder.ToString());

                //查询View实体
                var viewPath = Path.Combine(_saveFile, tableSchema + ".Model", "View");
                if (!Directory.Exists(viewPath))
                {
                    Directory.CreateDirectory(viewPath);
                }
                var stringBuilderView = new StringBuilder(); 
                stringBuilderView.Append("using System;\n");
                stringBuilderView.Append("using System.Collections.Generic;\n");
                stringBuilderView.Append("\n");
                stringBuilderView.AppendFormat("namespace {0}.Model.View\n", first.TableSchema.ConvertUpper());
                stringBuilderView.Append("{\n");
                stringBuilderView.AppendFormat("    public class {0}View\n", first.TableName.ConvertUpper());
                stringBuilderView.Append("    {\n");
                foreach (var current in columnList)
                {
                    var dataType = current.DataType.ConvertType();
                    //添加string和datetime类型的字段
                    if (dataType != "string" && dataType != "DateTime") continue;
                    if (dataType == "DateTime")
                    {
                        dataType = "List<DateTime>";
                    }
                    stringBuilderView.Append("        /// <summary>\n");
                    stringBuilderView.AppendFormat("        /// {0}\n", current.ColumnComment);
                    stringBuilderView.Append("        /// </summary>\n");
                    stringBuilderView.AppendFormat("        public {0} {1} {2}\n", dataType, current.ColumnName.ConvertUpper(), "{ get; set; }");
                    stringBuilderView.Append("\n");
                }
                stringBuilderView.Append("    }\n");
                stringBuilderView.Append("}");
                viewPath = Path.Combine(viewPath, first.TableName.ConvertUpper() + "View.cs");
                WriteFile(viewPath, stringBuilderView.ToString());

            }

            //其他目录
            var enumPath = Path.Combine(_saveFile, tableSchema + ".Model", "Enum");
            if (!Directory.Exists(enumPath))
            {
                Directory.CreateDirectory(enumPath);
            }
            var searchPath = Path.Combine(_saveFile, tableSchema + ".Model", "Search");
            if (!Directory.Exists(searchPath))
            {
                Directory.CreateDirectory(searchPath);
            }
        }

        #endregion

        #region 生成Service

        /// <summary>
        /// 生成Service项目
        /// </summary>
        /// <param name="tableList"></param>
        /// <param name="dicTableColumn"></param>
        /// <param name="dicArea"></param>
        /// <param name="tableSchemaName"></param>
        public static void ToService(
            Dictionary<string, string> tableList, 
            Dictionary<string, List<ColumnEntity>> dicTableColumn,
            Dictionary<string, List<string>> dicArea,
            string tableSchemaName)
        {
            if (tableList == null || tableList.Count == 0) return;

            tableSchemaName = tableSchemaName.ConvertUpper();
            var dicUrl = Path.Combine(_saveFile, tableSchemaName + ".Service");
            if (!Directory.Exists(dicUrl))
            {
                Directory.CreateDirectory(dicUrl);
            }

            // 整体结构
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Service", "Service.txt");
            var stringBuilder = new StringBuilder();
            using (var filestream = new StreamReader(filePath, Encoding.UTF8))
            {
                string temp;
                while ((temp = filestream.ReadLine()) != null)
                {
                    stringBuilder.Append(temp + "\n");
                }
            }

            // 子结构
            var fileChildPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Service", "ServiceChild.txt");
            var stringChildBuilder = new StringBuilder();
            using (var filestream = new StreamReader(fileChildPath, Encoding.UTF8))
            {
                string temp;
                while ((temp = filestream.ReadLine()) != null)
                {
                    stringChildBuilder.Append(temp + "\n");
                }
            }

            //记录合并表的 子结构
            var dicAreaStr = new Dictionary<string, string>();

            foreach (var table in tableList)
            {
                var columnList = dicTableColumn.FirstOrDefault(t => t.Key == table.Key).Value;    //列信息
                var dateTimeBuilder = new StringBuilder();  // 时间转换
                var searchBuilder = new StringBuilder();    // 查询条件
                foreach (var current in columnList)
                {
                    var cName = current.ColumnName.ConvertUpper();
                    var dataType = current.DataType.ConvertType();
                    switch (dataType)
                    {
                        case "string":
                            searchBuilder.AppendFormat("                .WhereIF(!search.{0}.IsNullOrWhiteSpace(), t1 => t1.{0}.Contains(search.{0}))\n", cName);
                            break;
                        case "DateTime":
                            dateTimeBuilder.AppendFormat("            if (search.{0}?.Count > 0) search.{0}[0] = search.{0}[0].Date;\n", cName);
                            dateTimeBuilder.AppendFormat("            if (search.{0}?.Count > 1) search.{0}[1] = search.{0}[1].Date.AddDays(1).Date;\n", cName);
                            searchBuilder.AppendFormat("                .WhereIF(search.{0}?.Count > 0, t1 => t1.{0} >= search.{0}[0])\n", cName);
                            searchBuilder.AppendFormat("                .WhereIF(search.{0}?.Count > 1, t1 => t1.{0} < search.{0}[1])\n", cName);
                            break;
                    }

                }
                dateTimeBuilder.Append("            ");
                searchBuilder.Append("                ");

                var tn = table.Key.ConvertUpper();
                var area = dicArea.FirstOrDefault(t => t.Value.Contains(table.Key));
                if (string.IsNullOrEmpty(area.Key))
                {
                    // 单独生成一个文件
                    var text = Path.Combine(dicUrl, $"{tn}Service.cs");
                    var temp = stringBuilder.ToString()
                        .Replace("{child}", stringChildBuilder.ToString())
                        .Replace("{dt}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                        .Replace("{tableName}", tn)
                        .Replace("{namespace}", tableSchemaName)
                        .Replace("{desc}", string.IsNullOrWhiteSpace(table.Value) ? tn : table.Value)
                        .Replace("{searchConvert}", dateTimeBuilder.ToString())
                        .Replace("{search}", searchBuilder.ToString());
                    WriteFile(text, temp);
                }
                else
                {
                    // 记录合并的子项字符
                    var temp = stringChildBuilder.ToString()
                        .Replace("{tableName}", tn)
                        .Replace("{desc}", string.IsNullOrWhiteSpace(table.Value) ? tn : table.Value)
                        .Replace("{searchConvert}", dateTimeBuilder.ToString())
                        .Replace("{search}", searchBuilder.ToString());
                    if (dicAreaStr.ContainsKey(area.Key))
                    {
                        dicAreaStr[area.Key] = $"{dicAreaStr[area.Key]}\n{temp}";
                    }
                    else
                    {
                        dicAreaStr[area.Key] = temp;
                    }
                }
            }

            // 生成合并table后的文件
            foreach (var dic in dicAreaStr)
            {
                var key = dic.Key.ConvertUpper();
                var text = Path.Combine(dicUrl, $"{key}Service.cs");
                var temp = stringBuilder.ToString()
                    .Replace("{child}", dic.Value)
                    .Replace("{dt}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                    .Replace("{tableName}", key)
                    .Replace("{namespace}", tableSchemaName);
                WriteFile(text, temp);
            }

            // Service固定模板
            ToServiceBaseFile(dicUrl, tableSchemaName);
        }

        /// <summary>
        /// Service固定模板
        /// </summary>
        /// <param name="dicUrl"></param>
        /// <param name="tableSchemaName"></param>
        private static void ToServiceBaseFile(string dicUrl, string tableSchemaName)
        {
            tableSchemaName = tableSchemaName.ConvertUpper();

            dicUrl = Path.Combine(dicUrl, "Base");
            if (!Directory.Exists(dicUrl))
            {
                Directory.CreateDirectory(dicUrl);
            }

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Service", "Config.txt");
            using (var filestream = new StreamReader(filePath, Encoding.UTF8))
            {
                string temp;
                var stringBuilder = new StringBuilder();
                while ((temp = filestream.ReadLine()) != null)
                {
                    stringBuilder.Append(temp + "\n");
                }
                var text = Path.Combine(dicUrl, "Config.cs");
                temp = stringBuilder.ToString()
                    .Replace("{0}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                    .Replace("{1}", tableSchemaName);
                WriteFile(text, temp);
            }

            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Service", "SqlSugarBase.txt");
            using (var filestream = new StreamReader(filePath, Encoding.UTF8))
            {
                string temp;
                var stringBuilder = new StringBuilder();
                while ((temp = filestream.ReadLine()) != null)
                {
                    stringBuilder.Append(temp + "\n");
                }
                var text = Path.Combine(dicUrl, "SqlSugarBase.cs");
                temp = stringBuilder.ToString()
                    .Replace("{0}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                    .Replace("{1}", tableSchemaName);
                WriteFile(text, temp);
            }
        }
        #endregion

        #region 生成Domain

        /// <summary>
        /// 生成Domain项目
        /// </summary>
        /// <param name="tableList"></param>
        /// <param name="dicArea"></param>
        /// <param name="tableSchemaName"></param>
        /// <param name="primaryKeyNameList"></param>
        public static void ToDomain(Dictionary<string, string> tableList, Dictionary<string, List<string>> dicArea, string tableSchemaName, List<string> primaryKeyNameList)
        {
            if (tableList == null || tableList.Count == 0) return;

            tableSchemaName = tableSchemaName.ConvertUpper();
            var dicUrl = Path.Combine(_saveFile, tableSchemaName + ".Domain");
            if (!Directory.Exists(dicUrl))
            {
                Directory.CreateDirectory(dicUrl);
            }

            // 整体结构
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Domain", "Domain.txt");
            var stringBuilder = new StringBuilder();
            using (var filestream = new StreamReader(filePath, Encoding.UTF8))
            {
                string temp;
                while ((temp = filestream.ReadLine()) != null)
                {
                    stringBuilder.Append(temp + "\n");
                }
            }

            // 子结构
            var fileChildPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Domain", "DomainChild.txt");
            var stringChildBuilder = new StringBuilder();
            using (var filestream = new StreamReader(fileChildPath, Encoding.UTF8))
            {
                string temp;
                while ((temp = filestream.ReadLine()) != null)
                {
                    stringChildBuilder.Append(temp + "\n");
                }
            }

            //记录合并表的 子结构
            var dicAreaStr = new Dictionary<string, string>();

            var index = 0;
            foreach (var table in tableList)
            {
                var tn = table.Key.ConvertUpper();
                var area = dicArea.FirstOrDefault(t => t.Value.Contains(table.Key));
                if (string.IsNullOrEmpty(area.Key))
                {
                    // 单独生成一个文件
                    var text = Path.Combine(dicUrl, $"I{tn}Repository.cs");
                    var temp = stringBuilder.ToString()
                        .Replace("{child}", stringChildBuilder.ToString())
                        .Replace("{dt}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                        .Replace("{tableName}", tn)
                        .Replace("{namespace}", tableSchemaName)
                        .Replace("{desc}", string.IsNullOrWhiteSpace(table.Value) ? tn : table.Value)
                        .Replace("{primaryKey}", primaryKeyNameList[index++]);
                    WriteFile(text, temp);
                }
                else
                {
                    // 记录合并的子项字符
                    var temp = stringChildBuilder.ToString()
                        .Replace("{tableName}", tn)
                        .Replace("{desc}", string.IsNullOrWhiteSpace(table.Value) ? tn : table.Value)
                        .Replace("{primaryKey}", primaryKeyNameList[index++]);
                    if (dicAreaStr.ContainsKey(area.Key))
                    {
                        dicAreaStr[area.Key] = $"{dicAreaStr[area.Key]}\n{temp}";
                    }
                    else
                    {
                        dicAreaStr[area.Key] = temp;
                    }
                }
            }

            // 生成合并table后的文件
            foreach (var dic in dicAreaStr)
            {
                var key = dic.Key.ConvertUpper();
                var text = Path.Combine(dicUrl, $"I{key}Repository.cs");
                var temp = stringBuilder.ToString()
                    .Replace("{child}", dic.Value)
                    .Replace("{dt}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                    .Replace("{tableName}", key)
                    .Replace("{namespace}", tableSchemaName);
                WriteFile(text, temp);
            }
        }

        #endregion

        #region 生成Core

        /// <summary>
        /// 生成Domain项目
        /// </summary>
        /// <param name="tableList"></param>
        /// <param name="dicArea"></param>
        /// <param name="tableSchemaName"></param>
        /// <param name="primaryKeyNameList"></param>
        public static void ToCore(Dictionary<string, string> tableList, Dictionary<string, List<string>> dicArea, string tableSchemaName, List<string> primaryKeyNameList)
        {
            if (tableList == null || tableList.Count == 0) return;

            tableSchemaName = tableSchemaName.ConvertUpper();
            var dicUrl = Path.Combine(_saveFile, tableSchemaName + ".Core");
            if (!Directory.Exists(dicUrl))
            {
                Directory.CreateDirectory(dicUrl);
            }

            // 整体结构
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Core", "Core.txt");
            var stringBuilder = new StringBuilder();
            using (var filestream = new StreamReader(filePath, Encoding.UTF8))
            {
                string temp;
                while ((temp = filestream.ReadLine()) != null)
                {
                    stringBuilder.Append(temp + "\n");
                }
            }

            // 子结构
            var fileChildPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Core", "CoreChild.txt");
            var stringChildBuilder = new StringBuilder();
            using (var filestream = new StreamReader(fileChildPath, Encoding.UTF8))
            {
                string temp;
                while ((temp = filestream.ReadLine()) != null)
                {
                    stringChildBuilder.Append(temp + "\n");
                }
            }

            //记录合并表的 子结构
            var dicAreaStr = new Dictionary<string, string>();

            var index = 0;
            foreach (var table in tableList)
            {
                var tn = table.Key.ConvertUpper();
                var area = dicArea.FirstOrDefault(t => t.Value.Contains(table.Key));
                if (string.IsNullOrEmpty(area.Key))
                {
                    // 单独生成一个文件
                    var text = Path.Combine(dicUrl, $"{tn}Repository.cs");
                    var temp = stringBuilder.ToString()
                        .Replace("{child}", stringChildBuilder.ToString())
                        .Replace("{dt}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                        .Replace("{tableName}", tn)
                        .Replace("{namespace}", tableSchemaName)
                        .Replace("{desc}", string.IsNullOrWhiteSpace(table.Value) ? tn : table.Value)
                        .Replace("{primaryKey}", primaryKeyNameList[index++]);
                    WriteFile(text, temp);
                }
                else
                {
                    // 记录合并的子项字符
                    var temp = stringChildBuilder.ToString()
                        .Replace("{tableName}", tn)
                        .Replace("{desc}", string.IsNullOrWhiteSpace(table.Value) ? tn : table.Value)
                        .Replace("{primaryKey}", primaryKeyNameList[index++]);
                    if (dicAreaStr.ContainsKey(area.Key))
                    {
                        dicAreaStr[area.Key] = $"{dicAreaStr[area.Key]}\n{temp}";
                    }
                    else
                    {
                        dicAreaStr[area.Key] = temp;
                    }
                }
            }

            // 生成合并table后的文件
            foreach (var dic in dicAreaStr)
            {
                var key = dic.Key.ConvertUpper();
                var text = Path.Combine(dicUrl, $"{key}Repository.cs");
                var temp = stringBuilder.ToString()
                    .Replace("{child}", dic.Value)
                    .Replace("{dt}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                    .Replace("{tableName}", key)
                    .Replace("{namespace}", tableSchemaName);
                WriteFile(text, temp);
            }
        }

        #endregion

        #region 生成Web

        /// <summary>
        /// 生成Domain项目
        /// </summary>
        /// <param name="tableList"></param>
        /// <param name="dicArea"></param>
        /// <param name="tableSchemaName"></param>
        /// <param name="connectionString"></param>
        public static void ToWeb(Dictionary<string, string> tableList, Dictionary<string, List<string>> dicArea, string tableSchemaName, string connectionString)
        {
            if (tableList == null || tableList.Count == 0) return;

            tableSchemaName = tableSchemaName.ConvertUpper();
            var dicUrl = Path.Combine(_saveFile, tableSchemaName + ".Web");
            if (!Directory.Exists(dicUrl))
            {
                Directory.CreateDirectory(dicUrl);
            }

            // 固定文件
            var fileName = new Dictionary<string, string>
            {
                {"Program", ""},
                {"Startup", ""},
                {"HomeController", "Controllers"},
                {"LoginFilter", "Filters"},
                {"Utils", "Extentions"}
            }; //文件名称+文件夹名称
            foreach (var file in fileName)
            {
                var filePathTemp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Web", $"{file.Key}.txt");
                var stringBuilderTemp = new StringBuilder();
                using (var filestream = new StreamReader(filePathTemp, Encoding.UTF8))
                {
                    string strTemp;
                    while ((strTemp = filestream.ReadLine()) != null)
                    {
                        stringBuilderTemp.Append(strTemp + "\n");
                    }
                    if (string.IsNullOrEmpty(file.Value) || !Directory.Exists(Path.Combine(dicUrl, file.Value)))
                    {
                        Directory.CreateDirectory(Path.Combine(dicUrl, file.Value));
                    }
                    var text = Path.Combine(dicUrl, file.Value, $"{file.Key}.cs");
                    var temp = stringBuilderTemp.ToString().Replace("{0}", tableSchemaName);
                    WriteFile(text, temp);
                }
            }
            // 注入文件
            var filePathDxE = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Web", "DependencyExtention.txt");
            using (var filestream = new StreamReader(filePathDxE, Encoding.UTF8))
            {
                string tempDxE;
                var stringBuilderDxE = new StringBuilder();
                while ((tempDxE = filestream.ReadLine()) != null)
                {
                    stringBuilderDxE.Append(tempDxE + "\n");
                }
                var text = Path.Combine(dicUrl, "Extentions", "DependencyExtention.cs");

                var strDxE = string.Empty;
                foreach (var item in tableList)
                {
                    var area = dicArea.FirstOrDefault(t => t.Value.Contains(item.Key));
                    if (!string.IsNullOrEmpty(area.Key)) continue;
                    var key = item.Key.ConvertUpper();
                    strDxE += $"            services.AddScoped<I{key}Repository, {key}Repository>();\n";
                }

                foreach (var item in dicArea)
                {
                    var key = item.Key.ConvertUpper();
                    strDxE += $"            services.AddScoped<I{key}Repository, {key}Repository>();\n";
                }

                var temp = stringBuilderDxE.ToString()
                    .Replace("{0}", tableSchemaName)
                    .Replace("{1}", strDxE);
                WriteFile(text, temp);
            }

            // 控制器文件
            var stringBuilderControllers = new StringBuilder();
            var filePathControllers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Web", "Controllers.txt");
            using (var filestream = new StreamReader(filePathControllers, Encoding.UTF8))
            {
                string tempControllers;
                while ((tempControllers = filestream.ReadLine()) != null)
                {
                    stringBuilderControllers.Append(tempControllers + "\n");
                }
            }

            var stringBuilderChildControllers = new StringBuilder();
            var filePathChildControllers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Web", "ControllersChild.txt");
            using (var filestream = new StreamReader(filePathChildControllers, Encoding.UTF8))
            {
                string tempControllers;
                while ((tempControllers = filestream.ReadLine()) != null)
                {
                    stringBuilderChildControllers.Append(tempControllers + "\n");
                }
            }

            var dicAreaStr = new Dictionary<string, string>();
            foreach (var item in tableList)
            {
                var key = item.Key.ConvertUpper();
                var area = dicArea.FirstOrDefault(t => t.Value.Contains(item.Key));
                if (string.IsNullOrEmpty(area.Key))
                {
                    // 单独生成一个文件
                    var temp = stringBuilderControllers.ToString()
                        .Replace("{child}", stringBuilderChildControllers.ToString())
                        .Replace("{namespace}", tableSchemaName)
                        .Replace("{tableName}", key)
                        .Replace("{desc}", string.IsNullOrWhiteSpace(item.Value) ? key : item.Value);
                    var text = Path.Combine(dicUrl, "Controllers", $"{key}Controllers.cs");
                    WriteFile(text, temp);
                }
                else
                {
                    // 记录合并的子项字符
                    var temp = stringBuilderChildControllers.ToString()
                        .Replace("{tableName}", key)
                        .Replace("{desc}", string.IsNullOrWhiteSpace(item.Value) ? key : item.Value);
                    if (dicAreaStr.ContainsKey(area.Key))
                    {
                        dicAreaStr[area.Key] = $"{dicAreaStr[area.Key]}\n{temp}";
                    }
                    else
                    {
                        dicAreaStr[area.Key] = temp;
                    }
                }
            }
            foreach (var dic in dicAreaStr)
            {
                var key = dic.Key.ConvertUpper();
                var text = Path.Combine(dicUrl, "Controllers", $"{key}Controllers.cs");
                var temp = stringBuilderControllers.ToString()
                    .Replace("{child}", dic.Value)
                    .Replace("{tableName}", key)
                    .Replace("{namespace}", tableSchemaName);
                WriteFile(text, temp);
            }

            //配置文件
            var appsettingsBuilder = new StringBuilder();
            appsettingsBuilder.Append("{\n");
            appsettingsBuilder.AppendFormat("  \"DbConnectionString\": \"{0}\"", connectionString);
            appsettingsBuilder.Append("}\n");
            WriteFile(Path.Combine(dicUrl, "appsettings.json"), appsettingsBuilder.ToString());
            WriteFile(Path.Combine(dicUrl, "appsettings.Development.json"), appsettingsBuilder.ToString());
        }

        #endregion

        #region 生成Lib

        /// <summary>
        /// 生成Lib文件夹
        /// </summary>
        public static void ToLib()
        {
            var dicUrl = Path.Combine(_saveFile, "Lib");
            if (!Directory.Exists(dicUrl))
            {
                Directory.CreateDirectory(dicUrl);
            }

            var templateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateFile", "Lib");
            var files = Directory.GetFiles(templateFilePath);

            foreach (var filePath in files)
            {
                File.Copy(filePath, Path.Combine(dicUrl, filePath.Replace(templateFilePath, "").Replace("\\", "")), true);
            }
            
        }

        #endregion

        #region 生成VS项目文件

            /// <summary>
            /// 生成VS项目文件
            /// </summary>
            /// <param name="tableSchemaName"></param>
            public static void ToCsproj(string tableSchemaName)
        {
            tableSchemaName = tableSchemaName.ConvertUpper();
            var baseUrl = _saveFile;
            //service
            var serviceStr = @"<Project Sdk=”Microsoft.NET.Sdk”>
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=”MySql.Data” Version=”8.0.13” />
    <PackageReference Include=”sqlSugarCore” Version=”4.9.7.4” />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include=”..\{0}.Model\{0}.Model.csproj” />
  </ItemGroup>
  <ItemGroup>
    <Reference Include=”Infrastructure” >
      <HintPath>..\Lib\Infrastructure.dll</HintPath>
    </Reference>
  </ItemGroup>
</Project>";
            serviceStr = serviceStr.Replace("”", "\"").Replace("{0}", tableSchemaName);
            WriteFile(Path.Combine(baseUrl, $"{tableSchemaName}.Service", $"{tableSchemaName}.Service.csproj"), serviceStr);
            //model
            var modelStr = @"<Project Sdk=”Microsoft.NET.Sdk”>
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=”sqlSugarCore” Version=”4.9.7.4” />
  </ItemGroup>
</Project>";
            modelStr = modelStr.Replace("”", "\"").Replace("{0}", tableSchemaName);
            WriteFile(Path.Combine(baseUrl, $"{tableSchemaName}.Model", $"{tableSchemaName}.Model.csproj"), modelStr);
            //domain
            var domainStr = @"<Project Sdk=”Microsoft.NET.Sdk”>
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=”Microsoft.Extensions.Caching.Memory” Version=”2.2.0” />
  </ItemGroup >
  <ItemGroup>
    <ProjectReference Include=”..\{0}.Model\{0}.Model.csproj” />
  </ItemGroup>
</Project>";
            domainStr = domainStr.Replace("”", "\"").Replace("{0}", tableSchemaName);
            WriteFile(Path.Combine(baseUrl, $"{tableSchemaName}.Domain", $"{tableSchemaName}.Domain.csproj"), domainStr);
            //core
            var coreStr = @"<Project Sdk=”Microsoft.NET.Sdk”>
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <PropertyGroup Condition=”'$(Configuration)|$(Platform)'=='Debug|AnyCPU'”>
    <OutputPath></OutputPath>
  </PropertyGroup>
  <PropertyGroup Condition=”'$(Configuration)|$(Platform)'=='Release|AnyCPU'”>
    <OutputPath></OutputPath>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include=”..\{0}.Domain\{0}.Domain.csproj” />
    <ProjectReference Include=”..\{0}.Model\{0}.Model.csproj” />
    <ProjectReference Include=”..\{0}.Service\{0}.Service.csproj” />
  </ItemGroup>
  <ItemGroup>
    <Reference Include=”Infrastructure” >
      <HintPath>..\Lib\Infrastructure.dll</HintPath>
    </Reference>
  </ItemGroup>
</Project>";
            coreStr = coreStr.Replace("”", "\"").Replace("{0}", tableSchemaName);
            WriteFile(Path.Combine(baseUrl, $"{tableSchemaName}.Core", $"{tableSchemaName}.Core.csproj"), coreStr);
            //web
            var webStr = @"<Project Sdk=”Microsoft.NET.Sdk.Web”>
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <PropertyGroup Condition=”'$(Configuration)|$(Platform)'=='Debug|AnyCPU'”>
    <OutputPath>bin\</OutputPath>
    <DocumentationFile></DocumentationFile>
  </PropertyGroup>
  <PropertyGroup Condition=”'$(Configuration)|$(Platform)'=='Release|AnyCPU'”>
    <OutputPath>bin\</OutputPath>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=”Microsoft.AspNetCore.All” Version=”2.2.0” />
    <PackageReference Include=”Microsoft.Extensions.Caching.Memory” Version=”2.2.0” />
    <PackageReference Include=”Swashbuckle.AspNetCore” Version=”4.0.1” />
  </ItemGroup>
  <ItemGroup>
    <DotNetCliToolReference Include=”Microsoft.VisualStudio.Web.CodeGeneration.Tools” Version=”2.0.3” />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include=”..\{0}.Core\{0}.Core.csproj” />
    <ProjectReference Include=”..\{0}.Domain\{0}.Domain.csproj” />
  </ItemGroup>
  <ItemGroup>
    <Reference Include=”Infrastructure” >
      <HintPath>..\Lib\Infrastructure.dll</HintPath>
    </Reference>
  </ItemGroup>
  <ItemGroup>
    <Content Update=”appsettings.Development.json”>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
    </Content>
    <Content Update=”appsettings.json”>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
  </ItemGroup>
</Project>";
            webStr = webStr.Replace("”", "\"").Replace("{0}", tableSchemaName);
            WriteFile(Path.Combine(baseUrl, $"{tableSchemaName}.Web", $"{tableSchemaName}.Web.csproj"), webStr);

            //解决方案
            var slnStr = @"Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 15
VisualStudioVersion = 15.0.26124.0
MinimumVisualStudioVersion = 15.0.26124.0
Project(”{9A19103F-16F7-4668-BE54-9A1E7A4F7556}”) = ”{0}.Service”, ”{0}.Service\{0}.Service.csproj”, ”{{guidPeoject01}}”
EndProject
Project(”{9A19103F-16F7-4668-BE54-9A1E7A4F7556}”) = ”{0}.Core”, ”{0}.Core\{0}.Core.csproj”, ”{{guidPeoject02}}”
EndProject
Project(”{9A19103F-16F7-4668-BE54-9A1E7A4F7556}”) = ”{0}.Model”, ”{0}.Model\{0}.Model.csproj”, ”{{guidPeoject03}}”
EndProject
Project(”{9A19103F-16F7-4668-BE54-9A1E7A4F7556}”) = ”{0}.Domain”, ”{0}.Domain\{0}.Domain.csproj”, ”{{guidPeoject04}}”
EndProject
Project(”{9A19103F-16F7-4668-BE54-9A1E7A4F7556}”) = ”{0}.Web”, ”{0}.Web\{0}.Web.csproj”, ”{{guidPeoject05}}”
EndProject
Project(”{2150E333-8FDC-42A3-9474-1A3956D46DE8}”) = ”Service”, ”Service”, ”{{guidFolder01}}”
EndProject
Project(”{2150E333-8FDC-42A3-9474-1A3956D46DE8}”) = ”Core”, ”Core”, ”{{guidFolder02}}”
EndProject
Project(”{2150E333-8FDC-42A3-9474-1A3956D46DE8}”) = ”Domain”, ”Domain”, ”{{guidFolder03}}”
EndProject
Project(”{2150E333-8FDC-42A3-9474-1A3956D46DE8}”) = ”Web”, ”Web”, ”{{guidFolder04}}”
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Debug|x64 = Debug|x64
		Debug|x86 = Debug|x86
		Release|Any CPU = Release|Any CPU
		Release|x64 = Release|x64
		Release|x86 = Release|x86
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{guidPeoject01}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{guidPeoject01}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{guidPeoject01}}.Debug|x64.ActiveCfg = Debug|Any CPU
		{{guidPeoject01}}.Debug|x64.Build.0 = Debug|Any CPU
		{{guidPeoject01}}.Debug|x86.ActiveCfg = Debug|Any CPU
		{{guidPeoject01}}.Debug|x86.Build.0 = Debug|Any CPU
		{{guidPeoject01}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{guidPeoject01}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{guidPeoject01}}.Release|x64.ActiveCfg = Release|Any CPU
		{{guidPeoject01}}.Release|x64.Build.0 = Release|Any CPU
		{{guidPeoject01}}.Release|x86.ActiveCfg = Release|Any CPU
		{{guidPeoject01}}.Release|x86.Build.0 = Release|Any CPU
		{{guidPeoject02}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{guidPeoject02}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{guidPeoject02}}.Debug|x64.ActiveCfg = Debug|Any CPU
		{{guidPeoject02}}.Debug|x64.Build.0 = Debug|Any CPU
		{{guidPeoject02}}.Debug|x86.ActiveCfg = Debug|Any CPU
		{{guidPeoject02}}.Debug|x86.Build.0 = Debug|Any CPU
		{{guidPeoject02}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{guidPeoject02}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{guidPeoject02}}.Release|x64.ActiveCfg = Release|Any CPU
		{{guidPeoject02}}.Release|x64.Build.0 = Release|Any CPU
		{{guidPeoject02}}.Release|x86.ActiveCfg = Release|Any CPU
		{{guidPeoject02}}.Release|x86.Build.0 = Release|Any CPU
		{{guidPeoject03}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{guidPeoject03}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{guidPeoject03}}.Debug|x64.ActiveCfg = Debug|Any CPU
		{{guidPeoject03}}.Debug|x64.Build.0 = Debug|Any CPU
		{{guidPeoject03}}.Debug|x86.ActiveCfg = Debug|Any CPU
		{{guidPeoject03}}.Debug|x86.Build.0 = Debug|Any CPU
		{{guidPeoject03}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{guidPeoject03}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{guidPeoject03}}.Release|x64.ActiveCfg = Release|Any CPU
		{{guidPeoject03}}.Release|x64.Build.0 = Release|Any CPU
		{{guidPeoject03}}.Release|x86.ActiveCfg = Release|Any CPU
		{{guidPeoject03}}.Release|x86.Build.0 = Release|Any CPU
		{{guidPeoject04}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{guidPeoject04}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{guidPeoject04}}.Debug|x64.ActiveCfg = Debug|Any CPU
		{{guidPeoject04}}.Debug|x64.Build.0 = Debug|Any CPU
		{{guidPeoject04}}.Debug|x86.ActiveCfg = Debug|Any CPU
		{{guidPeoject04}}.Debug|x86.Build.0 = Debug|Any CPU
		{{guidPeoject04}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{guidPeoject04}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{guidPeoject04}}.Release|x64.ActiveCfg = Release|Any CPU
		{{guidPeoject04}}.Release|x64.Build.0 = Release|Any CPU
		{{guidPeoject04}}.Release|x86.ActiveCfg = Release|Any CPU
		{{guidPeoject04}}.Release|x86.Build.0 = Release|Any CPU
		{{guidPeoject05}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{guidPeoject05}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{guidPeoject05}}.Debug|x64.ActiveCfg = Debug|Any CPU
		{{guidPeoject05}}.Debug|x64.Build.0 = Debug|Any CPU
		{{guidPeoject05}}.Debug|x86.ActiveCfg = Debug|Any CPU
		{{guidPeoject05}}.Debug|x86.Build.0 = Debug|Any CPU
		{{guidPeoject05}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{guidPeoject05}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{guidPeoject05}}.Release|x64.ActiveCfg = Release|Any CPU
		{{guidPeoject05}}.Release|x64.Build.0 = Release|Any CPU
		{{guidPeoject05}}.Release|x86.ActiveCfg = Release|Any CPU
		{{guidPeoject05}}.Release|x86.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{{guidPeoject01}} = {{guidFolder01}}
		{{guidPeoject02}} = {{guidFolder02}}
		{{guidPeoject03}} = {{guidFolder03}}
		{{guidPeoject04}} = {{guidFolder03}}
		{{guidPeoject05}} = {{guidFolder04}}
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {{guidSln}}
	EndGlobalSection
EndGlobal";
            slnStr = slnStr.Replace("”", "\"")
                .Replace("{0}", tableSchemaName)
                .Replace("{guidSln}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidFolder01}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidFolder02}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidFolder03}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidFolder04}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidPeoject01}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidPeoject02}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidPeoject03}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidPeoject04}", Guid.NewGuid().ToString().ToUpper())
                .Replace("{guidPeoject05}", Guid.NewGuid().ToString().ToUpper());

            WriteFile(Path.Combine(baseUrl, $"{tableSchemaName}.sln"), slnStr);
        }

        #endregion

    }
}
