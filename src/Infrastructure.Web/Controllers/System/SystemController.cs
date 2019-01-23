using System.Collections.Generic;
using Infrastructure.Common;
using Infrastructure.Cryptogram;
using Infrastructure.Domain;
using Infrastructure.Model.Entity.System;
using Infrastructure.Model.View.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Web.Controllers.System
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly ISystemRepository _repository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public SystemController(ISystemRepository repository, IMemoryCache cache)
        {
            _cache = cache;
            _repository = repository;
        }

        #endregion

        #region 菜单

        /// <summary>
        /// 获取全部菜单分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysMenuEntity>>))]
        public ActionResult<object> GetAllSysMenuPageList(int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var list = _repository.GetAllSysMenuPageList(ref totalCount, pageIndex, pageSize);
            return list.ResponseSuccess("", totalCount);
        }

        /// <summary>
        /// 获取全部菜单，后台管理菜单用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysMenuView>>))]
        public ActionResult<object> GetAllSysMenuList()
        {
            var data = _cache.GetOrCreate("systemcontroller_getallsysmenulist", (entry) => _repository.GetAllSysMenuList());
            return data.ResponseSuccess();
        }

        /// <summary>
        /// 移除菜单缓存
        /// </summary>
        private void RemoveSysMenuCache()
        {
            _cache.Remove("systemcontroller_getallsysmenulist");
        }

        /// <summary>
        /// 获取全部菜单，后台管理角色授权用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public ActionResult<object> GetMenuIdAndPermissionIdList(int? roleId)
        {
            return _repository.GetMenuIdAndPermissionIdList(roleId).ResponseSuccess();
        }

        /// <summary>
        /// 根据状态获取全部菜单
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysMenuEntity>>))]
        public ActionResult<object> GetSysMenuListByStatus(bool status = true)
        {
            return _repository.GetSysMenuListByStatus(status).ResponseSuccess();
        }

        /// <summary>
        /// 根据ID获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<SysMenuEntity>))]
        public ActionResult<object> GetSysMenu(int id)
        {
            return _repository.GetSysMenu(id).ResponseSuccess();
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifySysMenu([FromForm]SysMenuEntity model)
        {
            RemoveSysMenuCache();
            return _repository.ModifySysMenu(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="mid">菜单ID</param>
        /// <param name="permission">modelList</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifyMenuPermission(int mid, [FromForm]List<SysPermissionEntity> permission)
        {
            RemoveSysMenuCache();
            return _repository.ModifyMenuPermission(mid, permission).ResponseSuccessFailure();
        }

        /// <summary>
        /// 插入菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddSysMenu([FromForm]SysMenuEntity model)
        {
            RemoveSysMenuCache();
            return _repository.AddSysMenu(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> DelSysMenuById(int id)
        {
            RemoveSysMenuCache();
            return _repository.DelSysMenuById(id).ResponseSuccessFailure();
        }

        #endregion

        #region 角色

        /// <summary>
        /// 获取全部角色分页
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysRoleEntity>>))]
        public ActionResult<object> GetAllSysRolePageList(string roleName, int? orgId, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var list = _repository.GetAllSysRolePageList(roleName, orgId, ref totalCount, pageIndex, pageSize);
            return list.ResponseSuccess("", totalCount);
        }

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysRoleEntity>>))]
        public ActionResult<object> GetAllSysRoleList()
        {
            return _repository.GetAllSysRoleList().ResponseSuccess();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysRoleEntity>>))]
        public ActionResult<object> GetSysRoleListByStatus(bool status = true)
        {
            return _repository.GetSysRoleListByStatus(status).ResponseSuccess();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysRoleEntity>>))]
        public ActionResult<object> GetSysRoleListByOrgId(int orgId)
        {
            return _repository.GetSysRoleListByOrgId(orgId).ResponseSuccess();
        }

        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<SysRoleEntity>))]
        public ActionResult<object> GetSysRole(int id)
        {
            return _repository.GetSysRole(id).ResponseSuccess();
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifySysRole([FromForm]SysRoleEntity model)
        {
            return _repository.ModifySysRole(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 插入角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddSysRole([FromForm]SysRoleEntity model)
        {
            return _repository.AddSysRole(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> DelSysRoleById(int id)
        {
            return _repository.DelSysRoleById(id).ResponseSuccessFailure();
        }

        /// <summary>
        /// 添加角色菜单配置
        /// </summary>
        /// <param name="rolePermission">菜单ID，权限ID</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddSysRoleMenu([FromForm]AddRolePermissionView rolePermission)
        {
            return _repository.AddSysRoleMenu(rolePermission).ResponseSuccessFailure();
        }

        #endregion

        #region 用户

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysUserEntity>>))]
        public ActionResult<object> GetAllSysUserList(string userName, int? orgId, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var list = _repository.GetAllSysUserList(userName, orgId, ref totalCount, pageIndex, pageSize);
            return list.ResponseSuccess("", totalCount);
        }

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<SysUserEntity>))]
        public ActionResult<object> GetSysUser(int id)
        {
            return _repository.GetSysUser(id).ResponseSuccess();
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ResetSysUserByPwd(int userId ,string pwd)
        {
            var user = _repository.GetSysUser(userId);
            if (user == null) return false.ResponseDataError();
            user.UserPwd = CryptogramHelper.GetMd5Hash(pwd);
            user.IsLock = false;
            user.ErrorCount = 0;
            return _repository.ModifySysUser(user).ResponseSuccessFailure();
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifySysUser([FromForm]SysUserEntity model)
        {
            if (model.UserName.IsNullOrWhiteSpace()) return false.ResponseDataError();
            model.UserName = model.UserName.Trim();
            var user = _repository.GetSysUserCountByIdAndName(model.UserName);
            if (user > 1) return false.ResponseDataError();
            return _repository.ModifySysUser(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddSysUser([FromForm]SysUserEntity model)
        {
            if(model.UserName.IsNullOrWhiteSpace()) return false.ResponseDataError();
            model.UserName = model.UserName.Trim();
            var count = _repository.GetSysUserCountByIdAndName(model.UserName);
            if (count > 0) return false.ResponseDataError();
            
            return _repository.AddSysUser(model).ResponseSuccessFailure();
        }

        #endregion

        #region 组织

        /// <summary>
        /// 获取全部组织
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysOrgEntity>>))]
        public ActionResult<object> GetSysOrgPageList(int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var list = _repository.GetSysOrgPageList(ref totalCount, pageIndex, pageSize);
            return list.ResponseSuccess("", totalCount);
        }

        /// <summary>
        /// 获取全部组织
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysOrgEntity>>))]
        public ActionResult<object> GetAllSysOrgList()
        {
            return _repository.GetAllSysOrgList().ResponseSuccess();
        }

        /// <summary>
        /// 获取全部组织
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<SysOrgEntity>>))]
        public ActionResult<object> GetSysOrgByStatusList(bool status)
        {
            return _repository.GetSysOrgByStatusList(status).ResponseSuccess();
        }

        /// <summary>
        /// 根据ID获取组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<SysOrgEntity>))]
        public ActionResult<object> GetSysOrg(int id)
        {
            return _repository.GetSysOrg(id).ResponseSuccess();
        }

        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> ModifySysOrg([FromForm]SysOrgEntity model)
        {
            return _repository.ModifySysOrg(model).ResponseSuccessFailure();
        }

        /// <summary>
        /// 插入组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<bool>))]
        public ActionResult<object> AddSysOrg([FromForm]SysOrgEntity model)
        {
            return _repository.AddSysOrg(model).ResponseSuccessFailure();
        }

        #endregion

    }
}
