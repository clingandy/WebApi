using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("sys_role_permission")]
    public class SysRolePermissionEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName= "role_permission_id")]
        public int RolePermissionId { get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="role_id")]
        public int RoleId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>    
        [SugarColumn(ColumnName = "permission_id")]
        public int PermissionId { get;set;}

    }
}
