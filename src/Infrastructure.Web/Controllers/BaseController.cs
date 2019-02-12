using System;
using System.Collections.Generic;
using System.IO;
using Infrastructure.Common;
using Infrastructure.Cryptogram;
using Infrastructure.Model.Entity.System;
using Infrastructure.Model.View.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        #region 登录用户信息

        protected string SetLoginToken(SysUserEntity user, List<int> roleIds)
        {
            var sessionUser = new LoginUserView
            {
                //OrgId = user.OrgId,
                UserName = user.UserName,
                //UserCode = user.UserCode,
                UserId = user.UserId,
                RoleIds = roleIds
            };
            //HttpContext.Session.SetString(LoginSessionKey, sessionUser.JsonSerialize());     //存入缓存

            //建议用redis存取加密密钥，和用户登录状态
            //DES加密用户信息给出Token
            return CryptogramHelper.DESEncrypt(sessionUser.JsonSerialize(), DateTime.Now.ToString("yyyyMMdd"));
        }

        protected LoginUserView GetLoginUser(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.ContainsKey("Token"))
            {
                string token = httpContext.Request.Headers["Token"];
                var userJson = CryptogramHelper.DESDecrypt(token, DateTime.Now.ToString("yyyyMMdd"));
                return userJson.JsonDeserialize<LoginUserView>();
            }
            return new LoginUserView();
        }


        protected LoginUserView GetLoginSession()
        {
            var userJsonStr = HttpContext.Session.GetString("login_user");     //获取缓存
            return userJsonStr == null ? new LoginUserView() : userJsonStr.JsonDeserialize<LoginUserView>();
        }

        protected void CleanLoginSession()
        {
            HttpContext.Session.Clear();     //清理缓存
        }

        #endregion

        #region 下载文件

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="saveName">下载后文件名称 带后缀</param>
        /// <param name="filePath">服务器相对路径 必须使用Path.Combine拼接，服务器不一定是windows</param>
        /// <param name="contentType">默认：二进制流，不知道下载文件类型</param>
        /// <returns></returns>
        protected ActionResult DownloadFile(string saveName, string filePath, string contentType = "application/octet-stream")
        {
            try
            {
                HttpContext.Response.Headers["Access-Control-Expose-Headers"] = "Content-Disposition";  //设置headers用于前端获取文件名

                var addrUrl = Path.Combine(IOUtils.GetBaseDirectory(), filePath);
                FileStream fs = new FileStream(addrUrl, FileMode.Open);
                return File(fs, contentType, saveName);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="saveName">下载后文件名称 带后缀</param>
        /// <param name="bytes">字节</param>
        /// <param name="contentType">默认：二进制流，不知道下载文件类型</param>
        /// <returns></returns>
        protected ActionResult DownloadFile(string saveName, byte[] bytes, string contentType = "application/octet-stream")
        {
            try
            {
                HttpContext.Response.Headers["Access-Control-Expose-Headers"] = "Content-Disposition";  //设置headers用于前端获取文件名
                return File(bytes, contentType, saveName);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        #endregion

    }

}
