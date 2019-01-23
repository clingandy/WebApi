using System;
using SqlSugar;

namespace CrmScrew.Model.Entity
{
    ///<summary>
    ///产品信息
    ///</summary>
    [SugarTable("crm_product_screw")]
    public class CrmProductScrewEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "productId")]
        public int ProductId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "productType")]
        public int ProductType { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "productNameType")]
        public int ProductNameType { get; set; }

        /// <summary>
        /// Desc:规格
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "specification")]
        public string Specification { get; set; }

        /// <summary>
        /// Desc:材质
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "material")]
        public int Material { get; set; }

        /// <summary>
        /// Desc:外观 表面处理
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "exterior")]
        public int Exterior { get; set; }

        /// <summary>
        /// Desc:大小（单位MM）
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "size")]
        public int Size { get; set; }

        /// <summary>
        /// Desc:厚度（单位MM）
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "thickness")]
        public int Thickness { get; set; }

        /// <summary>
        /// Desc:重量（单位KG）
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "weight")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Desc:包重量（单位KG）
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "packageWeight")]
        public decimal PackageWeight { get; set; }

        /// <summary>
        /// Desc:参考价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "proposedPrice")]
        public decimal ProposedPrice { get; set; }

        /// <summary>
        /// Desc:零售价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "retailPrice")]
        public decimal RetailPrice { get; set; }

        /// <summary>
        /// Desc:进货价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "purchasePrice")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Desc:成本价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "costPrice")]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "modifyTime")]
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// Desc:图片地址
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "imgUrl")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// Desc:状态（1上架 2下架）
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public int Status { get; set; }

    }
}
