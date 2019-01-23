/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/27 18:38:36
** desc：    ClientRepository类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using CrmScrew.Domain;
using CrmScrew.Model.Entity;
using CrmScrew.Service;

namespace CrmScrew.Core
{
    public class ClientRepository : IClientRepository
    {
        #region 属性

        private readonly ClientService _service = ClientService.Instance;

        #endregion

        #region 客户

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<CrmClientEntity> GetClientPageList(string name, string mobile, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return _service.FindClientPageList(name?.Trim(), mobile?.Trim(), ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetClientNameIdByNameList(string name)
        {
            return _service.FindClientNameIdByNameList(name);
        }

        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddClient(CrmClientEntity model)
        {
            model.ModifyTime = DateTime.Now;
            return _service.Insert(model);
        }

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyClient(CrmClientEntity model)
        {
            model.ModifyTime = DateTime.Now;
            return _service.Update(model);
        }

        #endregion

        #region 客户地址

        /// <summary>
        /// 查询客户地址
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public IList<CrmClientAddressEntity> GetClientAddressList(int clientId)
        {
            return _service.FindByFunc<CrmClientAddressEntity>(t => t.ClientId == clientId);
        }

        /// <summary>
        /// 添加客户地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddClientAddress(CrmClientAddressEntity model)
        {
            return _service.Insert(model);
        }

        /// <summary>
        /// 修改客户地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyClientAddress(CrmClientAddressEntity model)
        {
            return _service.Update(model);
        }

        /// <summary>
        /// 删除客户地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelClientAddress(int id)
        {
            return _service.Delete<CrmClientAddressEntity>(id);
        }

        #endregion
    }
}
