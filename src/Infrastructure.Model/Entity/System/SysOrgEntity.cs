using System;
using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///组织
    ///</summary>
    [SugarTable("sys_org")]
    public class SysOrgEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="org_id")]
        public int OrgId {get;set;}

        /// <summary>
        /// Desc:父级ID
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName="p_org_id")]
        public int? POrgId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="org_name")]
        public string OrgName {get;set;}

        /// <summary>
        /// Desc:排序
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
        [SugarColumn(ColumnName = "description")]
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
        /// Default:1
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="status")]
        public bool Status {get;set;}

    }
}
