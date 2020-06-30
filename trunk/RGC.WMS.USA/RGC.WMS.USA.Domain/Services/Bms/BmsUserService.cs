using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using RGC.WMS.USA.Domain.Repositories.Bms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Domain.Services.Bms
{
    public class BmsUserService : IBmsUserService
    {

        private readonly IBmsUserRepository _bmsUserRepository;

        public BmsUserService(
            IBmsUserRepository bmsUserRepository
           )
        {
            _bmsUserRepository = bmsUserRepository;
        }

        public ResponseDo<BmsUserExtend> GetDetail(long userId)
        {
            var result = new ResponseDo<BmsUserExtend>();

            var user = _bmsUserRepository.SingleGet(userId);
            if (user == null || user.Id <= 0)
                throw new CustomException("无效用户名", 5);

            result.Code = 0;
            result.Success = true;
            result.Data = user;
            return result;
        }

        /// <summary>
        /// 后台创建用户
        /// </summary>
        public ResponseDo<string> CreateBmsUser(BmsUserCreateOrUpdateDo createUser, long creatorUserId)
        {
            var result = new ResponseDo<string>();
            var user = new BmsUserExtend();

            createUser.Password = user.CreatePassword(createUser.Password);

            user.FirstName = createUser.FirstName.ToEmpty();
            user.LastName = createUser.LastName.ToEmpty();
            user.EmailAddress = createUser.EmailAddress.ToEmpty();
            user.Sex = createUser.Sex.ToEmpty();
            user.Telephone = createUser.Telephone.ToEmpty();
            user.Mobile = createUser.Mobile.ToEmpty();
            user.Fax = createUser.Fax.ToEmpty();
            user.Wechat = createUser.Wechat.ToEmpty();

            user.StaffId = createUser.StaffId;
            user.Status = BmsUserStatus.Normal;//1;
            user.LoginName = createUser.LoginName.ToEmpty();
            user.CreatorUserId = creatorUserId;
            user.CreationTime = DateTime.Now;
            user.Password = createUser.Password.ToEmpty();

            var list = new List<Entities.Bms.BmsUserMenuExtend>();
            foreach (var menuId in createUser.SystemMenuIds)
            {
                var _system = new Entities.Bms.BmsUserMenuExtend();
                _system.MenuId = menuId;
                _system.MenuType = BmsMenuType.System;
                _system.IsGranted = true;
                _system.CreatorUserId = creatorUserId;
                _system.CreationTime = DateTime.Now;
                list.Add(_system);
            }

            var excute = _bmsUserRepository.SingleInsert(user, list);
            if (!excute)
                throw new CustomException("用户新增失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台更新用户信息
        /// </summary>
        public ResponseDo<string> UpdateBmsUser(BmsUserCreateOrUpdateDo updateUser, long modifierUserId)
        {
            var result = new ResponseDo<string>();
            var user = _bmsUserRepository.SingleGet(updateUser.Id);
            if (user == null || user.Id <= 0)
                throw new CustomException("用户不存在", 5);
            if (_bmsUserRepository.IsLoginNameExists(updateUser.LoginName, updateUser.Id))
                throw new CustomException("用户名已存在", 1);

            user.LoginName = updateUser.LoginName.ToEmpty();
            user.FirstName = updateUser.FirstName.ToEmpty();
            user.LastName = updateUser.LastName.ToEmpty();
            user.EmailAddress = updateUser.EmailAddress.ToEmpty();
            user.Sex = updateUser.Sex;
            user.Mobile = updateUser.Mobile.ToEmpty();
            user.Telephone = updateUser.Telephone.ToEmpty();
            user.Fax = updateUser.Fax.ToEmpty();
            user.Wechat = updateUser.Wechat.ToEmpty();
            user.LastModifierUserId = modifierUserId;
            user.LastModificationTime = DateTime.Now;

            var updateList = new List<BmsUserMenuExtend>();
            var addList = new List<BmsUserMenuExtend>();

            var excute = _bmsUserRepository.SingleUpdate(user, addList, updateList);
            if (!excute)
                throw new CustomException("用户数据更新失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }


        /// <summary>
        /// 后台更新用户状态
        /// </summary>
        public ResponseDo<string> UpdateBmsUserStatus(BmsUserCreateOrUpdateDo updateUser, long ModifierUserId)
        {
            var result = new ResponseDo<string>();
            var user = new BmsUserExtend();
            user.Id = updateUser.Id;
            user.Status = updateUser.Status;
            user.LastModifierUserId = ModifierUserId;
            user.LastModificationTime = DateTime.Now;
            var excute = _bmsUserRepository.SingleUpdateStatus(user);
            if (excute <= 0)
                throw new CustomException("用户数据更新失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台重置别人的密码，不需要校验旧密码
        /// </summary>
        public ResponseDo<string> ChangeBmsUserPassword(Tuple<long, string, string> input, long ModifierUserId)
        {
            var result = new ResponseDo<string>();
            //if (string.IsNullOrEmpty(input.Item3))
            //    throw new CustomException("密码数据不能空", 1);
            var user = new BmsUserExtend();
            user.Id = input.Item1;
            user.Password = user.CreatePassword(input.Item2);
            user.LastModifierUserId = ModifierUserId;
            user.LastModificationTime = DateTime.Now;
            var excute = _bmsUserRepository.UpdatePassword(user);
            if (!excute)
                throw new CustomException("用户密码修改失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 用户伪删除
        /// </summary>
        public ResponseDo<string> DeleteBmsUser(long userId, long ModifierUserId)
        {
            var result = new ResponseDo<string>();
            var user = _bmsUserRepository.SingleGet(userId);
            if (user.Id != userId)
                throw new CustomException("数据错误", 1);

            user.IsDeleted = true;
            user.DeleterUserId = ModifierUserId;
            user.DeletionTime = DateTime.Now;
            var excute = _bmsUserRepository.SigleDelete(user, user.LoginName);
            if (!excute)
                throw new CustomException("用户删除失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        #region 组织架构相关

        public int AddOrganization(List<BmsUserOrganization> request)
        {
            var excute = _bmsUserRepository.AddOrganization(request);
            return excute;
        }


        public ResponsePageDo<BmsUserExtend> GetOrganizationUserList(long orgId, string key, int PageSize, int currentPage)
        {
            var result = new ResponsePageDo<BmsUserExtend>();
            result.Data = _bmsUserRepository.GetOrganizationUserList(orgId, key, PageSize, currentPage, out int total);
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)total;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)total / PageSize);
                result.Page.PageSize = PageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = result.Data.Count;
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponsePageDo<BmsUserExtend> GetOtherOrganizationUserList(long orgId, string key, int PageSize, int currentPage)
        {
            var result = new ResponsePageDo<BmsUserExtend>();
            result.Data = _bmsUserRepository.GetOtherOrganizationUserList(orgId, key, PageSize, currentPage, out int total);
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)total;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)total / PageSize);
                result.Page.PageSize = PageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = result.Data.Count;
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        /// <summary>
        /// 移除组织机构
        /// </summary>
        public ResponseDo<string> RemoveOrganization(long userId, long orgId, long loginId)
        {
            var result = new ResponseDo<string>();

            int execute = _bmsUserRepository.RemoveOrganization(userId, orgId, loginId);
            if (execute <= 0)
                throw new CustomException("移除组织机构失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }
        #endregion
        public ResponseDo<int> MenuUpdate(Int64 userId, List<Entities.Bms.BmsUserMenuExtend> add_list, List<Entities.Bms.BmsUserMenuExtend> update_list, List<Entities.Bms.BmsUserMenuExtend> delete_list)
        {
            var result = new ResponseDo<int>();
            //新增列表
            var execute = _bmsUserRepository.MenuUpdate(userId, add_list, update_list, delete_list);
            if (execute <= 0)
                throw new CustomException("菜单更新失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDo<List<Entities.Bms.BmsUserMenuExtend>> MenuListAllGet(long UserId)
        {
            var result = new ResponseDo<List<BmsUserMenuExtend>>();
            result.Data = _bmsUserRepository.MenuListAllGet(UserId);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        #region 系统相关
        /// <summary>
        /// 获取用户已拥有授权的所有系统id
        /// </summary>
        public ResponseDo<List<long>> GetUserSystemIds(long userId)
        {
            var result = new ResponseDo<List<long>>();
            var user = _bmsUserRepository.SingleGet(userId);
            if (user == null || user.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            if (user.UserSystemDict.Count > 0)
                result.Data = user.UserSystemDict.Keys.ToList();
            else
                result.Data = new List<long>();
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 更新用户授权系统
        /// </summary>
        public ResponseDo<string> UpdateGrantedSystem(List<long> systemIds, long userId, long modifierUserId, out List<long> addIdList, out List<long> deleteIdList)
        {
            var result = new ResponseDo<string>();
            addIdList = new List<long>();
            deleteIdList = new List<long>();
            var user = _bmsUserRepository.SingleGet(userId);
            if (user == null || user.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            var addList = new List<BmsUserSystem>();
            var deleteList = new List<BmsUserSystem>();

            if (user.UserSystemDict.Count > 0)
            {
                foreach (var system in user.UserSystemDict.Values)
                {
                    if (!systemIds.Contains(system.SystemId))
                    {
                        deleteList.Add(system);
                    }
                }
            }
            foreach (var systemId in systemIds)
            {
                if (!user.UserSystemDict.ContainsKey(systemId))
                {
                    var system = new BmsUserSystem();
                    system.SystemId = systemId;
                    system.UserId = userId;
                    system.CreationTime = DateTime.Now;
                    system.CreatorUserId = modifierUserId;
                    addList.Add(system);
                }
            }

            result.Code = 0;
            result.Success = true;
            if (deleteList.Count > 0 || addList.Count > 0)
            {
                var excute = _bmsUserRepository.UpdateSystem(userId, addList, deleteList);
                if (!excute)
                    throw new CustomException("用户授权系统更新失败", 1);

                addIdList = addList.Select(p => p.SystemId).ToList();
                deleteIdList = deleteList.Select(p => p.SystemId).ToList();
            }           
            return result;
        }
        #endregion

        #region 角色相关
        /// <summary>
        /// 获取用户已拥有授权的所有角色id
        /// </summary>
        public ResponseDo<List<long>> GetUserRoleIds(long userId)
        {
            var result = new ResponseDo<List<long>>();            
            var user = _bmsUserRepository.SingleGet(userId);
            if (user == null || user.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            if (user.UserRoleDict.Count > 0)
                result.Data = user.UserRoleDict.Keys.ToList();
            else
                result.Data = new List<long>();
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 更新用户授权角色
        /// </summary>
        public ResponseDo<string> UpdateGrantedRole(List<long> roleIds, long userId, long modifierUserId)
        {
            var result = new ResponseDo<string>();
            var user = _bmsUserRepository.SingleGet(userId);
            if (user == null || user.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            var addList = new List<BmsUserRole>();
            var deleteList = new List<BmsUserRole>();

            if (user.UserRoleDict.Count > 0)
            {
                foreach (var role in user.UserRoleDict.Values)
                {
                    if (!roleIds.Contains(role.RoleId))
                    {
                        deleteList.Add(role);
                    }
                }
            }
            foreach (var roleId in roleIds)
            {
                if (!user.UserRoleDict.ContainsKey(roleId))
                {
                    var role = new BmsUserRole();
                    role.RoleId = roleId;
                    role.UserId = userId;
                    role.CreationTime = DateTime.Now;
                    role.CreatorUserId = modifierUserId;
                    addList.Add(role);
                }
            }

            result.Code = 0;
            result.Success = true;
            if (deleteList.Count > 0 || addList.Count > 0)
            {
                var excute = _bmsUserRepository.UpdateRole(userId, addList, deleteList);
                if (!excute)
                    throw new CustomException("用户授权角色更新失败", 1);
            }
            return result;
        }

        /// <summary>
        /// 获取拥有角色授权的全部用户
        /// </summary>
        public ResponseDo<List<BmsUserExtend>> GetRoleGrantedUsers(long roleId, int currentPage, out int count)
        {
            var result = new ResponseDo<List<BmsUserExtend>>();
            result.Data = new List<BmsUserExtend>();
            count = 0;
            var list = _bmsUserRepository.AllGet();
            if (list.Count > 0)
            {
                var _list = list.Where(p => p.UserRoleDict.ContainsKey(roleId)).ToList();
                if (_list.Count > 0)
                {
                    count = _list.Count;
                    var pageSize = 10;
                    _list = _list.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                    result.Data = _list;

                }
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }
        #endregion

        #region 平台相关
        /// <summary>
        /// 获取用户已拥有授权的所有平台id
        /// </summary>
        public ResponseDo<List<long>> GetUserPlatformIds(long userId)
        {
            var result = new ResponseDo<List<long>>();
            
            var user = _bmsUserRepository.SingleGet(userId);
            if (user == null || user.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            if (user.UserPlatformDict.Count > 0)
                result.Data = user.UserPlatformDict.Keys.ToList();
            else
                result.Data = new List<long>();
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 更新用户授权平台
        /// </summary>
        public ResponseDo<string> UpdateGrantedPlatform(List<long> platformIds, long userId, long modifierUserId)
        {
            var result = new ResponseDo<string>();
            var user = _bmsUserRepository.SingleGet(userId);
            if (user == null || user.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            var addList = new List<BmsUserPlatform>();
            var deleteList = new List<BmsUserPlatform>();

            if (user.UserPlatformDict.Count > 0)
            {
                foreach (var platform in user.UserPlatformDict.Values)
                {
                    if (!platformIds.Contains(platform.PlatformId))
                    {
                        deleteList.Add(platform);
                    }
                }
            }
            foreach (var platformId in platformIds)
            {
                if (!user.UserPlatformDict.ContainsKey(platformId))
                {
                    var platform = new BmsUserPlatform();
                    platform.PlatformId = platformId;
                    platform.UserId = userId;
                    platform.CreationTime = DateTime.Now;
                    platform.CreatorUserId = modifierUserId;
                    addList.Add(platform);
                }
            }

            result.Code = 0;
            result.Success = true;
            if (deleteList.Count > 0 || addList.Count > 0)
            {
                var excute = _bmsUserRepository.UpdatePlatform(userId, addList, deleteList);
                if(!excute)
                    throw new CustomException("用户授权平台更新失败", 1);
            }
            return result;
        }

        /// <summary>
        /// 获取拥有平台授权的全部用户
        /// </summary>
        public ResponseDo<List<BmsUserExtend>> GetPlatformGrantedUsers(long platformId, int currentPage, out int count)
        {
            var result = new ResponseDo<List<BmsUserExtend>>();
            result.Data = new List<BmsUserExtend>();
            count = 0;
            var list = _bmsUserRepository.AllGet();
            if (list.Count > 0)
            {
                var _list = list.Where(p => p.UserPlatformDict.ContainsKey(platformId)).ToList();
                if (_list.Count > 0)
                {
                    count = _list.Count;
                    var pageSize = 10;
                    _list = _list.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                    result.Data = _list;

                }
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }
        #endregion
    }
}
