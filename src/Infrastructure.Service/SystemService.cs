/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/18 12:29:23
** desc：    SystemService类
** Ver.:     V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Common;
using Infrastructure.Model.Entity.System;
using Infrastructure.Service.Base;

namespace Infrastructure.Service
{
    public class SystemService : SqlSugarBase
    {
        #region 构造函数、单列

        private SystemService() { }

        public static readonly SystemService Instance = new Lazy<SystemService>(() => new SystemService()).Value;

        #endregion

        #region 角色

        /// <summary>
        /// 获取全部角色分页
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="orgId"></param>
        /// <param name="orderFileds"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetAllSysRolePageList(string roleName, int? orgId, string orderFileds, ref int totalCount, int pageIndex, int pageSize)
        {
            return GetInstance().Queryable<SysRoleEntity>().OrderBy(orderFileds)
                .WhereIF(!roleName.IsNullOrWhiteSpace(), t => t.RoleName.Contains(roleName))
                .WhereIF(orgId != null, t => t.OrgId == orgId)
                .ToPageList(pageIndex, pageSize ,ref totalCount).ToList();
        }

        #endregion

        #region 菜单

        /// <summary>
        /// 获取菜单 "menu_id, p_menu_id, menu_name"
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<List<SysMenuEntity>> GetSysMenuByRoleId(int roleId)
        {
            return GetInstance().Queryable<SysMenuEntity, SysRoleMenuEntity>((m, rm) => m.MenuId == rm.MenuId)
                .Where((m, rm) => rm.RoleId == roleId).Select("m.menu_id, m.p_menu_id, m.menu_name").ToListAsync();
        }

        /// <summary>
        /// 获取按钮
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<List<SysPermissionEntity>> GetSysPermissionByRoleId(int roleId)
        {
            return GetInstance().Queryable<SysPermissionEntity, SysRolePermissionEntity>((m, rm) => m.PermissionId == rm.PermissionId)
                .Where((m, rm) => rm.RoleId == roleId).Select((m, rm) => m).ToListAsync();
        }

        #endregion

        #region 用户

        /// <summary>
        /// 获取全部用户分页
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="orgId"></param>
        /// <param name="orderFileds"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<SysUserEntity> GetAllSysUserPageList(string userName, int? orgId, string orderFileds, ref int totalCount, int pageIndex, int pageSize)
        {
            return GetInstance().Queryable<SysUserEntity>()
                .OrderBy(orderFileds)
                .WhereIF(!userName.IsNullOrWhiteSpace(), t => t.UserName.Contains(userName))
                .WhereIF(orgId != null, t => t.OrgId == orgId)
                .ToPageList(pageIndex, pageSize, ref totalCount).ToList();
        }

        #endregion
    }
}
