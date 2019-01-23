/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 17:12:37
** desc：    StringUtils类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Infrastructure.Common
{
    public static class StringUtils
    {
        private static readonly StringBuilder Sb = new StringBuilder();

        public static string EncodeBase64(this string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        public static string DecodeBase64(this string source)
        {
            byte[] bytes = Convert.FromBase64String(source);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string CreateVerifyCode(bool numberFlag, int length)
        {
            string text = numberFlag ? "0,1,2,3,4,5,6,7,8,9" : "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            if (length == 0)
            {
                length = 6;
            }
            string[] array = text.Split(new char[]
            {
                ','
            });
            string text2 = "";
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i++)
            {
                int num = random.Next(0, array.Length - 1);
                text2 += array[num];
            }
            return text2;
        }

        public static string ConvertStreamToString(this Stream s)
        {
            Sb.Remove(0, Sb.Length);
            StreamReader streamReader = new StreamReader(s, Encoding.UTF8);
            char[] array = new char[256];
            for (int i = streamReader.Read(array, 0, 256); i > 0; i = streamReader.Read(array, 0, 256))
            {
                string value = new string(array, 0, i);
                Sb.Append(value);
            }
            streamReader.Close();
            return Sb.ToString();
        }

        public static string ConvertSql(string str)
        {
            str = str.Trim();
            str = str.Replace("'", "''");
            str = str.Replace(";--", "");
            str = str.Replace("/*", "");
            str = str.Replace("*/", "");
            str = str.Replace("delete", "");
            str = str.Replace("drop", "");
            str = str.Replace("update", "");
            str = str.Replace("select", "");
            str = str.Replace("=", "");
            str = str.Replace(" or ", "");
            str = str.Replace(" and ", "");
            return str;
        }

        public static string ConvertJavaScript(string str)
        {
            StringBuilder stringBuilder = new StringBuilder(str).Replace("'", "").Replace("<", "").Replace(">", "").Replace("`", "");
            return stringBuilder.ToString();
        }

        public static string ObjectToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            try
            {
                result = (obj as string);
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        public static string StringToJson(string s, string pattern)
        {
            return Regex.Replace(s, pattern, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        public static bool IsMobileNumber(this string mobileNumber)
        {
            return Regex.IsMatch(mobileNumber, "^1[23456789]\\d{9}$", RegexOptions.IgnoreCase);
        }

        public static bool IsValidEmail(this string email)
        {
            return Regex.IsMatch(email, "^.+@[a-zA-Z0-9-.]+.([a-zA-Z]{2,3}|[0-9]{1,3})$");
        }

        public static bool IsValidURL(this string url)
        {
            return Regex.IsMatch(url, "^(http|https|ftp)://[a-zA-Z0-9-.]+.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9-._?,'/\\+&%$#=~])*[^.,)(s]$");
        }

        public static bool IsValidInt(this string val)
        {
            return Regex.IsMatch(val, "^[1-9]d*.?[0]*$");
        }

        public static bool IsNullOrWhiteSpace(this string val)
        {
            return string.IsNullOrWhiteSpace(val);
        }

        public static bool IsNum(this string str)
        {
            bool flag = true;
            if (string.IsNullOrEmpty(str))
            {
                flag = false;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    if (!char.IsNumber(c))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag && int.Parse(str) == 0)
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static bool IsDouble(this string str)
        {
            bool result = true;
            if (str == "")
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    if (!char.IsNumber(c) && c.ToString() != "-")
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        public static string Strings(this char Char, int i)
        {
            string text = null;
            for (int j = 0; j < i; j++)
            {
                text += Char;
            }
            return text;
        }

        public static string MakeName()
        {
            return DateTime.Now.ToString("yyMMddHHmmssfff");
        }

        public static int Len(this string str)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c > '\u007f')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }

        public static int GetBytesLen(string str)
        {
            Encoding encoding = Encoding.GetEncoding("gb2312");
            byte[] bytes = encoding.GetBytes(str);
            return bytes.Length;
        }

        public static string CutLen(this string str, int length)
        {
            return CutLen(str, length, string.Empty);
        }

        public static string CutLen(this string str, int length, string suffixStr)
        {
            int num = 0;
            int num2 = 0;
            string text = str;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c > '\u007f')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num > length)
                {
                    str = str.Substring(0, num2 - 2) + suffixStr;
                    break;
                }
                num2++;
            }
            return str;
        }

        public static string RandomNum(int intLong)
        {
            string text = "";
            Random random = new Random();
            for (int i = 0; i < intLong; i++)
            {
                text += random.Next(10);
            }
            return text;
        }

        public static string RandomStr(int intLong)
        {
            string text = "";
            string[] array = new string[]
            {
                "a",
                "b",
                "c",
                "d",
                "e",
                "f",
                "g",
                "h",
                "i",
                "j",
                "k",
                "l",
                "m",
                "n",
                "o",
                "p",
                "q",
                "r",
                "s",
                "t",
                "u",
                "v",
                "w",
                "x",
                "y",
                "z"
            };
            Random random = new Random();
            for (int i = 0; i < intLong; i++)
            {
                text += array[random.Next(26)];
            }
            return text;
        }

        public static string RandomNumStr(int intLong)
        {
            string text = "";
            string[] array = new string[]
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "a",
                "b",
                "c",
                "d",
                "e",
                "f",
                "g",
                "h",
                "i",
                "j",
                "k",
                "l",
                "m",
                "n",
                "o",
                "p",
                "q",
                "r",
                "s",
                "t",
                "u",
                "v",
                "w",
                "x",
                "y",
                "z"
            };
            Random random = new Random();
            for (int i = 0; i < intLong; i++)
            {
                text += array[random.Next(36)];
            }
            return text;
        }

        public static int GetInt32(this string str)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                int.TryParse(str, out result);
            }
            return result;
        }

        public static long GetInt64(this string str)
        {
            long result = 0L;
            if (!string.IsNullOrWhiteSpace(str))
            {
                long.TryParse(str, out result);
            }
            return result;
        }

        public static double GetDouble(this string str)
        {
            double result = 0.0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                double.TryParse(str, out result);
            }
            return result;
        }

        public static string HtmlEncode(this string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        public static string HtmlDecode(this string input)
        {
            return HttpUtility.HtmlDecode(input);
        }

        public static string GetStringJoin<T>(this IEnumerable<T> input, string splitStr)
        {
            return string.Join(splitStr, input);
        }

        /// <summary>
        /// 字符转List<int/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitChar">默认,</param>
        /// <returns></returns>
        public static List<int> StringConvertIntList(this string str, char[] splitChar = null)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<int>();
            }
            if (splitChar == null)
            {
                splitChar = new[] { ',' };
            }
            return Regex.Replace(str, "\\D", ",").Split(splitChar, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        }
    }
}
