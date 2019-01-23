using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("sys_permission")]
    public class SysPermissionEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName= "permission_id")]
        public int PermissionId { get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "permission_code")]
        public string PermissionCode { get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>    
        [SugarColumn(ColumnName = "permission_name")]
        public string PermissionName { get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>    
        [SugarColumn(ColumnName = "menu_id")]
        public int MenuId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>    
        [SugarColumn(ColumnName = "status")]
        public bool Status { get; set; }

    }
}
