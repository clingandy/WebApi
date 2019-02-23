/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/18 14:04:14
** desc：    OrderService类
** Ver.:     V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Search;
using CrmScrew.Model.View;
using CrmScrew.Service.Base;
using Infrastructure.Common;
using SqlSugar;

namespace CrmScrew.Service
{
    public class OrderService : SqlSugarBase
    {
        #region 构造函数、单列

        private OrderService() { }

        public static readonly OrderService Instance = new Lazy<OrderService>(() => new OrderService()).Value;

        #endregion

        #region 商品

        public IList<CrmProductScrewEntity> FindProductScrewPageList(CrmProductScrewSearch search, ref int totalCount, int pageIndex, int pageSize)
        {
            return GetInstance().Queryable<CrmProductScrewEntity>()
                .WhereIF(search.ProductType != null, t => t.ProductType == search.ProductType)
                .WhereIF(search.ProductNameType > -1, t => t.ProductNameType == search.ProductNameType)
                .WhereIF(search.Exterior != null, t => t.Exterior == search.Exterior)
                .WhereIF(search.Material != null, t => t.Material == search.Material)
                .WhereIF(!search.Specification.IsNullOrWhiteSpace(), t => t.Specification.Contains(search.Specification.Trim()))
                .OrderBy(t => t.ProductId, OrderByType.Desc)
                .ToPageList(pageIndex, pageSize, ref totalCount);
        }

        #endregion

        #region 订单

        /// <summary>
        /// 获取订单列表 包含OrderItem
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<CrmOrderView> FindOrderPageList(CrmOrderSearch search, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            //处理时间
            if (search.CreateDate?.Count > 0) search.CreateDate[0] = search.CreateDate[0].Date;
            if (search.CreateDate?.Count > 1) search.CreateDate[1] = search.CreateDate[1].AddDays(1).Date;

            var orderList = GetInstance().Queryable<CrmOrderEntity, CrmClientEntity, CrmOrderItemEntity>((t1, t2, t3) => new object[]
                {
                    JoinType.Left, t1.ClientId == t2.ClientId,
                    JoinType.Left, t1.OrderId == t3.OrderId
                })
                .WhereIF(!search.ClientName.IsNullOrWhiteSpace(), (t1, t2, t3) => t2.ClientName.Contains(search.ClientName))
                .WhereIF(!search.Mobile.IsNullOrWhiteSpace(), (t1, t2, t3) => t2.Mobile.Contains(search.Mobile))
                .WhereIF(!search.ProductName.IsNullOrWhiteSpace(), (t1, t2, t3) => t3.ProductFullName.Contains(search.ProductName))
                .WhereIF(search.CreateDate?.Count > 0, (t1, t2, t3) => t1.CreateTime >= search.CreateDate[0])
                .WhereIF(search.CreateDate?.Count > 1, (t1, t2, t3) => t1.CreateTime < search.CreateDate[1])
                .WhereIF(search.OrderStatus != null, (t1, t2, t3) => t1.Status == search.OrderStatus)
                .GroupBy((t1, t2, t3) => t1.OrderId)
                .OrderBy((t1, t2, t3) => t1.OrderId, OrderByType.Desc)
                .Select((t1, t2, t3) => new CrmOrderView { ClientName = t2.ClientName, Order = t1 })
                .ToPageList(pageIndex, pageSize, ref totalCount);

            if (orderList.Count > 0)
            {
                var orderIds = orderList.Select(t => t.Order.OrderId).ToList();
                var orderItemList = GetInstance().Queryable<CrmOrderItemEntity>().Where(t => orderIds.Contains(t.OrderId)).ToList();
                foreach (var order in orderList)
                {
                    order.OrderItem = orderItemList.Where(t => t.OrderId == order.Order.OrderId).ToList();
                }
            }
            return orderList;
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool AddCrmOrder(CrmOrderEntity order, List<CrmOrderItemEntity> items)
        {
            var db = GetInstance();
            var r = db.Ado.UseTran(() =>
            {
                var orderId = db.Insertable(order).ExecuteReturnIdentity();
                if (orderId > 0)
                {
                    foreach (var item in items)
                    {
                        item.OrderId = orderId;
                    }
                    db.Insertable(items).ExecuteCommand();   //添加订单详情
                }
                return orderId > 0;
            });

            return r.IsSuccess;
        }

        #endregion
    }

}
