/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 17:43:51
** desc：    UrlEncoderUtils类
** Ver.:     V1.0.0
*********************************************************************************/
using System.Collections.Generic;

namespace Infrastructure.Common
{
    public class UrlEncoderUtils
    {
        private static Dictionary<string, string> Dict => new Dictionary<string, string>
        {
            {
                "+",
                "%2b"
            },
            {
                "/",
                "%2f"
            },
            {
                "?",
                "%3f"
            },
            {
                "%",
                "%2f"
            },
            {
                "#",
                "%23"
            },
            {
                "&",
                "%26"
            },
            {
                "=",
                "%3d"
            }
        };

        public static bool HasUrlEncoded(string data)
        {
            string text = data.ToLower();
            foreach (var current in Dict)
            {
                if (text.Contains(current.Value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
