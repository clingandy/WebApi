/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 17:49:39
** desc：    UrlUtils类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Common
{
    public class UrlUtils
    {
        public static string BuildUrl(string url, object searchObj)
        {
            if (searchObj == null)
            {
                return url;
            }
            PropertyInfo[] properties = searchObj.GetType().GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                url = BuildUrl(url, propertyInfo.Name, Convert.ToString(propertyInfo.GetValue(searchObj, null)));
            }
            return url;
        }

        public static string BuildUrl(string url, string paramName, string paramValue)
        {
            Regex regex = new Regex($"{paramName}=[^&]*", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string text = regex.Replace(url, "");
            if (text.IndexOf("?", StringComparison.Ordinal) == -1)
            {
                text += $"?{paramName}={paramValue}";
            }
            else
            {
                text += $"&{paramName}={paramValue}";
            }
            text = regex2.Replace(text, "&");
            return text.Replace("?&", "?");
        }

        public static string RemoveParam(string url, string paramName)
        {
            Regex regex = new Regex($"{paramName}=[^&]*", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string text = regex.Replace(url, "");
            text = regex2.Replace(text, "&");
            return text.Replace("?&", "");
        }

        public static string GetQueryString(string url, string paramName)
        {
            Regex regex = new Regex($"{paramName}=[^&]*", RegexOptions.IgnoreCase);
            Match match = regex.Match(url);
            if (match.Length > 0)
            {
                return match.Value.Split(new char[]
                {
                    '='
                })[1];
            }
            return string.Empty;
        }

        public static string GetDomainName(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }
            Regex regex = new Regex("http(s)?://([\\w-]+\\.)+[\\w-]+/?");
            return regex.Match(url, 0).Value;
        }

        public static string ResolveUrl(string relativeUrl)
        {
            if (relativeUrl == null)
            {
                throw new ArgumentNullException(nameof(relativeUrl));
            }
            if (relativeUrl.Length == 0 || relativeUrl[0] == '/' || relativeUrl[0] == '\\')
            {
                return relativeUrl;
            }
            int num = relativeUrl.IndexOf("://", StringComparison.Ordinal);
            if (num != -1)
            {
                int num2 = relativeUrl.IndexOf('?');
                if (num2 == -1 || num2 > num)
                {
                    return relativeUrl;
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Directory.GetCurrentDirectory());
            if (stringBuilder.Length == 0 || stringBuilder[stringBuilder.Length - 1] != '/')
            {
                stringBuilder.Append('/');
            }
            bool flag = false;
            bool flag2;
            if (relativeUrl.Length > 1 && relativeUrl[0] == '~' && (relativeUrl[1] == '/' || relativeUrl[1] == '\\'))
            {
                relativeUrl = relativeUrl.Substring(2);
                flag2 = true;
            }
            else
            {
                flag2 = false;
            }
            string text = relativeUrl;
            int i = 0;
            while (i < text.Length)
            {
                char c = text[i];
                if (flag)
                {
                    goto IL_109;
                }
                if (c == '?')
                {
                    flag = true;
                    goto IL_109;
                }
                if (c == '/' || c == '\\')
                {
                    if (!flag2)
                    {
                        stringBuilder.Append('/');
                        flag2 = true;
                    }
                }
                else
                {
                    if (flag2)
                    {
                        flag2 = false;
                        goto IL_109;
                    }
                    goto IL_109;
                }
                IL_112:
                i++;
                continue;
                IL_109:
                stringBuilder.Append(c);
                goto IL_112;
            }
            return stringBuilder.ToString();
        }
    }
}
