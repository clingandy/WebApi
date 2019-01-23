/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/10 12:00:47
** desc：    LoginUserView类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;

namespace Infrastructure.Model.View.Account
{
    public class LoginUserView
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        //public string UserCode { get; set; }

        //public int OrgId { get; set; }

        public List<int> RoleIds { get; set; }

        //public string Token { get; set; }
    }
}
