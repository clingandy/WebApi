using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("sys_role_menu")]
    public class SysRoleMenuEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="role_menu_id")]
        public int RoleMenuId {get;set;}

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
        [SugarColumn(ColumnName = "menu_id")]
        public int MenuId {get;set;}

    }
}
