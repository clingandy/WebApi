/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/18 14:04:14
** desc：    ClientService类
** Ver.:     V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using CrmScrew.Model.Entity;
using CrmScrew.Service.Base;
using Infrastructure.Common;
using SqlSugar;

namespace CrmScrew.Service
{
    public class ClientService : SqlSugarBase
    {
        #region 构造函数、单列

        private ClientService() { }

        public static readonly ClientService Instance = new Lazy<ClientService>(() => new ClientService()).Value;

        #endregion

        #region 客户管理

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mobile">mobile</param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<CrmClientEntity> FindClientPageList(string name, string mobile, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return GetInstance().Queryable<CrmClientEntity>()
                .WhereIF(!name.IsNullOrWhiteSpace(), t => t.ClientName.Contains(name) || t.QQ.Contains(name) || t.WeChat.Contains(name))
                .WhereIF(!mobile.IsNullOrWhiteSpace(), t => t.Mobile.Contains(mobile))
                .OrderBy(t=> t.ClientId, OrderByType.Desc)
                .ToPageList(pageIndex, pageSize, ref totalCount).ToList();
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Dictionary<int,string> FindClientNameIdByNameList(string name)
        {
            return GetInstance().Queryable<CrmClientEntity>()
                .Where(t=> t.ClientName.Contains(name))
                .Select(t=> new {t.ClientName, t.ClientId})
                .ToList()
                .ToDictionary(t=> t.ClientId, t=>t.ClientName);
        }

        #endregion
    }

}
