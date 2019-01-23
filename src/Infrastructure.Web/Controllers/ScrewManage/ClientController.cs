using System.Collections.Generic;
using System.Linq;
using CrmScrew.Domain;
using CrmScrew.Model.Entity;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Web.Controllers.ScrewManage
{
    /// <summary>
    /// 客户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region 构造

        private readonly IClientRepository _repository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="repository"></param>
        public ClientController(IClientRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region 客户

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<CrmClientEntity>>))]
        public ActionResult<object> GetClientPageList(string name, string mobile, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var data = _repository.GetClientPageList(name, mobile, ref totalCount, pageIndex, pageSize);
            return data.ResponseSuccess("", totalCount);
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<KeyValuePair<int,string>>>))]
        public ActionResult<object> GetClientNameIdByNameList(string name)
        {
            return _repository.GetClientNameIdByNameList(name).Select(t=> new {key = t.Key, value = t.Value}).ResponseSuccess();
        }

        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddClient([FromForm]CrmClientEntity model)
        {
            return _repository.AddClient(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifyClient([FromForm]CrmClientEntity model)
        {
            return _repository.ModifyClient(model).ResponseSuccessFailure();
        }

        #endregion

        #region 客户地址

        /// <summary>
        /// 查询客户地址
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<CrmClientAddressEntity>>))]
        public ActionResult<object> GetClientAddressList(int clientId)
        {
            return _repository.GetClientAddressList(clientId).ResponseSuccess();
        }

        /// <summary>
        /// 添加客户地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddClientAddress([FromForm]CrmClientAddressEntity model)
        {
            return _repository.AddClientAddress(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 修改客户地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifyClientAddress([FromForm]CrmClientAddressEntity model)
        {
            return _repository.ModifyClientAddress(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 删除客户地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> DelClientAddress(int id)
        {
            return _repository.DelClientAddress(id).ResponseSuccessFailure();
        }

        #endregion
    }
}
