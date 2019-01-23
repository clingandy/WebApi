using System;
using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("sys_user")]
    public class SysUserEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="user_id")]
        public int UserId {get;set;}

        /// <summary>
        /// Desc:用户登录名称
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="user_name")]
        public string UserName {get;set;}

        /// <summary>
        /// Desc:用户编码
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="user_code")]
        public string UserCode {get;set;}

        /// <summary>
        /// Desc:用户密码
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="user_pwd")]
        public string UserPwd {get;set;}

        /// <summary>
        /// Desc:机构ID
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="org_id")]
        public int OrgId {get;set;}

        /// <summary>
        /// Desc:角色ID
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "role_id")]
        public int RoleId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="modify_time")]
        public DateTime ModifyTime {get;set;}

        /// <summary>
        /// Desc:0 不可用 1 可用
        /// Default:1
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="status")]
        public bool Status {get;set;}

        /// <summary>
        /// Desc:1表示账户锁住
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "is_lock")]
        public bool IsLock { get; set; }

        /// <summary>
        /// Desc:登录错误次数，5次后上锁，隔天重置
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "error_count")]
        public int ErrorCount { get; set; }
    }
}
