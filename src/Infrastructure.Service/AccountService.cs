/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/18 14:04:14
** desc：    AccountService类
** Ver.:     V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using Infrastructure.Model.Entity.System;
using Infrastructure.Service.Base;
using SqlSugar;

namespace Infrastructure.Service
{
    public class AccountService : SqlSugarBase
    {
        #region 构造函数、单列

        private AccountService() { }

        public static readonly AccountService Instance = new Lazy<AccountService>(() => new AccountService()).Value;

        #endregion

        #region 菜单权限

        /// <summary>
        /// 根据角色获取菜单列表
        /// </summary>
        /// <param name="roleIds">包含0全部</param>
        /// <returns></returns>
        public List<SysMenuEntity> GetMenuListByRoleIds(IList<int> roleIds)
        {
            var menuList = GetInstance()
                .Queryable<SysMenuEntity, SysRoleMenuEntity>((m, rm) => new object[] { JoinType.Left, m.MenuId == rm.MenuId, })
                .Where((m, rm) => m.Status)
                .WhereIF(!roleIds.Contains(0), (m, rm) => roleIds.Contains(rm.RoleId))
                .Select((m, rm) => m)
                .ToList();
            return menuList;
        }

        #endregion
    }

}
