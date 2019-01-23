/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/19 12:09:05
** desc：    AddRolePermissionView类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Model.View.System
{
    public class AddRolePermissionView
    {
        public int RoleId { get; set; }

        public List<int> MenuIds { get; set; }

        public List<int> PermissionIds { get; set; }
    }
}
