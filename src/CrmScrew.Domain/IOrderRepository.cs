/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/27 18:38:08
** desc：    IOrderRepository类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Search;
using CrmScrew.Model.View;

namespace CrmScrew.Domain
{
    public interface IOrderRepository
    {
        #region 商品

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<CrmProductScrewEntity> GetProductScrewPageList(CrmProductScrewSearch search, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 批量添加商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool AddBatchProductScrew(List<CrmProductScrewEntity> data);

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddProductScrew(CrmProductScrewEntity model);

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifyProductScrew(CrmProductScrewEntity model);

        /// <summary>
        /// 查询商品历史信息
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<CrmProductScrewHistoryEntity> GetProductScrewHistoryPageList(int productId, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        #endregion

        #region 商品图片

        /// <summary>
        /// 返回全部商品图片
        /// </summary>
        /// <returns></returns>
        IList<CrmProductScrewImgEntity> GetProductScrewImgAll();


        /// <summary>
        /// 添加商品图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddProductScrewImg(CrmProductScrewImgEntity model);

        /// <summary>
        /// 修改商品图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifyProductScrewImg(CrmProductScrewImgEntity model);

        #endregion

        #region 订单

        /// <summary>
        /// 查询订单 包含OrderItem
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<CrmOrderView> GetOrderPageList(CrmOrderSearch search, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> GetOrderStatus();

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifyCrmOrder(CrmOrderEntity model);

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        CrmOrderEntity GetCrmOrder(int orderId);

        /// <summary>
        /// 修改订单item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifyCrmOrderItem(CrmOrderItemEntity model);

        /// <summary>
        /// 获取订单item
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        List<CrmOrderItemEntity> GetCrmOrderItemByOrderId(int orderId);

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        bool AddCrmOrder(AddOrderView view);

        #endregion
    }
}
