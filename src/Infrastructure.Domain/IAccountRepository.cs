/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/19 10:21:02
** desc：    IAccountRepository类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Model.Entity.System;
using Infrastructure.Model.View.Account;

namespace Infrastructure.Domain
{
    public interface IAccountRepository
    {
        #region 菜单权限

        /// <summary>
        /// 获取登录用户菜单列表
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        IList<MenuRouterView> GetMenuRouterPermission(IList<int> roleIds);

        #endregion

        #region 获取登录用户相关信息

        /// <summary>
        /// 根据name获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SysUserEntity GetSysUserByName(string name);

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysUserEntity GetSysUserById(int id);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifySysUser(SysUserEntity model);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> ModifySysUserAsync(SysUserEntity model);

        #endregion
    }
}
