/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 11:00:27
** desc：    AppSettingsHelper类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common
{
    public class AppSettingsHelper
    {
        private static IConfigurationRoot Config = null;
        static AppSettingsHelper()
        {
            // Microsoft.Extensions.Configuration.Json扩展包提供的
#if DEBUG
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json");
            Config = builder.Build();
#else
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Config = builder.Build();
#endif
        }

        public static bool GetBoolValue(string key)
        {
            bool result = false;
            if (Config[key] != null)
            {
                bool.TryParse(Config[key], out result);
            }
            return result;
        }

        public static int GetIntValue(string key)
        {
            int result = 0;
            if (Config[key] != null)
            {
                int.TryParse(Config[key], out result);
            }
            return result;
        }

        public static string GetString(string key)
        {
            return GetValue(key, true, null);
        }

        public static string GetString(string key, string defaultValue)
        {
            return GetValue(key, false, defaultValue);
        }

        public static string[] GetStringArray(string key, string separator)
        {
            return GetStringArray(key, separator, true, null);
        }

        public static string[] GetStringArray(string key, string separator, string[] defaultValue)
        {
            return GetStringArray(key, separator, false, defaultValue);
        }

        private static string[] GetStringArray(string key, string separator, bool valueRequired, string[] defaultValue)
        {
            string value = GetValue(key, valueRequired, null);
            if (!string.IsNullOrEmpty(value))
            {
                return value.Split(new []
                {
                    separator
                }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!valueRequired)
            {
                return defaultValue;
            }
            throw new ApplicationException("在配置文件的appSettings节点集合中找不到key为" + key + "的子节点，且没有指定默认值");
        }

        public static int GetInt32(string key)
        {
            return GetInt32(key, null);
        }

        public static int GetInt32(string key, int defaultValue)
        {
            return GetInt32(key, new int?(defaultValue));
        }

        private static int GetInt32(string key, int? defaultValue)
        {
            return GetValue(key, delegate (string v, int pv)
            {
                int.TryParse(v, out pv);
                return pv;
            }, defaultValue);
        }

        public static bool GetBoolean(string key)
        {
            return GetBoolean(key, null);
        }

        public static bool GetBoolean(string key, bool defaultValue)
        {
            return GetBoolean(key, new bool?(defaultValue));
        }

        private static bool GetBoolean(string key, bool? defaultValue)
        {
            return GetValue(key, delegate (string v, bool pv)
            {
                bool.TryParse(v, out pv);
                return pv;
            }, defaultValue);
        }

        public static TimeSpan GetTimeSpan(string key)
        {
            return TimeSpan.Parse(GetValue(key, true, null));
        }

        public static TimeSpan GetTimeSpan(string key, TimeSpan defaultValue)
        {
            string value = GetValue(key, false, null);
            if (value == null)
            {
                return defaultValue;
            }
            return TimeSpan.Parse(value);
        }

        public static DateTime GetDateTime(string key)
        {
            return DateTime.Parse(GetValue(key, true, null));
        }

        public static DateTime GetDateTime(string key, DateTime defaultValue)
        {
            string value = GetValue(key, false, null);
            if (value == null)
            {
                return defaultValue;
            }
            return DateTime.Parse(value);
        }

        private static T GetValue<T>(string key, Func<string, T, T> parseValue, T? defaultValue) where T : struct
        {
            string text = Config[key];
            if (!string.IsNullOrEmpty(text))
            {
                return parseValue(text, default(T));
            }
            if (!defaultValue.HasValue)
            {
                throw new ApplicationException("在配置文件的appSettings节点集合中找不到key为" + key + "的子节点，且没有指定默认值");
            }
            return defaultValue.Value;
        }

        private static string GetValue(string key, bool valueRequired, string defaultValue)
        {
            string text = Config[key];
            if (text != null)
            {
                return text;
            }
            if (!valueRequired)
            {
                return defaultValue;
            }
            throw new ApplicationException("在配置文件的appSettings节点集合中找不到key为" + key + "的子节点");
        }
    }
}
