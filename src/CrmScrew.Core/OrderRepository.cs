/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/27 18:38:50
** desc：    OrderRepository类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using CrmScrew.Domain;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Enum;
using CrmScrew.Model.Search;
using CrmScrew.Model.View;
using CrmScrew.Service;
using Infrastructure.Common;

namespace CrmScrew.Core
{
    public class OrderRepository : IOrderRepository
    {
        #region 属性

        private readonly OrderService _service = OrderService.Instance;

        #endregion

        #region 商品

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<CrmProductScrewEntity> GetProductScrewPageList(CrmProductScrewSearch search, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return _service.FindProductScrewPageList(search, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 批量添加商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddBatchProductScrew(List<CrmProductScrewEntity> data)
        {
            if (data == null || data.Count == 0) return false;
            var isok = _service.InsertBatch(data);
            if (isok)
            {
                //添加历史
                _service.ExecuteCommand(
                    @"insert into crm_product_screw_history 
select 0 pHistoryId, a.* from crm_product_screw a
where (SELECT count(1) AS num FROM crm_product_screw_history b WHERE b.productId = a.productId) = 0");
            }
            return true;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddProductScrew(CrmProductScrewEntity model)
        {
            model.ModifyTime = DateTime.Now;
            var id = _service.InsertReturnId(model);
            if (id > 0)
            {
                //添加历史
                model.ProductId = id;
                _service.InsertAsync(new CrmProductScrewHistoryEntity().CopyFrom(model));
            }
            return id > 0;
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyProductScrew(CrmProductScrewEntity model)
        {
            model.ModifyTime = DateTime.Now;
            var isOk = _service.Update(model);
            if (isOk)
            {
                //添加历史
                _service.InsertAsync(new CrmProductScrewHistoryEntity().CopyFrom(model));
            }
            return isOk;
        }

        /// <summary>
        /// 查询商品历史信息
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<CrmProductScrewHistoryEntity> GetProductScrewHistoryPageList(int productId, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return _service.FindByPage<CrmProductScrewHistoryEntity>(t=> t.ProductId == productId, "pHistoryId desc", ref totalCount, pageIndex, pageSize);
        }

        #endregion

        #region 商品图片

        /// <summary>
        /// 返回全部商品图片
        /// </summary>
        /// <returns></returns>
        public IList<CrmProductScrewImgEntity> GetProductScrewImgAll()
        {
            return _service.FindByFunc<CrmProductScrewImgEntity>(t => true);
        }


        /// <summary>
        /// 添加商品图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddProductScrewImg(CrmProductScrewImgEntity model)
        {
            if (model.ImgUrl.IsNullOrWhiteSpace()) return false;
            var isOk = _service.Insert(model);
            if (isOk)
            {
                _service.UpdateAsync<CrmProductScrewEntity>(new { imgUrl= model.ImgUrl }, t=> t.ProductNameType == model.ProductNameType);
            }
            return isOk;
        }

        /// <summary>
        /// 修改商品图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyProductScrewImg(CrmProductScrewImgEntity model)
        {
            if (model.ImgUrl.IsNullOrWhiteSpace()) return false;
            var isOk = _service.Update(model);
            if (isOk)
            {
                _service.UpdateAsync<CrmProductScrewEntity>(new { imgUrl = model.ImgUrl }, t => t.ProductNameType == model.ProductNameType);
            }
            return isOk;
        }

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
        public IList<CrmOrderView> GetOrderPageList(CrmOrderSearch search, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return _service.FindOrderPageList(search, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetOrderStatus()
        {
            return typeof(CrmOrderStatusEnum).ToDictionary();
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyCrmOrder(CrmOrderEntity model)
        {
            switch (model.Status)
            {
                case (int)CrmOrderStatusEnum.Paid:
                    model.PaymentTime = DateTime.Now;
                    break;
                case (int)CrmOrderStatusEnum.Shipped:
                    model.ConsignTime = DateTime.Now;
                    break;
                case (int)CrmOrderStatusEnum.Success:
                    model.EndTime = DateTime.Now;
                    break;
                default:
                    model.CloseTime = DateTime.Now;
                    break;
            }
            return _service.Update(model);
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CrmOrderEntity GetCrmOrder(int id)
        {
            return _service.FindById<CrmOrderEntity>(id);
        }

        /// <summary>
        /// 修改订单item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyCrmOrderItem(CrmOrderItemEntity model)
        {
            return _service.Update(model);
        }

        /// <summary>
        /// 获取订单item
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<CrmOrderItemEntity> GetCrmOrderItemByOrderId(int orderId)
        {
            return _service.FindByFunc<CrmOrderItemEntity>(t=> t.OrderId == orderId).ToList();
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool AddCrmOrder(AddOrderView view)
        {
            if (view?.Order == null || view.OrderItems?.Count == 0) return false;
            view.Order.CreateTime = DateTime.Now;
            view.Order.CloseTime = DateTime.Now;
            view.Order.ConsignTime = DateTime.Now;
            view.Order.PaymentTime = DateTime.Now;
            //order.Status = (int)CrmOrderStatusEnum.Success;
            return _service.AddCrmOrder(view.Order, view.OrderItems); ;
        }

        #endregion
    }
}
