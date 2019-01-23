/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/3 16:45:30
** desc：    CrmProductScrewSearch类
** Ver.:     V1.0.0
*********************************************************************************/

namespace CrmScrew.Model.Search
{
    public class CrmProductScrewSearch
    {
        public int? ProductType { get; set; }

        public int? ProductNameType { get; set; }

        public string Specification { get; set; }

        /// <summary>
        /// 材质
        /// </summary>
        public int? Material { get; set; }

        /// <summary>
        /// 外观 表面处理
        /// </summary>
        public int? Exterior { get; set; }
    }
}
