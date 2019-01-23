/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/10 12:15:14
** desc：    MenuRouterView类
** Ver.:     V1.0.0
*********************************************************************************/
using System.Collections.Generic;

namespace Infrastructure.Model.View.Account
{
    public class MenuRouterView
    {
        public int MenuId { get; set; }

        public int? PMenuId { get; set; }


        public string Title { get; set; }


        public string Icon { get; set; }

        //public string Roles { get; set; }

        public Dictionary<string, bool> PermissionList { get; set; }

        public string VueFile { get; set; }

        public string VueUrl { get; set; }

        public string RouterName { get; set; }

        public bool IsShow { get; set; }
    }
}
