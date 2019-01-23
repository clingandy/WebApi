/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/19 10:21:24
** desc：    ISystemRepository类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;
using Infrastructure.Model.Entity.System;
using Infrastructure.Model.View.System;

namespace Infrastructure.Domain
{
    public interface ISystemRepository
    {
        #region 菜单

        /// <summary>
        /// 获取全部菜单分页
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<SysMenuEntity> GetAllSysMenuPageList(ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 获取全部菜单，后台管理菜单用
        /// </summary>
        /// <returns></returns>
        IList<SysMenuView> GetAllSysMenuList();

        /// <summary>
        /// 获取全部菜单，后台管理角色授权用
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        IList<object> GetMenuIdAndPermissionIdList(int? roleId);

        /// <summary>
        /// 根据状态获取全部菜单
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IList<SysMenuEntity> GetSysMenuListByStatus(bool status = true);

        /// <summary>
        /// 根据ID获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysMenuEntity GetSysMenu(int id);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifySysMenu(SysMenuEntity model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mid">菜单ID</param>
        /// <param name="permission">modelList</param>
        /// <returns></returns>
        bool ModifyMenuPermission(int mid, IList<SysPermissionEntity> permission);

        /// <summary>
        /// 插入菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddSysMenu(SysMenuEntity model);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DelSysMenuById(int id);

        #endregion

        #region 角色

        /// <summary>
        /// 获取全部角色分页
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="orgId"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetAllSysRolePageList(string roleName, int? orgId, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <returns></returns>
        IList<SysRoleEntity> GetAllSysRoleList();

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetSysRoleListByStatus(bool status);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetSysRoleListByOrgId(int orgId);

        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysRoleEntity GetSysRole(int id);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifySysRole(SysRoleEntity model);

        /// <summary>
        /// 插入角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddSysRole(SysRoleEntity model);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DelSysRoleById(int id);

        /// <summary>
        /// 添加角色菜单配置
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        bool AddSysRoleMenu(AddRolePermissionView view);

        #endregion

        #region 用户

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        IList<SysUserEntity> GetAllSysUserList(string userName, int? orgId, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysUserEntity GetSysUser(int id);

        /// <summary>
        /// 根据name获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SysUserEntity GetSysUserByName(string name);

        /// <summary>
        /// 获取name获取用户数量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetSysUserCountByIdAndName(string name);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifySysUser(SysUserEntity model);

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddSysUser(SysUserEntity model);

        #endregion

        #region 组织

        /// <summary>
        /// 获取全部组织
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<SysOrgEntity> GetSysOrgPageList(ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 获取全部组织
        /// </summary>
        /// <returns></returns>
        IList<SysOrgEntity> GetAllSysOrgList();

        /// <summary>
        /// 获取组织
        /// </summary>
        /// <returns></returns>
        IList<SysOrgEntity> GetSysOrgByStatusList(bool status);

        /// <summary>
        /// 根据ID获取组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysOrgEntity GetSysOrg(int id);

        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifySysOrg(SysOrgEntity model);

        /// <summary>
        /// 插入组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddSysOrg(SysOrgEntity model);

        #endregion
    }
}
