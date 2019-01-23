/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/3 11:40:34
** desc：    CrmOrderStatus类
** Ver.:     V1.0.0
*********************************************************************************/
using System.ComponentModel;

namespace CrmScrew.Model.Enum
{
    public enum CrmOrderStatusEnum
    {
        [Description("未付款")]
        Unpaid = 1,
        [Description("已付款")]
        Paid,
        [Description("未发货")]
        NotShipped,
        [Description("已发货")]
        Shipped,
        [Description("交易成功")]
        Success,
        [Description("交易关闭")]
        Close
    }
}
