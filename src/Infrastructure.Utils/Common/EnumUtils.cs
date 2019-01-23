/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:13:33
** desc：    EnumUtils类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Reflection;

namespace Infrastructure.Common
{
    public static class EnumUtils
    {
        public static T ToEnum<T>(this int num)
        {
            IDictionary<int, T> dictionary = new Dictionary<int, T>();
            Type typeFromHandle = typeof(T);
            string[] names = Enum.GetNames(typeFromHandle);
            for (int i = 0; i < names.Length; i++)
            {
                string value = names[i];
                int num2 = int.Parse(Enum.Format(typeFromHandle, Enum.Parse(typeFromHandle, value), "d"));
                object obj = Enum.Parse(typeFromHandle, value);
                if (!dictionary.Keys.Contains(num2))
                {
                    dictionary.Add(num2, (T)((object)obj));
                }
            }
            return dictionary[num];
        }

        public static T ToEnum<T>(this string str)
        {
            int key;
            int.TryParse(str, out key);
            IDictionary<int, T> dictionary = new Dictionary<int, T>();
            Type typeFromHandle = typeof(T);
            string[] names = Enum.GetNames(typeFromHandle);
            for (int i = 0; i < names.Length; i++)
            {
                string value = names[i];
                int num = int.Parse(Enum.Format(typeFromHandle, Enum.Parse(typeFromHandle, value), "d"));
                object obj = Enum.Parse(typeFromHandle, value);
                if (!dictionary.Keys.Contains(num))
                {
                    dictionary.Add(num, (T)((object)obj));
                }
            }
            return dictionary[key];
        }

        public static Dictionary<int, string> ToDictionary(this Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("参数必须是枚举类型！", "enumType");
            }
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (Enum item in Enum.GetValues(enumType))
            {
                int key = Convert.ToInt32(item);
                string value = GetEnumDes(item);
                dic[key] = value;
            }
            return dic;
        }

        public static int GetEnumInt(this Enum t)
        {
            return Convert.ToInt32(t);
        }

        public static string GetEnumDes(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] member = type.GetMember(en.ToString());
            if (member != null && member.Length > 0)
            {
                object[] customAttributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0)
                {
                    return ((DescriptionAttribute)customAttributes[0]).Description;
                }
            }
            return en.ToString();
        }

        public static bool CheckForEnum<T>(this object n)
        {
            return n == null || Enum.IsDefined(typeof(T), n.ToString().ToLower());
        }


    }
}
