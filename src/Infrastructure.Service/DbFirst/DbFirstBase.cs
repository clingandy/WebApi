/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/28 11:49:19
** desc：    DbFirstBase类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;
using Infrastructure.Service.Base;

namespace Infrastructure.Service.DbFirst
{
    /// <summary>
    /// 用于生成实体
    /// </summary>
    public class DbFirstBase : SqlSugarBase
    {

        /// <summary>
        /// 创建实体文件
        /// </summary>
        /// <param name="path">生成路径</param>
        public static void CreateClassFile(string path)
        {
            var db = GetInstance();
            db.DbFirst.CreateClassFile(path);
        }

        /// <summary>
        /// 创建实体文件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="path">生成路径</param>
        public static void CreateClassFile(string[] tableName, string path)
        {
            var db = GetInstance();
            db.DbFirst.Where(tableName).CreateClassFile(path);
        }

        /// <summary>
        /// 创建实体文件
        /// </summary>
        /// <param name="startsWithStr">开始包含的字符</param>
        /// <param name="path">生成路径</param>
        public static void CreateClassFile(string startsWithStr, string path)
        {
            var db = GetInstance();
            db.DbFirst.Where(t=> t.ToLower().StartsWith(startsWithStr)).CreateClassFile(path);
        }

        /// <summary>
        /// 创建实体文件 带别名
        /// </summary>
        /// <param name="mappingTables">【实体表名、数据库表名】</param>
        /// <param name="mappingColumns">【数据库表名|实体字段名、数据库字段名】；数据库表名有实体表名，需用实体表名</param>
        /// <param name="startsWithStr">开始包含的字符</param>
        /// <param name="path">生成路径</param>
        public static void CreateClassFile(Dictionary<string,string> mappingTables, Dictionary<string, string> mappingColumns, string startsWithStr, string path)
        {
            var db = GetInstance();

            foreach (var item in mappingTables)
            {
                db.MappingTables.Add(item.Key, item.Value);
            }
            foreach (var item in mappingColumns)
            {
                var tempStrArray = item.Key.Split("|");
                db.MappingColumns.Add(tempStrArray[1], item.Value, tempStrArray[0]);
            }
            db.DbFirst.IsCreateAttribute().Where(t => t.ToLower().StartsWith(startsWithStr)).CreateClassFile(path);
        }

    }
}
