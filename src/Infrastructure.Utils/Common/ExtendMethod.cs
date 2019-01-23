/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:15:14
** desc：    ExtendMethod类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Common
{
    public static class ExtendMethod
    {
        private static IEnumerable<Type> _SimpleType = new List<Type>
        {
            typeof(string),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(decimal),
            typeof(float),
            typeof(DateTime),
            typeof(bool),
            typeof(short?),
            typeof(int?),
            typeof(long?),
            typeof(decimal?),
            typeof(float?),
            typeof(DateTime?),
            typeof(bool?)
        }.AsEnumerable<Type>();

        public static string FormatWith(this string template, params object[] vals)
        {
            return string.Format(template, vals);
        }

        public static string ToHTMLColor(this string source, string color = "red")
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }
            return "<font style='color:{0}'>{1}</font>".FormatWith(new object[]
            {
                color,
                source
            });
        }

        public static string ToHTMLStrong(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }
            return "<strong>{0}</strong>".FormatWith(new object[]
            {
                source
            });
        }

        public static T CopyFrom<S, T>(this T target, S source, IList<string> ignoreProperties = null) where T : class
        {
            Type typeFromHandle = typeof(T);
            Type typeFromHandle2 = typeof(S);
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            PropertyInfo[] properties2 = typeFromHandle2.GetProperties();
            PropertyInfo[] array = properties2;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo sp = array[i];
                if (ignoreProperties == null || !ignoreProperties.Contains(sp.Name))
                {
                    PropertyInfo propertyInfo = properties.FirstOrDefault((PropertyInfo t) => t.Name == sp.Name);
                    if (!(propertyInfo == null) && !(propertyInfo.PropertyType != sp.PropertyType) && propertyInfo.CanWrite && ExtendMethod._SimpleType.Contains(propertyInfo.PropertyType))
                    {
                        propertyInfo.SetValue(target, sp.GetValue(source, null), null);
                    }
                }
            }
            return target;
        }
    }
}
