using SqlSugar;

namespace CrmScrew.Model.Entity
{
    ///<summary>
    ///图片管理
    ///</summary>
    [SugarTable("crm_product_screw_img")]
    public class CrmProductScrewImgEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "productScrewImgId")]
        public int ProductScrewImgId { get;set;}

        /// <summary>
        /// Desc:产品名称类型
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "productNameType")]
        public int ProductNameType { get;set;}

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "imgUrl")]
        public string ImgUrl { get;set;}

    }
}
