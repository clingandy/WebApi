/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/11 10:50:12
** desc：    CrmOrderSearch类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;

namespace CrmScrew.Model.Search
{
    public class CrmOrderSearch
    {
        public string ProductName { get; set; }
        public string ClientName { get; set; }

        public string Mobile { get; set; }

        /// <summary>
        /// 创建时间范围
        /// </summary>
        public List<DateTime> CreateDate { get; set; }

        public int? OrderStatus { get; set; }
    }
}
