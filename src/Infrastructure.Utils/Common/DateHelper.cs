/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:06:06
** desc：    DateHelper类
** Ver.:     V1.0.0
*********************************************************************************/
using System;

namespace Infrastructure.Common
{
    public static class DateHelper
    {
        private static DateTime _minDate = Convert.ToDateTime("1900-01-01");

        private static DateTime _maxDate = Convert.ToDateTime("2999-01-01");

        public static long ConvertDateTimeInt()
        {
            DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(MinDate());
            DateTime now = DateTime.Now;
            return (long)Math.Round((now - d).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }

        public static DateTime MinDate()
        {
            return _minDate;
        }

        public static DateTime MaxDate()
        {
            return _maxDate;
        }

        public static bool CompareMinDate(this DateTime inPutDateTime)
        {
            return inPutDateTime.Equals(MinDate());
        }

        public static DateTime TryParse(this string dateFormatStr)
        {
            DateTime now = DateTime.Now;
            if (DateTime.TryParse(dateFormatStr, out now))
            {
                return now;
            }
            return MinDate();
        }

        public static string GetDateString(this DateTime? time)
        {
            if (!time.HasValue)
            {
                return string.Empty;
            }
            return time.Value.ToString("yyyy-MM-dd");
        }

        public static string GetDateTimeString(this DateTime? time)
        {
            if (!time.HasValue)
            {
                return string.Empty;
            }
            return time.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTimeShortString(this DateTime? time)
        {
            if (!time.HasValue)
            {
                return string.Empty;
            }
            return time.Value.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
