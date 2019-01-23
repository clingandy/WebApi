/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/19 10:52:58
** desc：    SystemRepository类
** Ver.:     V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Common;
using Infrastructure.Cryptogram;
using Infrastructure.Domain;
using Infrastructure.Model.Entity.System;
using Infrastructure.Model.View.System;
using Infrastructure.Service;

namespace Infrastructure.Core
{
    public class SystemRepository : ISystemRepository
    {
        #region 属性

        private readonly SystemService _service = SystemService.Instance;

        #endregion

        #region 菜单

        /// <summary>
        /// 获取全部菜单分页
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<SysMenuEntity> GetAllSysMenuPageList(ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return _service.FindByPage<SysMenuEntity>("menu_id desc", ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取全部菜单，后台管理菜单用
        /// </summary>
        /// <returns></returns>
        public IList<SysMenuView> GetAllSysMenuList()
        {
            var mlist = _service.FindByFuncAsync<SysMenuEntity>(t => true);
            var pList = _service.FindByFuncAsync<SysPermissionEntity>(t => true);

            var rootListTemp = mlist.Result.Where(t => t.PMenuId == 0).OrderBy(t => t.Sort);
            var rootList = new List<SysMenuView>();
            foreach (var item in rootListTemp)
            {
                var childList = new List<SysMenuChildrenView>();
                var childListTemp = mlist.Result.Where(all => all.PMenuId == item.MenuId).OrderBy(t=> t.Sort).ToList();
                foreach (var childItem in childListTemp)
                {
                    var childModel = new SysMenuChildrenView().CopyFrom(childItem);
                    childModel.Permission = pList.Result.Where(t => t.MenuId == childItem.MenuId).ToList();
                    childList.Add(childModel);
                }
                var model = new SysMenuView().CopyFrom(item);
                model.Children = childList;
                model.Permission = pList.Result.Where(t => t.MenuId == item.MenuId).ToList();
                rootList.Add(model);
            }
            return rootList;
        }

        /// <summary>
        /// 获取全部菜单，后台管理角色授权用
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public IList<object> GetMenuIdAndPermissionIdList(int? roleId)
        {
            Task<List<SysMenuEntity>> mlist;
            Task<List<SysPermissionEntity>> pList;
            if (roleId != null)
            {
                //可优化到多表查询
                mlist = _service.GetSysMenuByRoleId(roleId.Value);
                pList = _service.GetSysPermissionByRoleId(roleId.Value);
            }
            else
            {
                mlist = _service.FindByFuncAsync<SysMenuEntity>(t => true, "menu_id, p_menu_id, menu_name");
                pList = _service.FindByFuncAsync<SysPermissionEntity>(t => true);
            }

            var rootListTemp = mlist.Result.Where(t => t.PMenuId == 0);
            var rootList = new List<dynamic>();
            foreach (var item in rootListTemp)
            {
                var childList = mlist.Result.Where(all => all.PMenuId == item.MenuId).Select(t=> new
                {
                    Check = roleId != null,
                    t.MenuId,
                    t.PMenuId,
                    t.MenuName,
                    Permission = pList.Result.Where(p => p.MenuId == t.MenuId).Select(p => new { Check = roleId != null, p.MenuId, p.PermissionId, p.PermissionName }).ToList()
                }).ToList();
                dynamic model = new ExpandoObject();
                model.check = roleId != null;
                model.menuId = item.MenuId;
                model.pMenuId = item.PMenuId;
                model.menuName = item.MenuName;
                model.children = childList;
                model.permission = pList.Result.Where(t => t.MenuId == item.MenuId).Select(p => new { Check = roleId != null, p.MenuId, p.PermissionId, p.PermissionName }).ToList();
                rootList.Add(model);
            }
            return rootList;
        }

        /// <summary>
        /// 根据状态获取全部菜单
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<SysMenuEntity> GetSysMenuListByStatus(bool status = true)
        {
            return _service.FindByFunc<SysMenuEntity>(t => t.Status == status);
        }

        /// <summary>
        /// 根据ID获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysMenuEntity GetSysMenu(int id)
        {
            return _service.FindById<SysMenuEntity>(id);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifySysMenu(SysMenuEntity model)
        {
            model.ModifyTime = DateTime.Now;
            return _service.Update(model);
        }

        /// <summary>
        /// 修改菜单权限
        /// </summary>
        /// <param name="mid">菜单ID</param>
        /// <param name="permission">modelList</param>
        /// <returns></returns>
        public bool ModifyMenuPermission(int mid, IList<SysPermissionEntity> permission)
        {
            permission = permission.Where(t => !t.PermissionCode.IsNullOrWhiteSpace() && !t.PermissionName.IsNullOrWhiteSpace()).ToList();
            if (permission.Count == 0)
            {
                _service.DeleteBatchBySelfAsync<SysPermissionEntity>(t => t.MenuId == mid);
                return true;
            }
            foreach (var item in permission)
            {
                item.MenuId = mid;
                item.Status = true;
            }
            _service.DeleteBatchBySelf<SysPermissionEntity>(t => t.MenuId == mid);
            var isok = _service.InsertBatch(permission.ToList());

            return isok;
        }

        /// <summary>
        /// 插入菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSysMenu(SysMenuEntity model)
        {
            model.ModifyTime = DateTime.Now;
            return _service.Insert(model);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelSysMenuById(int id)
        {
            var isok = _service.Delete<SysMenuEntity>(id);
            if (isok)
            {
                //异步删除对应sys_role_menu表数据
                _service.DeleteBatchBySelfAsync<SysRoleMenuEntity>(t => t.MenuId == id);
                //异步删除对应sys_permission表数据
                _service.DeleteBatchBySelfAsync<SysPermissionEntity>(t => t.MenuId == id);
            }
            return isok;
        }

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
        public IList<SysRoleEntity> GetAllSysRolePageList(string roleName, int? orgId, ref int totalCount,int pageIndex = 1, int pageSize = 10)
        {
            return _service.GetAllSysRolePageList(roleName, orgId, "role_id desc", ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <returns></returns>
        public IList<SysRoleEntity> GetAllSysRoleList()
        {
            return _service.FindByFunc<SysRoleEntity>(t => true);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetSysRoleListByStatus(bool status = true)
        {
            return _service.FindByFunc<SysRoleEntity>(t => t.Status == status);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetSysRoleListByOrgId(int orgId)
        {
            return _service.FindByFunc<SysRoleEntity>(t => t.OrgId == orgId);
        }

        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysRoleEntity GetSysRole(int id)
        {
            return _service.FindById<SysRoleEntity>(id);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifySysRole(SysRoleEntity model)
        {
            model.Status = true;
            model.ModifyTime = DateTime.Now;
            return _service.Update(model);
        }

        /// <summary>
        /// 插入角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSysRole(SysRoleEntity model)
        {
            model.Status = true;
            model.ModifyTime = DateTime.Now;
            return _service.Insert(model);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelSysRoleById(int id)
        {
            var isok = _service.Delete<SysRoleEntity>(id);
            if (isok)
            {
                //异步删除对应sys_role_menu表数据
                _service.DeleteBatchBySelfAsync<SysRoleMenuEntity>(t => t.RoleId == id);
                //异步删除对应sys_role_permission表数据
                _service.DeleteBatchBySelfAsync<SysRolePermissionEntity>(t => t.RoleId == id);
            }
            return isok;
        }

        /// <summary>
        /// 添加角色菜单配置
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool AddSysRoleMenu(AddRolePermissionView view)
        {
            if (view.RoleId == 0 || view.MenuIds == null || view.PermissionIds == null)
            {
                return false;
            }
            var mlist = view.MenuIds.Select(t => new SysRoleMenuEntity { MenuId = t, RoleId = view.RoleId }).ToList();
            var plist = view.PermissionIds.Select(t => new SysRolePermissionEntity { PermissionId = t, RoleId = view.RoleId }).ToList();
            if (plist.Count > 0)
            {
                _service.DeleteBatchBySelf<SysRolePermissionEntity>(t => t.RoleId == view.RoleId);
                _service.InsertBatchAsync(plist);   //异步
            }
            if (mlist.Count > 0)
            {
                //异步删除对应sys_role_menu表数据
                _service.DeleteBatchBySelf<SysRoleMenuEntity>(t => t.RoleId == view.RoleId);
                return _service.InsertBatch(mlist);
            }
            return false;
        }

        #endregion

        #region 用户

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="orgId"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<SysUserEntity> GetAllSysUserList(string userName, int? orgId, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return _service.GetAllSysUserPageList(userName, orgId, "user_id desc", ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysUserEntity GetSysUser(int id)
        {
            return _service.FindById<SysUserEntity>(id);
        }

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
        /// 获取name获取用户数量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetSysUserCountByIdAndName(string name)
        {
            return _service.FindCountByFunc<SysUserEntity>(t => t.UserName == name);
        }


        /// <summary>
        /// 更新用户，只更新部分信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifySysUser(SysUserEntity model)
        {
            return _service.Update<SysUserEntity>(new
            {
                model.UserName,
                model.UserCode,
                model.IsLock,
                model.Status,
                model.OrgId,
                model.RoleId
            }, t=> t.UserId == model.UserId);
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSysUser(SysUserEntity model)
        {
            model.ModifyTime = DateTime.Now;
            model.UserPwd = CryptogramHelper.GetMd5Hash(model.UserPwd);
            return _service.Insert(model);
        }

        #endregion

        #region 组织

        /// <summary>
        /// 获取全部组织
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<SysOrgEntity> GetSysOrgPageList(ref int totalCount ,int pageIndex = 1, int pageSize = 10)
        {
            return _service.FindByPage<SysOrgEntity>("org_id desc", ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取全部组织
        /// </summary>
        /// <returns></returns>
        public IList<SysOrgEntity> GetAllSysOrgList()
        {
            return _service.FindByFunc<SysOrgEntity>(t=> true);
        }

        /// <summary>
        /// 获取组织
        /// </summary>
        /// <returns></returns>
        public IList<SysOrgEntity> GetSysOrgByStatusList(bool status)
        {
            return _service.FindByFunc<SysOrgEntity>(t => t.Status == status);
        }

        /// <summary>
        /// 根据ID获取组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysOrgEntity GetSysOrg(int id)
        {
            return _service.FindById<SysOrgEntity>(id);
        }

        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifySysOrg(SysOrgEntity model)
        {
            model.Status = true;
            model.ModifyTime = DateTime.Now;
            return _service.Update(model);
        }

        /// <summary>
        /// 插入组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSysOrg(SysOrgEntity model)
        {
            model.Status = true;
            model.ModifyTime = DateTime.Now;
            return _service.Insert(model);
        }

        #endregion
    }
}
