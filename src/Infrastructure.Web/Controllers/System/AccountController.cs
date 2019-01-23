using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Common;
using Infrastructure.Cryptogram;
using Infrastructure.Domain;
using Infrastructure.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Web.Controllers.System
{
    /// <summary>
    /// 账户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly IAccountRepository _repository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public AccountController(IAccountRepository repository, IMemoryCache cache)
        {
            _cache = cache;
            _repository = repository;
        }

        #endregion

        #region 登录权限

        [HttpPost]
        [NoPermission]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public ActionResult<object> Login(string username, string password)
        {
            if (username.IsNullOrWhiteSpace() || password.IsNullOrWhiteSpace())
            {
                return false.ResponseDataError("参数为空");
            }
            var user = _repository.GetSysUserByName(username);
            if (user == null)
            {
                return false.ResponseDataError("账号或密码错误");
            }
            if (CryptogramHelper.GetMd5Hash(password) != user.UserPwd)
            {
                if (user.IsLock && user.ModifyTime.Date == DateTime.Now.Date)
                {
                    return false.ResponseUnknown("账户错误次数过多，请明天在尝试");
                }
                if (user.ModifyTime.Date != DateTime.Now.Date)
                {
                    user.ErrorCount = 0;
                    user.IsLock = false;
                }
                user.ErrorCount += 1;
                user.ModifyTime = DateTime.Now;     //修改时间
                if (user.ErrorCount >= 5)
                {
                    user.IsLock = true;
                }
                _repository.ModifySysUserAsync(user); //异步更新数据
                return false.ResponseDataError("账号或密码错误");
            }

            var roleIds = new List<int>{user.RoleId};
            var model = new
            {
                token = SetLoginToken(user, roleIds),
                name = user.UserName,
                avatar = AppSettingsHelper.GetString("LoginUserAvatarUrl", "#"),
                introduction = user.UserCode,
                roles = roleIds.GetStringJoin(",")
            };
            user.ErrorCount = 0;
            user.IsLock = false;
            user.ModifyTime = DateTime.Now;     //修改时间
            _repository.ModifySysUserAsync(user); //异步更新数据
            _cache.Set(model.token.GetHashCode(), model.token, DateTimeOffset.Now.AddHours(1)); //缓存数据1小时
            return model.ResponseSuccess();
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifySysUserByPwd(string userPwd, string newPwd)
        {
            var userLogin = GetLoginUser(HttpContext);
            var user = _repository.GetSysUserById(userLogin.UserId);
            if (CryptogramHelper.GetMd5Hash(userPwd) == user.UserPwd)
            {
                user.UserPwd = CryptogramHelper.GetMd5Hash(newPwd);
                return _repository.ModifySysUser(user).ResponseSuccessFailure();
            }
            return false.ResponseDataError("旧密码错误");
        }

        [HttpPost]
        [NoPermission]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> LogOut()
        {
            if (HttpContext.Request.Headers.ContainsKey("Token"))
            {
                string token = HttpContext.Request.Headers["Token"];
                _cache.Remove(token);   //清空
            }
            return true.ResponseSuccessFailure();
        }


        /// <summary>
        /// 获取用户权限菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public ActionResult<object> GetMenuRouterPermission()
        {
            var user = GetLoginUser(HttpContext);
            if (user.UserName == "admin")
            {
                user.RoleIds = new List<int> { 0 };
            }
            if (user.RoleIds == null || user.RoleIds.Count == 0)
            {
                return new List<object>().ResponseSuccess();
            }
            var menuRouterList = _repository.GetMenuRouterPermission(user.RoleIds);

            var rootList = menuRouterList.Where(t => t.PMenuId == 0).ToList();
            var resultList = rootList.Select(t => new
            {
                Path = t.VueFile,
                Redirect = t.VueUrl,
                Hidden = !t.IsShow,
                Meta = new { t.Title, t.Icon, Roles = "", Permission = "" },
                Children = menuRouterList.Where(tt => tt.PMenuId == t.MenuId).Select(pt => new
                {
                    Component = pt.VueUrl,
                    Path = pt.VueFile,
                    name = pt.RouterName,
                    Hidden = !pt.IsShow,
                    Meta = new { pt.Title, pt.Icon, Permission = pt.PermissionList, Roles = "" },
                })
            }).ToList();
            return resultList.ResponseSuccess();
        }

        #endregion

    }
}
