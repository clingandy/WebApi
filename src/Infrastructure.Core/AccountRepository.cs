/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/19 10:49:27
** desc：    AccountRepository类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Domain;
using Infrastructure.Model.Entity.System;
using Infrastructure.Model.View.Account;
using Infrastructure.Service;

namespace Infrastructure.Core
{
    public class AccountRepository :IAccountRepository
    {
        #region 属性

        private readonly AccountService _service = AccountService.Instance;

        #endregion

        #region 获取登录用户菜单列表

        /// <summary>
        /// 获取登录用户菜单列表
        /// </summary>
        /// <param name="roleIds">包含0全部</param>
        /// <returns></returns>
        public IList<MenuRouterView> GetMenuRouterPermission(IList<int> roleIds)
        {
            var result = new List<MenuRouterView>();
            var menuList = _service.GetMenuListByRoleIds(roleIds).OrderBy(t => t.Sort).ToList();
            foreach (var menu in menuList)
            {
                result.Add(new MenuRouterView
                {
                    VueFile = menu.VueFile,
                    Icon = menu.MenuIcon,
                    IsShow = menu.IsShow,
                    MenuId = menu.MenuId,
                    PMenuId = menu.PMenuId,
                    RouterName = menu.RouterName,
                    Title = menu.MenuName,
                    VueUrl = menu.VueUrl,
                    PermissionList = null
                });
            }
            var menuIdsList = menuList.Select(t => t.MenuId).ToList();
            if (menuIdsList.Count > 0)
            {
                IList<SysPermissionEntity> permissionList;
                //获取按钮权限
                if (roleIds.Contains(0))
                {
                    permissionList = _service.FindByFunc<SysPermissionEntity>(t => menuIdsList.Contains(t.MenuId));
                }
                else
                {
                    var permIds = _service.FindByFunc<SysRolePermissionEntity>(t => roleIds.Contains(t.RoleId), "permission_id").Select(t=> t.PermissionId);
                    permissionList = _service.FindByFunc<SysPermissionEntity>(t => permIds.Contains(t.PermissionId));
                }
                var tempIds = permissionList.Select(t => t.MenuId).ToList();
                foreach (var item in result)
                {
                    if (tempIds.Contains(item.MenuId))
                    {
                        item.PermissionList = permissionList.Where(t => t.MenuId == item.MenuId).ToDictionary(t => t.PermissionCode, tt => tt.Status);
                    }
                }

            }
            return result;
        }

        #endregion

        #region 获取登录用户相关信息

        /// <summary>
        /// 根据name获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SysUserEntity GetSysUserByName(string name)
        {
            return _service.FindFirstByFunc<SysUserEntity>(t => t.UserName == name);
        }

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysUserEntity GetSysUserById(int id)
        {
            return _service.FindFirstByFunc<SysUserEntity>(t => t.UserId == id);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifySysUser(SysUserEntity model)
        {
            return _service.Update(model);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifySysUserAsync(SysUserEntity model)
        {
            return _service.UpdateAsync(model);
        }

        #endregion
    }
}
