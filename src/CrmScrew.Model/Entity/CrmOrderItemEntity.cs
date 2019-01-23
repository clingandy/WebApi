using System;
using SqlSugar;

namespace CrmScrew.Model.Entity
{
    ///<summary>
    ///订单表
    ///</summary>
    [SugarTable("crm_order_item")]
    public class CrmOrderItemEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary> 
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "orderItemId")]
        public int OrderItemId { get; set; }

        /// <summary>
        /// Desc:订单ID
        /// Default:
        /// Nullable:False
        /// </summary> 
        [SugarColumn(ColumnName = "orderId")]
        public int OrderId {get;set;}

        /// <summary>
        /// Desc:商品名称
        /// Default:
        /// Nullable:False
        /// </summary> 
        [SugarColumn(ColumnName = "productFullName")]
        public string ProductFullName { get; set; }

        /// <summary>
        /// Desc:商品ID
        /// Default:
        /// Nullable:False
        /// </summary> 
        [SugarColumn(ColumnName = "productId")]
        public int ProductId { get; set; }


        /// <summary>
        /// Desc:订单数量
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "count")]
        public int Count {get;set;}

        /// <summary>
        /// Desc:订单重量（单位：KG）
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "weight")]
        public decimal Weight {get;set;}

        /// <summary>
        /// Desc:单价
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "singlePrice")]
        public decimal SinglePrice { get; set; }

        /// <summary>
        /// Desc:总价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "totalPrice")]
        public decimal TotalPrice {get;set;}

        /// <summary>
        /// Desc:商品地址
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "productUrl")]
        public string ProductUrl { get; set; }

        /// <summary>
        /// Desc:图片地址
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "imgUrl")]
        public string ImgUrl { get; set; }

    }
}
