using System.Collections.Generic;
using System.Linq;
using CrmScrew.Domain;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Search;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Web.Controllers.ScrewManage
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DictController : ControllerBase
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private string _allDictKey = "dictcontroller_getcrmdiclist";

        private readonly IDictRepository _repository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public DictController(IDictRepository repository, IMemoryCache cache)
        {
            _cache = cache;
            _repository = repository;
        }

        #endregion

        #region 基本操作

        /// <summary>
        /// 查询字典信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<CrmDictEntity>>))]
        public ActionResult<object> GetDictPageList([FromQuery]CrmDictSearch search, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var data = _repository.GetDictPageList(search, ref totalCount, pageIndex, pageSize);
            return data.ResponseSuccess("", totalCount);
        }

        /// <summary>
        /// 查询字典全部信息  状态为1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<KeyValuePair<int, string>>>))]
        public ActionResult<object> GetDictByTypeList(int dictType, int? pDictKey)
        {
            var crmDictList = _cache.GetOrCreate(_allDictKey, (entry) => _repository.GetDicList());
            var query = crmDictList.Where(t => t.DictType == dictType);
            if (pDictKey != null)
            {
                query = query.Where(t => t.PDictKey == pDictKey);
            }
            return query.OrderBy(t => t.Sort).Select(t => new { key = t.DictKey, value = t.DictValue }).ResponseSuccess();
        }

        /// <summary>
        /// 获取字典类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<KeyValuePair<int, string>>>))]
        public ActionResult<object> GetDictType()
        {
            return _repository.GetDictType().Select(t=> new {key= t.Key, value = t.Value}).ResponseSuccess();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddDict([FromForm]CrmDictEntity dict)
        {
            var isok = _repository.AddDict(dict);
            if (isok)
            {
                _cache.Remove(_allDictKey);
            }
            else
            {
                return false.ResponseDataError("添加的所属分类+key值已经存在");
            }
            return true.ResponseSuccessFailure();
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifyDict([FromForm]CrmDictEntity dict)
        {
            var isok = _repository.ModifyDict(dict);
            if (isok)
            {
                _cache.Remove(_allDictKey);
            }
            return isok.ResponseSuccessFailure();
        }

        #endregion
    }
}
