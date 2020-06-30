using AutoMapper;
using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.System.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Services.Bms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Bms
{
    public class BmsOrganizationAppService : IBmsOrganizationAppService
    {
        private readonly IBmsOrganizationService _bmsOrganizationService;
        private readonly IBmsOrganizationRepository _bmsOrganizationRepository;
        private readonly IBmsUserService _bmsUserService;
        private readonly IBmsMenuService _bmsMenuService;
        private IMapper _mapper { get; }
        public BmsOrganizationAppService(
            IBmsOrganizationService bmsOrganizationService,
            IBmsOrganizationRepository bmsOrganizationRepository,
            IBmsUserService bmsUserService,
            IBmsMenuService bmsMenuService,
            IMapper mapper)
        {
            _bmsOrganizationService = bmsOrganizationService;
            _bmsOrganizationRepository = bmsOrganizationRepository;
            _bmsUserService = bmsUserService;
            _bmsMenuService = bmsMenuService;
            _mapper = mapper;
        }
        public ResponseDto<BmsOrganizationDto> ManageCreateOrganization(BmsOrganizationDto request, long userId)
        {
            var result = new ResponseDto<BmsOrganizationDto>();
            if (string.IsNullOrEmpty(request.Name))
                throw new CustomException("组织结构的名称必填", 1);

            var parentResp = _bmsOrganizationService.ManageGetChildrenOrganization(request.ParentId);
            if (parentResp.Code != 0)
                throw new CustomException("父级组织架构不存在", 1);

            if (parentResp.Data.Exists(p => p.Name == request.Name))
                throw new CustomException("该父级下的组织架构已存在", 1);

            var organization = new BmsOrganization()
            {
                Name = request.Name.Trim(),
                DisplayName = request.DisplayName.ToEmpty(),
                Code = request.Code,
                ShowOrder = request.ShowOrder,
                ParentId = request.ParentId,
                CreationTime = DateTime.Now,
                CreatorUserId = userId
            };

            var execute = _bmsOrganizationService.ManageCreateOrganization(organization, userId);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            if (execute.Success)
            {
                result.Data = new BmsOrganizationDto()
                {
                    Name = execute.Data.Name,
                    DisplayName = execute.Data.DisplayName,
                    Code = execute.Data.Code,
                    ShowOrder = execute.Data.ShowOrder,
                    ParentId = execute.Data.ParentId,
                    Id = execute.Data.Id
                };
            }
            else
                result.Data = new BmsOrganizationDto();
            result.Success = execute.Success;
            return result;
        }

        public ResponseDto<BmsOrganizationDto> ManageDeleteOrganization(Int64 OrganizationId, long userId)
        {
            var result = new ResponseDto<BmsOrganizationDto>();
            if (OrganizationId <= 0)
                throw new CustomException("数据异常", 1);

            var execute = _bmsOrganizationService.ManageDeleteOrganization(OrganizationId, userId);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            result.Success = execute.Success;
            return result;
        }

        public ResponseDto<BmsOrganizationDto> ManageModifyOrganization(BmsOrganizationDto request, long userId)
        {
            var result = new ResponseDto<BmsOrganizationDto>();
            if (request.Id <= 0)
                throw new CustomException("数据异常", 1);

            var organization = new BmsOrganization()
            {
                Name = request.Name ?? "",
                Code = request.Code,
                ShowOrder = request.ShowOrder,
                DisplayName = request.DisplayName,
                LastModificationTime = DateTime.Now,
                LastModifierUserId = userId,
                Id = request.Id
            };

            var execute = _bmsOrganizationService.ManageModifyOrganization(organization, userId);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            if (organization.Id > 0)
            {
                result.Data = new BmsOrganizationDto()
                {
                    Name = organization.Name,
                    DisplayName = organization.DisplayName,
                    Code = organization.Code,
                    ShowOrder = organization.ShowOrder,
                    ParentId = organization.ParentId,
                    Id = organization.Id
                };
            }
            else
                result.Data = new BmsOrganizationDto();
            result.Success = execute.Success;
            return result;
        }

        public ResponseDto<List<BmsOrganizationTreeDto>> ManageGetOrganizationTree()
        {
            var result = new ResponseDto<List<BmsOrganizationTreeDto>>();
            var execute = _bmsOrganizationService.ManageGetOrganizationTree();
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            if (execute.Data != null && execute.Data.Count > 0)
                result.Data = _mapper.Map<List<BmsOrganizationTreeDto>>(execute.Data);
            else
                result.Data = new List<BmsOrganizationTreeDto>();
            result.Success = execute.Success;
            return result;
        }

        /// <summary>
        /// 获取下级组织
        /// </summary>
        public ResponseDto<List<BmsOrganizationDto>> ManageGetChildrenOrganization(long organizationId)
        {
            var result = new ResponseDto<List<BmsOrganizationDto>>();
            var execute = _bmsOrganizationService.ManageGetChildrenOrganization(organizationId);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            if (execute.Data != null && execute.Data.Count > 0)
                result.Data = _mapper.Map<List<BmsOrganizationDto>>(execute.Data);
            else
                result.Data = new List<BmsOrganizationDto>();
            result.Success = execute.Success;
            return result;
        }

        public ResponseDto<BmsOrganizationDto> ManageGetOrganizationDetail(long organizationId)
        {
            var result = new ResponseDto<BmsOrganizationDto>();
            var execute = _bmsOrganizationService.ManageGetOrganizationDetail(organizationId);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            if (execute.Data != null && execute.Data.Id > 0)
                result.Data = _mapper.Map<BmsOrganizationDto>(execute.Data);
            else
                result.Data = new BmsOrganizationDto();
            result.Success = execute.Success;
            return result;
        }


        public ResponsePageDto<BmsUserDto> GetOrganizationUserList(long orgId, string key, int pageSize, int currentPage)
        {
            var result = new ResponsePageDto<BmsUserDto>();
            var execute = _bmsUserService.GetOrganizationUserList(orgId, key, pageSize, currentPage);

            result.Code = execute.Code;
            result.Msg = execute.Msg;
            result.Data = new List<BmsUserDto>();
            if (execute.Data != null && execute.Data.Count > 0)
            {
                execute.Data.ForEach(p =>
                {
                    result.Data.Add(new BmsUserDto()
                    {
                        Id = p.Id,
                        LoginName = p.LoginName,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        FullName = p.FullName,
                        EmailAddress = p.EmailAddress,
                        Status = p.Status
                    });
                });
            }
            result.Success = execute.Success;
            return result;
        }

        public ResponsePageDto<BmsUserDto> GetOtherOrganizationUserList(long orgId, string key, int pageSize, int currentPage)
        {
            var result = new ResponsePageDto<BmsUserDto>();

            var execute = _bmsUserService.GetOtherOrganizationUserList(orgId, key, pageSize, currentPage);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            result.Data = new List<BmsUserDto>();
            if (execute.Data != null && execute.Data.Count > 0)
            {
                execute.Data.ForEach(p =>
                {
                    result.Data.Add(new BmsUserDto()
                    {
                        Id = p.Id,
                        LoginName = p.LoginName,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        EmailAddress = p.EmailAddress,
                        FullName = p.FullName,
                        Status = p.Status
                    });
                });
            }
            result.Success = execute.Success;
            return result;
        }


        /// <summary>
        /// 在组织架构中加入用户
        /// 同时记录组织架构路径
        /// </summary>
        public ResponseDto<string> AddUser(BmsOrganizationUserCreateDto request, long loginId)
        {
            var result = new ResponseDto<string>();

            var ids = string.Empty;
            ids = _bmsOrganizationService.GetOrgIds(ids, request.OrganizationId);
            ids = ids.Substring(0, ids.Length - 1);
            var userList = new List<BmsUserOrganization>();
            foreach (var item in request.UserIds)
            {
                var org = new BmsUserOrganization();
                org.CreationTime = DateTime.Now;
                org.CreatorUserId = item;
                org.UserId = (int)item;
                org.OrganizationId = (int)request.OrganizationId;
                org.OrgIds = ids;
                org.CreatorUserId = loginId;
                userList.Add(org);
            }

            var execute = _bmsUserService.AddOrganization(userList);
            if (execute <= 0)
                throw new CustomException("组织机构添加失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 移除组织机构与用户的关联
        /// </summary>
        public ResponseDto<string> RemoveUser(long userId, long organizationId, long loginId)
        {
            var result = new ResponseDto<string>();
            var execute = _bmsUserService.RemoveOrganization(userId, organizationId, loginId);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            result.Success = execute.Success;
            return result;
        }

        /// <summary>
        /// 获取岗位完整的菜单树
        /// </summary>
        public ResponseDto<BmsMenuTreeDto> GetWholeMenuTree(long OrganizationId)
        {
            var result = new ResponseDto<BmsMenuTreeDto>();
            result.Data = new BmsMenuTreeDto();
            if (OrganizationId <= 0)
                throw new CustomException("组织架构Id必须大于0", 1);

            var organizationResp = _bmsOrganizationService.ManageSingleGet(OrganizationId);
            if (organizationResp.Code == 0 && organizationResp.Success)
            {
                var organization = organizationResp.Data;
                if (organization == null || organization.Id != OrganizationId)
                    throw new CustomException("组织架构不存在", 1);

                var menuIds = organization.OrganizationMenuDict.Keys.ToList();

                var childrenResp = _bmsMenuService.GetWholeTree(0, menuIds);

                if (childrenResp.Code == 0 && childrenResp.Success)
                {
                    result.Code = 0;
                    result.Success = true;
                    result.Data.Children = _mapper.Map<List<BmsMenuTreeDto>>(childrenResp.Data);
                }
            }
            return result;
        }

        /// <summary>
        /// 修改组织架构菜单树
        /// </summary>
        public ResponseDto<int> ModifyMenuTree(long userId, BmsOrganizationMenuTreeModifyDto request)
        {
            var result = new ResponseDto<int>();

            var organizationResp = _bmsOrganizationService.ManageSingleGet(request.OrganizationId);
            if (organizationResp.Code == 0 && organizationResp.Success)
            {
                var organization = organizationResp.Data;
                if (organization == null || organization.Id != request.OrganizationId)
                    throw new CustomException("组织架构不存在", 1);

                //新权限集合
                var grantedList = _bmsMenuService.MenuIdsGetWithParentId(request.GrantedList);

                //老权限集合
                var oldList = organization.OrganizationMenuDict.Keys.ToList();

                var add_list = new List<BmsOrganizationMenu>(); //新增
                var delete_list = new List<BmsOrganizationMenu>(); //删除
                foreach (var i in grantedList)
                {
                    //新增的权限
                    if (oldList.Contains(i) == false)
                    {
                        var obj = new BmsOrganizationMenu();
                        obj.OrganizationId = request.OrganizationId;
                        obj.MenuId = i;
                        obj.CreationTime = DateTime.Now;
                        obj.CreatorUserId = userId;

                        add_list.Add(obj);
                    }
                }
                foreach (var i in oldList)
                {
                    //删除的权限
                    if (grantedList.Contains(i) == false)
                    {
                        if (organization.OrganizationMenuDict.Keys.Contains(i))
                        {
                            var obj = organization.OrganizationMenuDict[i];
                            delete_list.Add(obj);
                        }
                    }
                }

                if (add_list.Any() == false && delete_list.Any() == false)
                {
                    result.Code = 0;
                    result.Success = true;
                    result.Msg = "没有变化";
                    result.Data = 0;
                }
                else
                {
                    var excuteResp = _bmsOrganizationService.ManageMenuUpdate(request.OrganizationId, add_list, delete_list);
                    if (excuteResp.Code == 0 && excuteResp.Success)
                    {
                        result.Code = 0;
                        result.Success = true;
                        result.Data = excuteResp.Data;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 强制刷新用户字典
        /// </summary>
        public ResponseDto<string> ForceRefreshDict()
        {
            var result = new ResponseDto<string>();
            _bmsOrganizationRepository.ForceRefreshDict();
            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
