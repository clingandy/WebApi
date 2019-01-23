using System;
using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///字典数据
    ///</summary>
    [SugarTable("sys_dict_data")]
    public class SysDictDataEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="dict_data_id")]
        public int DictDataId {get;set;}

        /// <summary>
        /// Desc:字典ID
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="dict_id")]
        public int DictId {get;set;}

        /// <summary>
        /// Desc:Key
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="dict_data_key")]
        public string DictDataKey {get;set;}

        /// <summary>
        /// Desc:Value
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="dict_data_value")]
        public string DictDataValue {get;set;}

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "sort")]
        public int Sort { get;set;}

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="description")]
        public string Description {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="modify_time")]
        public DateTime ModifyTime {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="status")]
        public bool Status {get;set;}

    }
}
