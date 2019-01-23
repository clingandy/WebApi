/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:27:21
** desc：    IdentityCardHelper类
** Ver.:     V1.0.0
*********************************************************************************/

namespace Infrastructure.Common
{
    public class IdentityCardHelper
    {
        public static int GetGender(string identityCard)
        {
            if (identityCard.Length != 15 && identityCard.Length != 18)
            {
                return 0;
            }
            string s = string.Empty;
            if (identityCard.Length == 18)
            {
                s = identityCard.Substring(14, 3);
            }
            if (identityCard.Length == 15)
            {
                s = identityCard.Substring(12, 3);
            }
            if (int.Parse(s) % 2 == 0)
            {
                return 2;
            }
            return 1;
        }
    }
}
