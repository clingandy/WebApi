/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/19 12:20:55
** desc：    AddOrderView类
** Ver.:     V1.0.0
*********************************************************************************/
using System.Collections.Generic;
using CrmScrew.Model.Entity;

namespace CrmScrew.Model.View
{
    public class AddOrderView
    {
        public CrmOrderEntity Order { get; set; }

        public List<CrmOrderItemEntity> OrderItems { get; set; }
    }

}
