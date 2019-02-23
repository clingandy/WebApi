/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/2/14 17:30:44
** desc：    ColumnEntity类
** Ver.:     V1.0.0
*********************************************************************************/

namespace CodeGeneration
{
    public class ColumnEntity
    {
        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get;set; }

        public string Nullable { get;set;}

        public string DataType { get; set; }

        public string ColumnComment { get; set; }

        public string Default { get; set; }

        public string Key { get; set; }

        public string Extra { get; set; }

    }
}
