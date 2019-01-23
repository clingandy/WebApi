using System;
using SqlSugar;

namespace CrmScrew.Model.Entity
{
    ///<summary>
    ///订单表
    ///</summary>
    [SugarTable("crm_order")]
    public class CrmOrderEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary> 
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "orderId")]
        public int OrderId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "clientId")]
        public int ClientId {get;set;}

        /// <summary>
        /// Desc:状态（1未付款 2已付款 3未发货 4已发货 5交易成功 6交易关闭）
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Desc:邮寄类型
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "shippingType")]
        public decimal ShippingType { get; set; }

        /// <summary>
        /// Desc:邮寄编码
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "shippingCode")]
        public string ShippingCode { get; set; }

        /// <summary>
        /// Desc:运费
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "freightPrice")]
        public decimal FreightPrice {get;set;}

        /// <summary>
        /// Desc:总价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "totlaPrice")]
        public decimal TotlaPrice {get;set;}

        /// <summary>
        /// Desc:收货地址
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "fullAddress")]
        public string FullAddress {get;set;}

        /// <summary>
        /// Desc:描述、备注
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "createTime")]
        public DateTime CreateTime {get;set;}

        /// <summary>
        /// Desc:支付时间
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "paymentTime")]
        public DateTime PaymentTime { get; set; }

        /// <summary>
        /// Desc:发货时间
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "consignTime")]
        public DateTime ConsignTime { get; set; }

        /// <summary>
        /// Desc:交易完成时间
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "endTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Desc:交易关闭时间
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "closeTime")]
        public DateTime CloseTime { get; set; }
    }
}
