using System;
using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///角色表
    ///</summary>
    [SugarTable("sys_role")]
    public class SysRoleEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="role_id")]
        public int RoleId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "org_id")]
        public int OrgId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="role_name")]
        public string RoleName {get;set;}

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
