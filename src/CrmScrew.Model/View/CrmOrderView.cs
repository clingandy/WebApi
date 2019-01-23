/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/11 10:57:35
** desc：    CrmOrderView类
** Ver.:     V1.0.0
*********************************************************************************/
using System.Collections.Generic;
using CrmScrew.Model.Entity;

namespace CrmScrew.Model.View
{
    public class CrmOrderView
    {
        public string ClientName { get; set; }

        public CrmOrderEntity Order { get; set; }

        public List<CrmOrderItemEntity> OrderItem { get; set; }
    }
}
