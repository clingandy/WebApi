/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 17:40:16
** desc：    TimeSpanUtil类
** Ver.:     V1.0.0
*********************************************************************************/
using System;

namespace Infrastructure.Common
{
    public class TimeSpanUtil
    {
        public DateTime? StartTime;

        public DateTime? EndTime;

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public void End()
        {
            EndTime = DateTime.Now;
        }

        public long GetDuration()
        {
            if (!Check())
            {
                return 0L;
            }
            return (long)GetTimeSpan().TotalMilliseconds;
        }

        private bool Check()
        {
            return StartTime.HasValue && EndTime.HasValue;
        }

        private TimeSpan GetTimeSpan()
        {
            return EndTime.Value.Subtract(StartTime.Value);
        }
    }
}
