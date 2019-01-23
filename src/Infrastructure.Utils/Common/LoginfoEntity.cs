/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:51:46
** desc：    LoginfoEntity类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;

namespace Infrastructure.Common
{
    public class LoginfoEntity
    {
        public string Message
        {
            get;
            set;
        }

        public LogType Type
        {
            get;
            set;
        }

        public string ChildLogDirectory
        {
            get;
            set;
        }

        public Dictionary<string, string> Parameters
        {
            get;
            set;
        }
    }
}
