using System;
using System.Linq;
using Infrastructure.Common;
using Infrastructure.Cryptogram;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Web.Filters
{
    /// <summary>
    /// 登录特性
    /// <para>包含整个验证得流程</para>
    /// </summary>
    public class PermissionAttribute : ActionFilterAttribute
    {
        private readonly IMemoryCache _cache;

        public PermissionAttribute(IMemoryCache cache)
        {
            _cache = cache;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                if (controllerActionDescriptor.MethodInfo.GetCustomAttributes(true).Any(a => a is NoPermissionAttribute))
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }
            }

            //判断token是否存在
            if (filterContext.HttpContext.Request.Headers.ContainsKey("Token"))
            {
                string token = filterContext.HttpContext.Request.Headers["Token"];
                //var userJson = CryptogramHelper.DESDecrypt(token, DateTime.Now.ToString("yyyyMMdd"));
                var tokenCache = _cache.Get<string>(token.GetHashCode());
                if (tokenCache.IsNullOrWhiteSpace() || token != tokenCache)
                {
                    filterContext.Result = new RedirectResult("/Home/LoginWarn");
                }
                else
                {
                    _cache.Set(token.GetHashCode(), token, DateTimeOffset.Now.AddHours(1));
                }
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }

    /// <summary>
    /// 不登录特性
    /// </summary>
    public class NoPermissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

    }
}
