/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 12:31:08
** desc：    DataMasking类
** Ver.:     V1.0.0
*********************************************************************************/
using System.Text.RegularExpressions;

namespace Infrastructure.Common
{
    public class DataMasking
    {
        public static string GetMobile(string mobile)
        {
            Regex regex = new Regex("(\\d{3})(\\d{4})(\\d{4})", RegexOptions.None);
            return regex.Replace(mobile, "$1****$3");
        }

        public static string GetEmail(string email)
        {
            Regex regex = new Regex("(.{2}).+(.{2}@.+)", RegexOptions.None);
            return regex.Replace(email, "$1****$2");
        }

        public static string GetBankAccount(string bankAccount)
        {
            if (string.IsNullOrEmpty(bankAccount) || bankAccount.Length < 4)
            {
                return bankAccount;
            }
            return bankAccount.Substring(bankAccount.Length - 4, 4);
        }
    }
}
