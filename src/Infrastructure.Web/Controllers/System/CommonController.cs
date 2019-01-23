using System.Collections.Generic;
using System.Linq;
using Infrastructure.Common;
using Infrastructure.Domain;
using Infrastructure.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Web.Controllers.System
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        #region 构造

        private IMemoryCache _cache;
        private readonly ICommonRepository _repository;

        public CommonController(ICommonRepository repository, IMemoryCache cache)
        {
            _cache = cache;
            _repository = repository;
        }

        #endregion

        #region 省市

        /// <summary>
        /// 根据父级获取省市地区
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet]
        [NoPermission]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<KeyValuePair<int, string>>>))]
        public ActionResult<object> GetCitys(int? pid)
        {
            var citys = _cache.GetOrCreate("commoncontroller_getcitys", (entity) =>  _repository.GetChinaCityList() );
            pid = pid ?? 0;
            var data = citys.Where(t => t.PId == pid).OrderBy(t => t.Id).Select(t => new { key = t.Id, value = t.Name });
            return data.ResponseSuccess();
        }

        #endregion

    }
}
