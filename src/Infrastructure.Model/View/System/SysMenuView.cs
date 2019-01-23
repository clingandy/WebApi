/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/18 12:17:41
** desc：    SysMenuView类
** Ver.:     V1.0.0
*********************************************************************************/
using System.Collections.Generic;
using Infrastructure.Model.Entity.System;

namespace Infrastructure.Model.View.System
{
    public class SysMenuView : SysMenuEntity
    {
        public List<SysMenuChildrenView> Children { get; set; }
        public List<SysPermissionEntity> Permission { get; set; }
    }

    public class SysMenuChildrenView : SysMenuEntity
    {
        public List<SysPermissionEntity> Permission { get; set; }
    }
}
