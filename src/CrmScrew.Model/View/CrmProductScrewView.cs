using Infrastructure.Common;

namespace CrmScrew.Model.View
{
    ///<summary>
    ///产品信息
    ///</summary>
    public class CrmProductScrewView
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("产品分类")]
        public int ProductType { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("产品名称")]
        public int ProductNameType { get; set; }

        /// <summary>
        /// Desc:规格
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("规格")]
        public string Specification { get; set; }

        /// <summary>
        /// Desc:材质
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("材质")]
        public int Material { get; set; }

        /// <summary>
        /// Desc:外观 表面处理
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("外观")]
        public int Exterior { get; set; }

        /// <summary>
        /// Desc:大小（单位MM）
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("大小")]
        public int Size { get; set; }

        /// <summary>
        /// Desc:厚度（单位MM）
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("厚度")]
        public int Thickness { get; set; }

        /// <summary>
        /// Desc:重量（单位KG）
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("重量")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Desc:包重量（单位KG）
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("包重量")]
        public decimal PackageWeight { get; set; }

        /// <summary>
        /// Desc:参考价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("参考价格")]
        public decimal ProposedPrice { get; set; }

        /// <summary>
        /// Desc:零售价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("零售价格")]
        public decimal RetailPrice { get; set; }

        /// <summary>
        /// Desc:进货价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("进货价格")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Desc:成本价格
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("成本价格")]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// Desc:图片地址
        /// Default:
        /// Nullable:False
        /// </summary>
        [TabaleColName("图片地址")]
        public string ImgUrl { get; set; }

    }
}
