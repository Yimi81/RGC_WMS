using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Services.Bms;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms
{
    public class BmsRoleAppService : IBmsRoleAppService, IAppService
    {
        private readonly IBmsRoleRepository _bmsRoleRepository;
        private readonly IBmsRoleService _bmsRoleService;
        private readonly IBmsUserService _bmsUserService;


        public BmsRoleAppService(
            IBmsRoleRepository bmsRoleRepository,
            IBmsRoleService bmsRoleService,
            IBmsUserService bmsUserService
            )
        {
            _bmsRoleRepository = bmsRoleRepository;
            _bmsRoleService = bmsRoleService;
            _bmsUserService = bmsUserService;
        }

        /// <summary>
        /// 后台获取角色列表
        /// </summary>
        public ResponseDto<List<BmsRoleListDto>> ManageGetRoleList(string key)
        {
            var result = new ResponseDto<List<BmsRoleListDto>>();
            result.Data = new List<BmsRoleListDto>();

            var excute = _bmsRoleRepository.AllListGet(key);
            if (excute.Count > 0)
            {
                foreach (var role in excute)
                {
                    var _role = new BmsRoleListDto();
                    _role.Id = role.Id;
                    _role.Name = role.Name;
                    _role.DisplayName = role.DisplayName;
                    _role.IsStatic = role.IsStatic;
                    _role.Desc = role.Desc;
                    _role.creationTime = role.CreationTimeString;
                    result.Data.Add(_role);
                }
            }

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        public ResponseDto<string> CreateRole(BmsRoleCreateOrUpdateDto createRole, long creatorUserId)
        {
            var result = new ResponseDto<string>();
            if (string.IsNullOrEmpty(createRole.Name))
                throw new CustomException("角色名称不能为空", 1);

            if (_bmsRoleRepository.IsNameExists(createRole.Name))
                throw new CustomException("角色名称不能重复", 1);

            var role = new BmsRoleCreateOrUpdateDo();
            role.Name = createRole.Name;
            role.DisplayName = createRole.DisplayName;
            role.IsStatic = createRole.IsStatic;
            role.Desc = createRole.Desc;

            var excute = _bmsRoleService.ManageCreateRole(role, creatorUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 获取角色详情
        /// </summary>
        public ResponseDto<BmsRoleCreateOrUpdateDto> GetRoleDetail(long roleId)
        {
            var result = new ResponseDto<BmsRoleCreateOrUpdateDto>();
            result.Data = new BmsRoleCreateOrUpdateDto();
            if (roleId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _bmsRoleService.ManageGetRoleByRoleId(roleId);
            if (excute.Success)
            {
                var role = excute.Data;
                var _role = new BmsRoleCreateOrUpdateDto();
                _role.Id = role.Id;
                _role.Name = role.Name;
                _role.DisplayName = role.DisplayName;
                _role.IsStatic = role.IsStatic;
                _role.Desc = role.Desc;
                result.Data = _role;

                result.Success = true;
                result.Code = 0;
            }
            else
            {
                result.Code = excute.Code;
                result.Msg = excute.Msg;
                result.Success = excute.Success;
            }
            return result;
        }

        /// <summary>
        /// 编辑角色资料
        /// </summary>
        public ResponseDto<string> ModifyRole(BmsRoleCreateOrUpdateDto modifyRole, long modifierUserId)
        {
            var result = new ResponseDto<string>();
            if (modifyRole.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            if (string.IsNullOrEmpty(modifyRole.Name))
                throw new CustomException("角色名称不能为空", 1);

            var role = new BmsRoleCreateOrUpdateDo();
            role.Id = modifyRole.Id;
            role.Name = modifyRole.Name;
            role.DisplayName = modifyRole.DisplayName;
            role.IsStatic = modifyRole.IsStatic;
            role.Desc = modifyRole.Desc;

            var excute = _bmsRoleService.ManageModifyRole(role, modifierUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 删除角色数据
        /// </summary>
        public ResponseDto<string> DeleteRole(long roleId, long deleterUserId)
        {
            var result = new ResponseDto<string>();
            if (roleId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _bmsRoleService.ManageDeleteRole(roleId, deleterUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 后台查询全部角色列表,选择列表
        /// </summary>
        public ResponsePageDto<BmsRoleSimpleListDto> GetRoleSimpleList()
        {
            var result = new ResponsePageDto<BmsRoleSimpleListDto>();
            result.Data = new List<BmsRoleSimpleListDto>();
            var excute = _bmsRoleRepository.AllListGet(string.Empty);
            if (excute.Count > 0)
            {
                foreach (var system in excute)
                {
                    var _role = new BmsRoleSimpleListDto();
                    _role.Id = system.Id;
                    _role.Name = system.Name;
                    _role.DisplayName = system.DisplayName;
                    result.Data.Add(_role);
                }
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 获取拥有角色授权的所有用户
        /// </summary>
        public ResponsePageDto<BmsUsersimpleListDto> GetGrantedUsers(long roleId, int currentPage)
        {
            var result = new ResponsePageDto<BmsUsersimpleListDto>();
            result.Data = new List<BmsUsersimpleListDto>();
            var excute = _bmsUserService.GetRoleGrantedUsers(roleId, currentPage, out int count);
            if (excute.Success)
            {
                foreach (var user in excute.Data)
                {
                    var _user = new BmsUsersimpleListDto();
                    _user.Id = user.Id;
                    _user.FullName = user.FirstName + " " + user.LastName;
                    result.Data.Add(_user);
                }
                result.Page.TotalCount = count;
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Code = excute.Code;
                result.Msg = excute.Msg;
                result.Success = excute.Success;
            }
            return result;
        }

        /// <summary>
        /// 强制刷新角色字典
        /// </summary>
        /// <returns></returns>
        public ResponseDto<string> ForceRefreshDict()
        {
            var result = new ResponseDto<string>();
            _bmsRoleRepository.ForceRefreshDict();
            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
