using System;
using SqlSugar;

namespace Infrastructure.Model.Entity.System
{
    ///<summary>
    ///菜单表
    ///</summary>
    [SugarTable("sys_menu")]
    public class SysMenuEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="menu_id")]
        public int MenuId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName="p_menu_id")]
        public int? PMenuId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="menu_name")]
        public string MenuName {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName="menu_icon")]
        public string MenuIcon {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "org_id")]
        public int OrgId { get; set; }

        /// <summary>
        /// Desc:1显示 0不显示
        /// Default:1
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName="is_show")]
        public bool IsShow {get;set;}

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "sort")]
        public int Sort { get;set;}

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

        /// <summary>
        /// Desc:VUE路由名称
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName= "router_name")]
        public string RouterName { get;set;}

        /// <summary>
        /// Desc:vue文件
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "vue_file")]
        public string VueFile { get;set;}

        /// <summary>
        /// Desc:vue_url
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "vue_url")]
        public string VueUrl { get;set;}

    }
}
