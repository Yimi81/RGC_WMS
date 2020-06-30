using AutoMapper;
using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using RGC.WMS.USA.Domain.Entities.System.Enum;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Services.Bms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Bms
{
    public class BmsUserAppService : IBmsUserAppService, IAppService
    {
        private readonly IBmsUserRepository _bmsUserRepository;
        private readonly IBmsOrganizationRepository _bmsOrganizationRepository;

        private readonly IBmsMenuService _bmsMenuService;
        private readonly IBmsUserService _bmsUserService;
        private readonly IBmsOrganizationService _bmsOrganizationService;
        private IMapper _mapper { get; }
        public BmsUserAppService(
            IBmsUserRepository bmsUserRepository,
            IBmsOrganizationRepository bmsOrganizationRepository,
            IBmsMenuService bmsMenuService,
            IBmsUserService bmsUserService,
            IBmsOrganizationService bmsOrganizationService,
            IMapper mapper)
        {
            _bmsUserRepository = bmsUserRepository;
            _bmsOrganizationRepository = bmsOrganizationRepository;
            _bmsMenuService = bmsMenuService;
            _bmsUserService = bmsUserService;
            _bmsOrganizationService = bmsOrganizationService;
            _mapper = mapper;
        }

        /// <summary>
        /// 通过登录名密码获取用户
        /// </summary>
        public ResponseDto<BmsUserExtend> CanLogin(string loginName, string password)
        {
            var result = new ResponseDto<BmsUserExtend>();

            var user = _bmsUserRepository.SingleGet(loginName);
            if (user == null && user.Id <= 0)
                throw new CustomException("无效用户名", 1);

            if (user.Status != BmsUserStatus.Normal)
                throw new CustomException("账号未激活", 1);

            if (user.Password != user.CreatePassword(password))
                throw new CustomException("密码错误", 1);

            result.Data = user;
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台创建用户
        /// </summary>
        public ResponseDto<string> CreateBmsUser(BmsUserCreateOrUpdateDto createUser, long creatorUserId)
        {
            var result = new ResponseDto<string>();

            if (string.IsNullOrEmpty(createUser.LoginName))
                throw new CustomException("登录名不能为空", 1);

            if (string.IsNullOrEmpty(createUser.EmailAddress))
                throw new CustomException("邮箱不能为空", 1);

            if (string.IsNullOrEmpty(createUser.Password))
                throw new CustomException("密码不能为空", 1);

            if (_bmsUserRepository.IsLoginNameExists(createUser.LoginName))
                throw new CustomException("登录名不能重复", 1);

            if (_bmsUserRepository.IsEmailAddressExists(createUser.EmailAddress))
                throw new CustomException("邮箱不能重复", 1);

            var user = new BmsUserCreateOrUpdateDo();
            user.Status = BmsUserStatus.Normal;// 1;
            user.LoginName = createUser.LoginName;
            user.FirstName = createUser.FirstName;
            user.LastName = createUser.LastName;
            user.EmailAddress = createUser.EmailAddress;
            user.Sex = createUser.Sex;
            user.Telephone = createUser.Telephone;
            user.Mobile = createUser.Mobile;
            user.Fax = createUser.Fax;
            user.Wechat = createUser.Wechat;
            user.Password = createUser.Password;

            var excute = _bmsUserService.CreateBmsUser(user, creatorUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 后台分页查询用户列表
        /// </summary>
        public ResponsePageDto<BmsUserListDto> GetBmsUserList(SearchFilterDto searchFilter)
        {
            var result = new ResponsePageDto<BmsUserListDto>();
            result.Data = new List<BmsUserListDto>();
            var searchDo = new SearchFilterDo()
            {
                CurrentPage = searchFilter.CurrentPage,
                PageSize = searchFilter.PageSize,
                SearchKey = searchFilter.SearchKey,
                Sorting = searchFilter.Sorting
            };
            var excute = _bmsUserRepository.PageQuery(searchDo, out int count);

            if (excute.Count > 0)
            {
                foreach (var user in excute)
                {
                    var _user = new BmsUserListDto();
                    _user.Id = user.Id;
                    _user.LoginName = user.LoginName;
                    _user.EmailAddress = user.EmailAddress;
                    _user.FullName = user.FirstName + " " + user.LastName;
                    _user.Status = user.Status;
                    _user.LastLoginTime = user.LastLoginTime == null ? "" : user.LastLoginTime.Value.ToString("G");
                    result.Data.Add(_user);
                }
            }
            result.Code = 0;
            result.Success = true;
            result.Page.TotalCount = count;
            result.Page.TotalPages = (int)Math.Ceiling((Decimal)result.Page.TotalCount / searchFilter.PageSize);
            result.Page.PageSize = searchFilter.PageSize;
            result.Page.CurrentPage = searchFilter.CurrentPage;
            return result;
        }

        /// <summary>
        /// 后台获取用户编辑对象
        /// </summary>
        public ResponseDto<BmsUserCreateOrUpdateDto> GetBmsUserDetail(long Id)
        {
            var result = new ResponseDto<BmsUserCreateOrUpdateDto>();
            var userResp = _bmsUserService.GetDetail(Id);
            if (!(userResp.Success && userResp.Data.Id > 0))
                throw new CustomException("用户不存在", 1);

            var user = userResp.Data;
            result.Data = new BmsUserCreateOrUpdateDto();
            result.Data.Id = user.Id;
            result.Data.LoginName = user.LoginName;
            result.Data.FirstName = user.FirstName;
            result.Data.LastName = user.LastName;
            result.Data.EmailAddress = user.EmailAddress;
            result.Data.Sex = user.Sex;
            result.Data.Mobile = user.Mobile;
            result.Data.Telephone = user.Telephone;
            result.Data.Fax = user.Fax;
            result.Data.Wechat = user.Wechat;
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台更新用户信息
        /// </summary>
        public ResponseDto<string> UpdateBmsUser(BmsUserCreateOrUpdateDto updateUser, long modifierUserId)
        {
            var result = new ResponseDto<string>();
            if (updateUser.Id <= 0)
                throw new CustomException("数据异常", 1);

            if (string.IsNullOrEmpty(updateUser.LoginName))
                throw new CustomException("用户名不能未空", 1);

            if (string.IsNullOrEmpty(updateUser.EmailAddress))
                throw new CustomException("用户邮箱不能未空", 1);

            var user = new BmsUserCreateOrUpdateDo();
            user.Id = updateUser.Id;

            user.LoginName = updateUser.LoginName.Trim();
            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.EmailAddress = updateUser.EmailAddress;
            user.Sex = updateUser.Sex;
            user.Mobile = updateUser.Mobile;
            user.Telephone = updateUser.Telephone;
            user.Fax = updateUser.Fax;
            user.Wechat = updateUser.Wechat;

            var excute = _bmsUserService.UpdateBmsUser(user, modifierUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 后台更新用户状态
        /// </summary>
        public ResponseDto<string> UpdateBmsUserStatus(BmsUserCreateOrUpdateDto updateUser, long ModifierUserId)
        {
            var result = new ResponseDto<string>();
            var user = new BmsUserCreateOrUpdateDo();
            user.Id = updateUser.Id;
            user.Status = updateUser.Status;

            var excute = _bmsUserService.UpdateBmsUserStatus(user, ModifierUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 后台重置别人的密码，不需要校验旧密码
        /// </summary>
        public ResponseDto<string> ChangeBmsUserPassword(BmsUserChangePwdDto input, long ModifierUserId)
        {
            var result = new ResponseDto<string>();
            if (string.IsNullOrEmpty(input.NewPassword))
                throw new CustomException("密码数据不能空", 1);

            var excute = _bmsUserService.ChangeBmsUserPassword(Tuple.Create(input.UserId, input.NewPassword, input.OldPassword), ModifierUserId);

            if (!excute.Success)
                throw new CustomException("用户密码修改失败", 1);

            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }
        
        /// <summary>
        /// 后台删除用户
        /// </summary>
        public ResponseDto<string> DeleteBmsUser(long userId, long ModifierUserId)
        {
            var result = new ResponseDto<string>();
            if (userId == ModifierUserId)
                throw new CustomException("操作异常，无法删除自己的账号", 1);

            var excute = _bmsUserService.DeleteBmsUser(userId, ModifierUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 获取用户完整的菜单树，用于用户权限修改
        /// </summary>
        public ResponseDto<BmsUserMenuTreeDto> GetUserWholeMenuTree(long userId, long organizationId)
        {
            var result = new ResponseDto<BmsUserMenuTreeDto>();

            if (userId <= 0)
                throw new CustomException("用户不存在", 1);

            result.Data = new BmsUserMenuTreeDto();
            result.Data.UserId = userId;

            var userRep = _bmsUserService.GetDetail(userId);
            var user = new BmsUserExtend();
            if (userRep.Code == 0 && userRep.Success)
            {
                user = userRep.Data;
                if (user == null || user.Id != userId)
                    throw new CustomException("用户不存在", 1);

                if (organizationId == 0)
                    organizationId = user.PrimaryOrganizationId;
            }

            //如果没传，默认组织架构
            var menuIds = new List<Int64>();
            var removedMenuIds = new List<Int64>();
            var grantedMenuIds = new List<Int64>();
            var role_list = user.UserRoleDict.Keys.ToList();
            if (role_list.Contains((Int64)StaticRoleIds.SuperAdmin))
            {
                //超级管理员
                menuIds = _bmsMenuService.MenuIdsGetBySuperAdmin();
            }
            else
            {
                menuIds = _bmsOrganizationService.GrantedMenuIdsQuery(organizationId);
            }

            if (user.UserMenuExtendDict.Any())
            {
                removedMenuIds = user.UserMenuExtendDict.Values.Where(p => p.OrganizationId == organizationId && p.IsGranted == false).Select(p => p.MenuId).ToList();

                //超级管理员已经包含所有菜单
                if (role_list.Contains((Int64)StaticRoleIds.SuperAdmin) == false)
                {
                    //用户额外勾选的权限
                    grantedMenuIds = user.UserMenuExtendDict.Values.Where(p => p.OrganizationId == organizationId && p.IsGranted).Select(p => p.MenuId).ToList();
                }
            }

            var children = _bmsMenuService.WholeTreeGet(0, menuIds, removedMenuIds, grantedMenuIds);
            if (children != null && children.Count > 0)
            {
                result.Code = 0;
                result.Success = true;
                result.Data.MenuTree.Children = _mapper.Map<List<BmsMenuTreeDto>>(children);
            }
            return result;
        }

        /// <summary>
        /// 修改用户菜单树
        /// </summary>
        public ResponseDto<int> ManageModifyUserMenuTree(BmsUserMenuTreeModifyDto request, long loginId)
        {
            var result = new ResponseDto<int>();
            if (request.UserId <= 0)
                throw new CustomException("用户不存在", 1);

            var userRep = _bmsUserService.GetDetail(request.UserId);
            var user = new BmsUserExtend();
            if (userRep.Code == 0 && userRep.Success)
            {
                user = userRep.Data;
                if (user == null || user.Id != request.UserId)
                    throw new CustomException("用户不存在", 1);

                if (user.UserMenuExtendDict == null)
                    user.UserMenuExtendDict = new Dictionary<long, BmsUserMenuExtend>();
            }

            //新权限集合，包括父Id
            var grantedList = _bmsMenuService.MenuIdsGetWithParentId(request.GrantedList);

            //岗位权限
            var menuIds = new List<Int64>();

            var organizationResp = _bmsOrganizationService.ManageGetOrganizationDetail(request.OrganizationId);
            if (organizationResp.Code == 0 && organizationResp.Success)
            {
                var organization = organizationResp.Data;
                if (organization != null && organization.Id == request.OrganizationId)
                {
                    menuIds = organization.OrganizationMenuDict.Keys.ToList() ?? new List<long>();
                }
            }

            var add_list = new List<BmsUserMenuExtend>(); //新增 
            var update_list = new List<BmsUserMenuExtend>(); //修改
            var delete_list = new List<BmsUserMenuExtend>(); //删除

            //遍历选中的菜单获取新增
            foreach (var i in grantedList)
            {
                var obj = user.UserMenuExtendDict.Values.FirstOrDefault(p => p.OrganizationId == request.OrganizationId && p.MenuId == i);
                if (obj != null && obj.Id > 0)
                {
                    //只要用户中存在，就不新增

                    if (obj.IsGranted == false)
                    {
                        if (menuIds.Contains(i))
                        {
                            //岗位已经有权限，用户不用额外赋予权限
                            delete_list.Add(obj);
                        }
                        else
                        {
                            //否则是单独为该用户赋予权限
                            obj.IsGranted = true;
                            update_list.Add(obj);
                        }
                    }
                    else
                    {
                        //用户本来有额外权限，岗位已经有权限，用户不用额外赋予权限
                        if (menuIds.Contains(i))
                        {
                            delete_list.Add(obj);
                        }
                    }
                }
                else
                {
                    //新增的权限 用户和岗位都不包含
                    if (menuIds.Contains(i) == false)
                    {
                        var menu = new BmsUserMenuExtend();
                        menu.UserId = request.UserId;
                        menu.OrganizationId = request.OrganizationId;
                        menu.MenuId = i;
                        menu.IsGranted = true;
                        menu.CreationTime = DateTime.Now;
                        menu.CreatorUserId = loginId;

                        add_list.Add(menu);
                    }
                    else
                    {
                        //新增的权限，岗位有，就不需要处理
                    }
                }
            }

            //遍历用户原来的权限，但新的列表中没有选中的
            foreach (var obj in user.UserMenuExtendDict.Values.Where(p => p.OrganizationId == request.OrganizationId && p.IsGranted))
            {
                if (grantedList.Contains(obj.MenuId) == false)
                {
                    if (menuIds.Contains(obj.MenuId))
                    {
                        //如果岗位有权限，而人没有权限了，单独赋值
                        //正常不会出现这种情况，岗位有权限，人不会新赋值权限
                        obj.IsGranted = false;
                        update_list.Add(obj);
                    }
                    else
                    {
                        //岗位和人都没权限，直接删除
                        delete_list.Add(obj);
                    }

                }
            }

            //遍历岗位权限，用户没有，则需要标记
            foreach (var i in menuIds)
            {
                //针对该用户删除的权限
                if (grantedList.Contains(i) == false)
                {
                    var obj = user.UserMenuExtendDict.Values.FirstOrDefault(p => p.OrganizationId == request.OrganizationId && p.MenuId == i);
                    if (obj != null && obj.Id > 0)
                    {
                        //正常岗位有权限，用户不会单独再赋值
                        if (obj.IsGranted)
                        {
                            obj.IsGranted = false;
                            update_list.Add(obj);
                        }
                    }
                    else
                    {
                        var menu = new BmsUserMenuExtend();
                        menu.UserId = request.UserId;
                        menu.OrganizationId = request.OrganizationId;
                        menu.MenuId = i;
                        menu.IsGranted = false;
                        menu.CreationTime = DateTime.Now;
                        menu.CreatorUserId = loginId;

                        add_list.Add(menu);
                    }

                }
            }
            if (add_list.Any() == false && update_list.Any() == false && delete_list.Any() == false)
            {
                result.Code = 0;
                result.Success = true;
                result.Msg = "没有变化";
                result.Data = 0;
            }
            else
            {
                var excuteResp = _bmsUserService.MenuUpdate(request.UserId, add_list, update_list, delete_list);
                if (excuteResp.Code == 0 && excuteResp.Success)
                {
                    result.Code = 0;
                    result.Success = true;
                    result.Data = excuteResp.Data;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取用户授权的系统id集合
        /// </summary>
        public ResponseDto<List<long>> GetUserSystemIds(long userId)
        {
            var result = new ResponseDto<List<long>>();
            result.Data = new List<long>();
            if (userId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _bmsUserService.GetUserSystemIds(userId);
            result.Code = excute.Code;
            result.Data = excute.Data;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 更新用户的授权系统
        /// </summary>
        public ResponseDto<string> UpdateGrantedSystem(List<long> systemIds, long userId, long modifierUserId)
        {
            var result = new ResponseDto<string>();
            if (userId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _bmsUserService.UpdateGrantedSystem(systemIds, userId, modifierUserId, out List<long> addIdList, out List<long> deleteIdList);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 获取用户授权的角色id集合
        /// </summary>
        public ResponseDto<List<long>> GetUserRoleIds(long userId)
        {
            var result = new ResponseDto<List<long>>();
            result.Data = new List<long>();
            if (userId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _bmsUserService.GetUserRoleIds(userId);
            result.Code = excute.Code;
            result.Data = excute.Data;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 更新用户的授权角色
        /// </summary>
        public ResponseDto<string> UpdateGrantedRole(List<long> roleIds, long userId, long modifierUserId)
        {
            var result = new ResponseDto<string>();
            if (userId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _bmsUserService.UpdateGrantedRole(roleIds, userId, modifierUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 获取用户授权的平台id集合
        /// </summary>
        public ResponseDto<List<long>> GetUserPlatformIds(long userId)
        {
            var result = new ResponseDto<List<long>>();
            result.Data = new List<long>();
            if (userId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _bmsUserService.GetUserPlatformIds(userId);
            result.Code = excute.Code;
            result.Data = excute.Data;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 更新用户的授权平台
        /// </summary>
        public ResponseDto<string> UpdateGrantedPlatform(List<long> platformIds, long userId, long modifierUserId)
        {
            var result = new ResponseDto<string>();
            if (userId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _bmsUserService.UpdateGrantedPlatform(platformIds, userId, modifierUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 强制刷新用户字典
        /// </summary>
        public ResponseDto<string> ForceRefreshDict()
        {
            var result = new ResponseDto<string>();
            _bmsUserRepository.ForceRefreshDict();
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台获取用户编辑对象
        /// </summary>
        public ResponseDto<BmsUserDto> GetCurrentUserDetail(long Id)
        {
            var result = new ResponseDto<BmsUserDto>();
            
            var userResp = _bmsUserService.GetDetail(Id);

            if (!(userResp.Success && userResp.Data.Id > 0))
                throw new CustomException("用户不存在", 1);

            var user = userResp.Data;
            result.Data = new BmsUserDto();
            result.Data.Id = user.Id;
            result.Data.LoginName = user.LoginName;
            result.Data.FirstName = user.FirstName;
            result.Data.LastName = user.LastName;
            result.Data.EmailAddress = user.EmailAddress;
            result.Data.Sex = user.Sex;
            result.Data.Mobile = user.Mobile;
            result.Data.Telephone = user.Telephone;
            result.Data.CurrentOrgId = user.PrimaryOrganizationId;
            result.Data.UserRoleList = user.UserRoleDict.Values.ToList();
            result.Data.UserPlatformList = user.UserPlatformDict.Values.ToList();

            if (user.UserOrganizationDict.Values != null && user.UserOrganizationDict.Values.Any())
            {
                var orgsList = new List<BmsOrganizationIdsDto>();
                foreach (var org in user.UserOrganizationDict.Values)
                {
                    var orgs = new BmsOrganizationIdsDto();
                    orgs.OrgId = org.OrganizationId;
                    orgs.Ids = org.OrgIds;
                    orgsList.Add(orgs);
                }
                result.Data.OrgsList = orgsList;
            };
            if (result.Data.OrgsList != null && result.Data.OrgsList.Any())
            {
                foreach (var orgs in result.Data.OrgsList)
                {
                    orgs.DisplayName = _bmsOrganizationRepository.GetOrgIdsName(orgs.Ids);
                }
                if (result.Data.CurrentOrgId <= 0)
                {
                    var organization = result.Data.OrgsList.FirstOrDefault();
                    if (organization != null)
                    {
                        result.Data.CurrentOrgId = organization.OrgId;
                    }
                }
            }

            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
