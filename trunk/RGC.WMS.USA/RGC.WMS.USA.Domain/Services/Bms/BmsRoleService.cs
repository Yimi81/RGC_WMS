using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using RGC.WMS.USA.Domain.Repositories.Bms;
using System;

namespace RGC.WMS.USA.Domain.Services.Bms
{
    public class BmsRoleService : IBmsRoleService
    {
        private readonly IBmsRoleRepository _bmsRoleRepository;

        public BmsRoleService(
            IBmsRoleRepository bmsRoleRepository
            )
        {
            _bmsRoleRepository = bmsRoleRepository;
        }


        /// <summary>
        /// 后台新增角色
        /// </summary>
        public ResponseDo<string> ManageCreateRole(BmsRoleCreateOrUpdateDo createRole, long creatorUserId)
        {
            var result = new ResponseDo<string>();
            var role = new BmsRole();
            role.Name = createRole.Name;
            role.DisplayName = createRole.DisplayName;
            role.IsStatic = createRole.IsStatic;
            role.Desc = createRole.Desc;

            role.CreationTime = DateTime.Now;
            role.CreatorUserId = creatorUserId;

            var excute = _bmsRoleRepository.SingleAdd(role);
            if (excute == null || excute.Id <= 0)
                throw new CustomException("角色新增失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台修改角色
        /// </summary>
        public ResponseDo<string> ManageModifyRole(BmsRoleCreateOrUpdateDo modifyRole, long modifierUserId)
        {
            var result = new ResponseDo<string>();
            var role = _bmsRoleRepository.SingleGet(modifyRole.Id);
            if (role == null || role.Id <= 0)
                throw new CustomException("请求数据异常，操作失败", 1);

            if (_bmsRoleRepository.IsNameExists(modifyRole.Name, role.Id))
                throw new CustomException("角色名称已存在", 1);

            role.Name = modifyRole.Name;
            role.DisplayName = modifyRole.DisplayName;
            role.IsStatic = modifyRole.IsStatic;
            role.Desc = modifyRole.Desc;

            role.LastModificationTime = DateTime.Now;
            role.LastModifierUserId = modifierUserId;

            var excute = _bmsRoleRepository.SingleUpdate(role);
            if (excute <= 0)
                throw new CustomException("角色数据更新失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台伪删除角色
        /// </summary>
        public ResponseDo<string> ManageDeleteRole(long roleId, long modifierUserId)
        {
            var result = new ResponseDo<string>();
            var role = _bmsRoleRepository.SingleGet(roleId);
            if (role == null || role.Id <= 0)
                throw new CustomException("请求数据异常，操作失败", 1);

            if (role.IsStatic)
                throw new CustomException("该角色无法被删除", 1);

            role.IsDeleted = true;
            role.DeleterUserId = modifierUserId;
            role.DeletionTime = DateTime.Now;
            var excute = _bmsRoleRepository.SingleDelete(role);
            if (excute <= 0)
                throw new CustomException("角色删除失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台根据角色id获取角色详情
        /// </summary>
        public ResponseDo<BmsRoleCreateOrUpdateDo> ManageGetRoleByRoleId(long roleId)
        {
            var result = new ResponseDo<BmsRoleCreateOrUpdateDo>();
            
            var role = _bmsRoleRepository.SingleGet(roleId);
            if (role == null || role.Id <= 0)
                throw new CustomException("请求数据异常，角色不存在", 1);

            result.Data = new BmsRoleCreateOrUpdateDo();
            result.Data.Id = role.Id;
            result.Data.Name = role.Name;
            result.Data.DisplayName = role.DisplayName;
            result.Data.IsStatic = role.IsStatic;
            result.Data.Desc = role.Desc;

            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
