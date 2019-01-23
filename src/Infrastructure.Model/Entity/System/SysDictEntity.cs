using System;
using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///字典表
    ///</summary>
    [SugarTable("sys_dict")]
    public  class SysDictEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="dict_id")]
        public int DictId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "dict_pid")]
        public int DictPid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="dict_name")]
        public string DictName {get;set;}

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
        /// Nullable:True
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
