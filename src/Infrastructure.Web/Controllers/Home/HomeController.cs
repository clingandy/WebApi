using System.Diagnostics;
using Infrastructure.Common;
using Infrastructure.Web.Filters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Web.Controllers.Home
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [NoPermission]
        public ActionResult<object> Index()
        {
            return true.ResponseSuccessFailure("站点已经启动");
        }

        [HttpGet]
        [NoPermission]
        public ActionResult<object> LoginWarn()
        {
            return false.ResponseNotLogin("请先登录后在操作...");
        }

        [HttpGet]
        [NoPermission]
        public ActionResult<object> ErrorWriteLog()
        {
            try
            {
                var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
                var error = feature?.Error;
                Logging.LogError($"【{Activity.Current?.Id ?? HttpContext.TraceIdentifier}】", error);
            }
            catch
            {
                // ignored
            }
            return false.ResponseSuccessFailure($"错误编号：{Activity.Current?.Id ?? HttpContext.TraceIdentifier}，请联系系统管理员查询具体原因。");
        }

        [HttpGet]
        [NoPermission]
        public ActionResult<object> ErrorPage()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            if (error != null)
            {
                return false.ResponseSuccessFailure($"{error.Message}\\{error.StackTrace}");
            }
            return false.ResponseSuccessFailure("发生未知的错误0x000001");
        }
    }
}
