/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/27 18:37:50
** desc：    IClientRepository类
** Ver.:     V1.0.0
*********************************************************************************/
using System.Collections.Generic;
using CrmScrew.Model.Entity;

namespace CrmScrew.Domain
{
    public interface IClientRepository
    {
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
        IList<CrmClientEntity> GetClientPageList(string name, string mobile, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 获取客户
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        CrmClientEntity GetClient(int clientId);

        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddClient(CrmClientEntity model);

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifyClient(CrmClientEntity model);

        #endregion

        #region 客户地址

        /// <summary>
        /// 查询客户地址
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        IList<CrmClientAddressEntity> GetClientAddressList(int clientId);

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Dictionary<int, string> GetClientNameIdByNameList(string name);

        /// <summary>
        /// 添加客户地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddClientAddress(CrmClientAddressEntity model);

        /// <summary>
        /// 修改客户地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifyClientAddress(CrmClientAddressEntity model);

        /// <summary>
        /// 删除客户地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DelClientAddress(int id);

        #endregion
    }
}
