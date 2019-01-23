/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 16:58:25
** desc：    StringDurationArrayConverter类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Infrastructure.Common
{
    public class StringDurationArrayConverter : TypeConverter
    {
        private static readonly Regex Expression = new Regex("(?<duration>\\d+)(?<type>ms|[smhd])(\\*(?<repeat>\\d+))*", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value != null && value.GetType() != typeof(string))
            {
                return new[]
                {
                    TimeSpan.FromMinutes(30.0)
                };
            }
            string[] array = ((string)value)?.Split(new char[]
            {
                ';',
                ','
            }, StringSplitOptions.RemoveEmptyEntries);
            List<TimeSpan> list = new List<TimeSpan>();
            if (array == null)
            {
                return list.ToArray();
            }
            foreach (var t in array)
            {
                Match match = Expression.Match(t);
                int num = 0;
                int num2;
                if (!match.Success || !int.TryParse(match.Groups["duration"].Value, out num2))
                {
                    throw new ArgumentException($"The application configuration contains a 'durationToIgnoreOnFailure' with invalid items '{value}'.");
                }
                string value2 = match.Groups["repeat"].Value;
                if (!string.IsNullOrEmpty(value2))
                {
                    if (!int.TryParse(value2, out num))
                    {
                        throw new ArgumentException($"The application configuration contains a 'durationToIgnoreOnFailure' with invalid items '{value}'.");
                    }
                }
                else
                {
                    num = 1;
                }
                TimeSpan item = default(TimeSpan);
                string a;
                if ((a = match.Groups["type"].Value.ToLower()) != null)
                {
                    switch (a)
                    {
                        case "ms":
                            item = TimeSpan.FromMilliseconds(num2);
                            break;
                        case "s":
                            item = TimeSpan.FromSeconds(num2);
                            break;
                        case "m":
                            item = TimeSpan.FromMinutes(num2);
                            break;
                        case "h":
                            item = TimeSpan.FromHours(num2);
                            break;
                        case "d":
                            item = TimeSpan.FromDays(num2);
                            break;
                    }
                }
                for (int j = 0; j < num; j++)
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}
