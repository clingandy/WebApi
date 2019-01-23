/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:41:08
** desc：    JsonUtils类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Infrastructure.Common
{
    public static class JsonUtils
    {
        public static int GetIntProperty(string key, string json)
        {
            int result = 0;
            Regex regex = new Regex($"\"{key}\":(?<value>-?\\d+)", RegexOptions.IgnoreCase);
            Match match = regex.Match(json);
            if (match.Success)
            {
                string value = match.Groups["value"].Value;
                int.TryParse(value, out result);
                return result;
            }
            return 0;
        }

        public static string GetStringProperty(string key, string json)
        {
            Regex regex = new Regex($"\"{key}\":\"(?<value>.+?)\"", RegexOptions.IgnoreCase);
            Match match = regex.Match(json);
            if (match.Success)
            {
                return match.Groups["value"].Value;
            }
            return string.Empty;
        }

        public static string JsonSerialize(this object obj)
        {
            string input = JsonConvert.SerializeObject(obj);
            return Regex.Replace(input, "\\\\/Date\\((-?(\\d+))\\)\\\\/", delegate (Match match)
            {
                DateTime dateTime = new DateTime(1970, 1, 1);
                dateTime = dateTime.AddMilliseconds((double)long.Parse(match.Groups[1].Value));
                dateTime = dateTime.ToLocalTime();
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            });
        }

        public static T JsonDeserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
