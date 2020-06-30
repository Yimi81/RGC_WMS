using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Bms
{
    public class BmsUserRepository : RepositoryBase<BmsUserExtend>, IBmsUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<BmsUserMenuExtend> _bmsUserMenu;
        private IRepository<BmsUserOrganization> _bmsUserOrganization;
        private IRepository<BmsUserSystem> _bmsUserSystem;
        private IRepository<BmsUserRole> _bmsUserRole;
        private IRepository<BmsUserPlatform> _bmsUserPlatform;

        public BmsUserRepository(DbContext context,
            IUnitOfWork unitOfWork,
            IRepository<BmsUserMenuExtend> bmsUserMenum,
            IRepository<BmsUserOrganization> bmsUserOrganization,
            IRepository<BmsUserSystem> bmsUserSystem,
            IRepository<BmsUserRole> bmsUserRole,
            IRepository<BmsUserPlatform> bmsUserPlatform
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _bmsUserOrganization = bmsUserOrganization;
            _bmsUserMenu = bmsUserMenum;
            _bmsUserSystem = bmsUserSystem;
            _bmsUserRole = bmsUserRole;
            _bmsUserPlatform = bmsUserPlatform;
        }

        private static readonly object _locker = new object();
        private static Dictionary<long, BmsUserExtend> UserDict;

        #region 数据字典
        /// <summary>
        /// 后台用户数据字典
        /// </summary>
        public Dictionary<long, BmsUserExtend> GetUserDictFromDB()
        {
            var dict = new Dictionary<long, BmsUserExtend>();

            var list = TableNoTracking.ToList();
            var userOrganizationList = _bmsUserOrganization.TableNoTracking.ToList();

            if (list != null && list.Count > 0)
            {
                var menuList = _bmsUserMenu.TableNoTracking;
                var systemList = _bmsUserSystem.TableNoTracking;
                var roleList = _bmsUserRole.TableNoTracking;
                var platform = _bmsUserPlatform.TableNoTracking;
                foreach (var user in list)
                {
                    user.UserMenuExtendDict = menuList.Where(p => p.UserId == user.Id).ToDictionary(p => p.Id);
                    user.UserOrganizationDict = userOrganizationList.Where(p => p.UserId == user.Id).ToDictionary(p => p.OrganizationId);
                    user.UserSystemDict = systemList.Where(p => p.UserId == user.Id).ToDictionary(p => p.SystemId);
                    user.UserRoleDict = roleList.Where(p => p.UserId == user.Id).ToDictionary(p => p.RoleId);
                    user.UserPlatformDict = platform.Where(p => p.UserId == user.Id).ToDictionary(p => p.PlatformId);
                }
                dict = list.ToDictionary(p => p.Id);
            }

            return dict;
        }

        /// <summary>
        /// 更新字典
        /// </summary>
        public void UpdateUserDict(BmsUserExtend user)
        {
            //加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (!UserDict.ContainsKey(user.Id))
                {
                    UserDict.Add(user.Id, user);
                }
                else
                {
                    UserDict[user.Id] = user;
                }
            }
        }

        /// <summary>
        /// 刷新数据字典
        /// </summary>
        public void RefreshUserDict()
        {
            //加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (UserDict == null || UserDict.Count == 0)
                {
                    UserDict = new Dictionary<long, BmsUserExtend>();
                    UserDict = GetUserDictFromDB();
                }
            }
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        public void ForceRefreshDict()
        {
            ///加锁，保证同时只有一个线程访问 
            lock (_locker)
            {
                UserDict = new Dictionary<Int64, BmsUserExtend>();
                UserDict = GetUserDictFromDB();
            }
        }
        #endregion

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        public BmsUserExtend SingleGet(long Id)
        {
            var user = new BmsUserExtend();

            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }
            if (UserDict.ContainsKey(Id))
            {
                user = UserDict[Id];
                if (user.IsDeleted)
                    user = new BmsUserExtend();
            }
            return user;
        }
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        public BmsUserExtend SingleGet(string loginName)
        {
            var user = new BmsUserExtend();

            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }

            user = UserDict.Values.FirstOrDefault(p => p.LoginName == loginName);

            return user;
        }

        /// <summary>
        /// 根据用户主键集合，获取对应的用户集合
        /// MeridianGo 2020/06/24
        /// </summary>
        public List<BmsUserExtend> GetAllUserByKeys(List<long> keys)
        {
            if (UserDict == null || UserDict.Count == 0)
                RefreshUserDict();
            var lstUser = UserDict.Values.Where(x => keys.Contains(x.Id)).ToList();
            return lstUser;
        }

        public bool IsLoginNameExists(string loginName)
        {

            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }
            if (UserDict.Values.Count(p => p.LoginName.ToEmpty().ToLower() == loginName.ToEmpty().ToLower() && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsLoginNameExists(string loginName, long UserId)
        {

            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }
            if (UserDict.Values.Count(p => p.LoginName.ToEmpty().ToLower() == loginName.ToEmpty().ToLower() && UserId != p.Id && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsEmailAddressExists(string emailAddress)
        {

            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }
            if (UserDict.Values.Count(p => p.EmailAddress.ToEmpty().ToLower() == emailAddress.ToEmpty().ToLower() && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取全部用户列表
        /// </summary>
        public List<BmsUserExtend> AllGet()
        {
            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }
            return UserDict.Values.Where(p => !p.IsDeleted).ToList();
        }

        /// <summary>
        /// 新增用户数据
        /// </summary>
        public bool SingleInsert(BmsUserExtend user, List<BmsUserMenuExtend> systemList)
        {
            var result = false;
            if (string.IsNullOrEmpty(user.Sex))
                user.Sex = "u";
            Insert(user);
            var _excute = _unitOfWork.SaveChanges();
            if (_excute == 1)
            {
                foreach (var system in systemList)
                {
                    system.UserId = user.Id;
                    _bmsUserMenu.Insert(system);
                }
                if (systemList.Any())
                {
                    _excute = _unitOfWork.SaveChanges();
                }
                if (_excute > 0)
                {
                    if (UserDict == null || UserDict.Count == 0)
                    {
                        RefreshUserDict();
                    }
                    else
                    {
                        user.UserMenuExtendDict = systemList.ToDictionary(p => p.Id);
                        UpdateUserDict(user);
                    }
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 后台部分更新用户数据
        /// </summary>
        public bool SingleUpdate(BmsUserExtend user, List<BmsUserMenuExtend> addList, List<BmsUserMenuExtend> updateList)
        {
            var result = false;
            Update(user);
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                result = true;
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                else
                {
                    if (!UserDict.ContainsKey(user.Id))
                        UserDict.Add(user.Id, user);
                    else
                        UserDict[user.Id] = user;
                }
            }
            return result;
        }


        /// <summary>
        /// 后台更新用户状态
        /// </summary>
        public int SingleUpdateStatus(BmsUserExtend user)
        {
            Update(user, p => p.Status,
                p => p.LastModificationTime,
                p => p.LastModifierUserId
                );
            var excute = _unitOfWork.SaveChanges();
            if (excute == 1)
            {
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                else
                {
                    if (UserDict.ContainsKey(user.Id))
                    {
                        var _user = UserDict[user.Id];
                        _user.Status = user.Status;
                        _user.LastModificationTime = user.LastModificationTime;
                        _user.LastModifierUserId = user.LastModifierUserId;
                    }

                }
            }
            return excute;
        }

        /// <summary>
        /// 后台更新用户密码
        /// </summary>
        public bool UpdatePassword(BmsUserExtend user)
        {
            var result = false;
            Update(user, p => p.Password,
                p => p.LastModificationTime,
                p => p.LastModifierUserId
                );
            var excute = _unitOfWork.SaveChanges();
            if (excute == 1)
            {
                result = true;
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                else
                {
                    if (UserDict.ContainsKey(user.Id))
                    {
                        var _user = UserDict[user.Id];
                        _user.Password = user.Password;
                        _user.LastModificationTime = user.LastModificationTime;
                        _user.LastModifierUserId = user.LastModifierUserId;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除用户
        /// </summary>
        public bool SigleDelete(BmsUserExtend user, string operatorName)
        {
            user.DeletionTime = DateTime.Now;
            Update(user,
                p => p.IsDeleted,
                p => p.DeleterUserId,
                p => p.DeletionTime);
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }

                if (UserDict.ContainsKey(user.Id))
                {
                    var _user = UserDict[user.Id];
                    _user.IsDeleted = user.IsDeleted;
                    _user.DeleterUserId = user.DeleterUserId;
                    _user.DeletionTime = user.DeletionTime;
                }
            }
            return excute > 0;
        }

        /// <summary>
        /// 获取用户关联的所有菜单
        /// </summary>
        public List<BmsUserMenuExtend> MenuListAllGet(long UserId)
        {
            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }
            var result = new List<BmsUserMenuExtend>();
            if (UserDict.ContainsKey(UserId))
            {
                var menuList = UserDict[UserId].UserMenuExtendDict.Values;
                if (menuList != null && menuList.Count() > 0)
                    result = menuList.ToList();
            }
            return result;
        }

        /// <summary>
        /// 更新用户菜单授权
        /// </summary>
        public bool UpdateMenu(long UserId, List<BmsUserMenuExtend> addList, List<BmsUserMenuExtend> updateList)
        {
            var result = false;
            foreach (var system in addList)
            {
                _bmsUserMenu.Insert(system);
            }
            foreach (var system in updateList)
            {
                _bmsUserMenu.Update(system);
            }

            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                result = true;
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                else
                {
                    if (UserDict.ContainsKey(UserId))
                    {
                        var _user = UserDict[UserId];
                        foreach (var system in addList)
                        {
                            if (!_user.UserMenuExtendDict.ContainsKey(system.Id))
                            {
                                _user.UserMenuExtendDict.Add(system.Id, system);
                            }
                        }
                        foreach (var system in updateList)
                        {
                            if (_user.UserMenuExtendDict.ContainsKey(system.Id))
                            {
                                _user.UserMenuExtendDict[system.Id] = system;
                            }
                        }
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 分页查询用户列表
        /// </summary>
        public List<BmsUserExtend> PageQuery(SearchFilterDo searchFilter, out int count)
        {
            var result = new List<BmsUserExtend>();
            var userList = AllGet();
            var total = 0;
            var list = new List<BmsUserExtend>();
            if (userList.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchFilter.SearchKey))
                {
                    searchFilter.SearchKey = searchFilter.SearchKey.Trim().ToLower();
                    userList = userList.Where(p => p.Id.ToString() == searchFilter.SearchKey
                    || p.LoginName.ToLower().Contains(searchFilter.SearchKey)).ToList();
                }
                total = userList.Count();
                list = userList.Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

            }
            count = total;
            result = list;

            return result;
        }

        public int AddOrganization(List<BmsUserOrganization> request)
        {
            foreach (var model in request)
            {
                _bmsUserOrganization.Insert(model);
            }
            var excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                lock (_locker)
                {
                    foreach (var model in request)
                    {
                        if (UserDict.Keys.Contains(model.UserId))
                        {
                            if (UserDict[model.UserId].UserOrganizationDict == null)
                            {
                                UserDict[model.UserId].UserOrganizationDict = new Dictionary<long, BmsUserOrganization>();
                                UserDict[model.UserId].UserOrganizationDict.Add(model.OrganizationId, model);
                            }
                            else
                            {
                                UserDict[model.UserId].UserOrganizationDict.Add(model.OrganizationId, model);

                            }
                        }
                        else
                        {
                            RefreshUserDict();
                        }
                    }

                }
            }

            return excute;
        }

        /// <summary>
        /// 某分类下user列表
        /// </summary>
        public List<BmsUserExtend> GetOrganizationUserList(long orgId, string key, int pageSize, int currentPage, out int totalCount)
        {
            var result = new List<BmsUserExtend>();
            totalCount = 0;
            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }
            if (UserDict.Any())
            {
                if (key == null)
                {
                    key = "";
                }
                var UserIds = new List<long>();
                UserIds = _bmsUserOrganization.TableNoTracking.Where(p => p.OrganizationId == orgId)?.Select(p => p.UserId).ToList();
                var total = UserDict.Values.Count(p => p.IsDeleted == false && UserIds.Contains((int)p.Id));

                var list = UserDict.Values.Where(p => p.IsDeleted == false && UserIds.Contains((int)p.Id)).Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                result = list;

                totalCount = (int)total;
            }
            return result;
        }

        /// <summary>
        /// 非该组织架构下user列表
        /// </summary>
        public List<BmsUserExtend> GetOtherOrganizationUserList(long orgId, string key, int pageSize, int currentPage, out int totalCount)
        {
            var result = new List<BmsUserExtend>();
            totalCount = 0;
            if (UserDict == null || UserDict.Count == 0)
            {
                RefreshUserDict();
            }
            if (UserDict.Any())
            {
                if (key == null)
                {
                    key = "";
                }
                var UserIds = _bmsUserOrganization.TableNoTracking.Where(p => p.OrganizationId == orgId).Select(p => p.UserId).ToList();
                var total = UserDict.Values.Count(p => p.IsDeleted == false && !UserIds.Contains((int)p.Id));

                var list = UserDict.Values.Where(p => p.IsDeleted == false && !UserIds.Contains((int)p.Id)).Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                result = list;

                totalCount = (int)total;
            }
            return result;
        }
        
        /// <summary>
        /// 移除用户组织架构
        /// </summary>
        public int RemoveOrganization(long UserId, long orgId, long loginId)
        {
            var request = new BmsUserOrganization();
            var temp = _bmsUserOrganization.TableNoTracking.Where(p => p.UserId == UserId && p.OrganizationId == orgId);
            if (temp.Count() > 0)
                request = temp.FirstOrDefault();
            _bmsUserOrganization.Delete(request);
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0 && request.Id > 0)
            {
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                lock (_locker)
                {
                    if (UserDict.Keys.Contains(request.UserId))
                    {
                        if (UserDict[request.UserId].UserOrganizationDict != null)
                        {
                            UserDict[request.UserId].UserOrganizationDict.Remove(request.OrganizationId);
                        }

                    }
                    else
                    {
                        RefreshUserDict();
                    }
                }
            }

            return excute;
        }

        public int MenuUpdate(Int64 userId, List<BmsUserMenuExtend> add_list, List<BmsUserMenuExtend> update_list, List<BmsUserMenuExtend> delete_list)
        {
            var result = 0;
            using (var trans = BeginTransaction())
            {
                try
                {
                    if (add_list != null && add_list.Count > 0)
                    {
                        foreach (var obj in add_list)
                        {
                            if (obj.UserId == userId && obj.OrganizationId > 0 && obj.MenuId > 0)
                                _bmsUserMenu.Insert(obj);
                        }
                    }
                    if (update_list != null && update_list.Count > 0)
                    {
                        foreach (var obj in update_list)
                        {
                            if (obj.UserId == userId && obj.OrganizationId > 0 && obj.MenuId > 0 && obj.Id > 0)
                                _bmsUserMenu.Update(obj);

                        }
                    }
                    if (delete_list != null && delete_list.Count > 0)
                    {
                        foreach (var obj in delete_list)
                        {
                            if (obj.UserId == userId && obj.OrganizationId > 0 && obj.MenuId > 0 && obj.Id > 0)
                                _bmsUserMenu.Delete(obj);

                        }
                    }

                    result = _unitOfWork.SaveChanges();
                    trans.Commit();

                    if (result > 0)
                    {
                        lock (_locker)
                        {
                            if (UserDict == null || UserDict.Count == 0)
                            {
                                RefreshUserDict();
                            }
                            else
                            {
                                if (UserDict.Keys.Contains(userId))
                                {
                                    var user = UserDict[userId];
                                    if (add_list != null && add_list.Count > 0)
                                    {
                                        foreach (var obj in add_list)
                                        {
                                            //if (user.UserMenuExtendDict.Keys.Contains(obj.MenuId) == false)
                                            //    user.UserMenuExtendDict.Add(obj.MenuId, obj);
                                            if (user.UserMenuExtendDict.Values.Count(p => p.OrganizationId == obj.OrganizationId && p.MenuId == obj.MenuId) == 0)
                                                user.UserMenuExtendDict.Add(obj.Id, obj);
                                        }
                                    }
                                    if (update_list != null && update_list.Count > 0)
                                    {
                                        foreach (var obj in update_list)
                                        {
                                            //if (user.UserMenuExtendDict.Keys.Contains(obj.MenuId))
                                            //    user.UserMenuExtendDict[obj.MenuId] = obj; 
                                            if (user.UserMenuExtendDict.Keys.Contains(obj.Id))
                                                user.UserMenuExtendDict[obj.Id] = obj;
                                        }
                                    }
                                    if (delete_list != null && delete_list.Count > 0)
                                    {
                                        foreach (var obj in delete_list)
                                        {
                                            //if (user.UserMenuExtendDict.Keys.Contains(obj.MenuId))
                                            //    user.UserMenuExtendDict.Remove(obj.MenuId);
                                            if (user.UserMenuExtendDict.Keys.Contains(obj.Id))
                                                user.UserMenuExtendDict.Remove(obj.Id);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
            return result;
        }

        /// <summary>
        /// 更新用户授权系统
        /// </summary>
        public bool UpdateSystem(long userId, List<BmsUserSystem> addList, List<BmsUserSystem> deleteList)
        {
            var result = false;
            foreach (var system in addList)
            {
                _bmsUserSystem.Insert(system);
            }
            foreach (var system in deleteList)
            {
                _bmsUserSystem.Delete(system);
            }
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                result = true;
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                else
                {
                    if (UserDict.ContainsKey(userId))
                    {
                        var _user = UserDict[userId];
                        foreach (var system in addList)
                        {
                            if (!_user.UserSystemDict.ContainsKey(system.SystemId))
                            {
                                _user.UserSystemDict.Add(system.SystemId, system);
                            }
                        }
                        foreach (var system in deleteList)
                        {
                            if (_user.UserSystemDict.ContainsKey(system.SystemId))
                            {
                                _user.UserSystemDict.Remove(system.SystemId);
                            }
                        }
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 更新用户授权角色
        /// </summary>
        public bool UpdateRole(long userId, List<BmsUserRole> addList, List<BmsUserRole> deleteList)
        {
            var result = false;
            foreach (var role in addList)
            {
                _bmsUserRole.Insert(role);
            }
            foreach (var role in deleteList)
            {
                _bmsUserRole.Delete(role);
            }
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                result = true;
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                else
                {
                    if (UserDict.ContainsKey(userId))
                    {
                        var _user = UserDict[userId];
                        foreach (var role in addList)
                        {
                            if (!_user.UserRoleDict.ContainsKey(role.RoleId))
                            {
                                _user.UserRoleDict.Add(role.RoleId, role);
                            }
                        }
                        foreach (var role in deleteList)
                        {
                            if (_user.UserRoleDict.ContainsKey(role.RoleId))
                            {
                                _user.UserRoleDict.Remove(role.RoleId);
                            }
                        }
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 更新用户授权平台
        /// </summary>
        public bool UpdatePlatform(long userId, List<BmsUserPlatform> addList, List<BmsUserPlatform> deleteList)
        {
            var result = false;
            foreach (var platform in addList)
            {
                _bmsUserPlatform.Insert(platform);
            }
            foreach (var platform in deleteList)
            {
                _bmsUserPlatform.Delete(platform);
            }
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                result = true;
                if (UserDict == null || UserDict.Count == 0)
                {
                    RefreshUserDict();
                }
                else
                {
                    if (UserDict.ContainsKey(userId))
                    {
                        var _user = UserDict[userId];
                        foreach (var platform in addList)
                        {
                            if (!_user.UserPlatformDict.ContainsKey(platform.PlatformId))
                            {
                                _user.UserPlatformDict.Add(platform.PlatformId, platform);
                            }
                        }
                        foreach (var platform in deleteList)
                        {
                            if (_user.UserPlatformDict.ContainsKey(platform.PlatformId))
                            {
                                _user.UserPlatformDict.Remove(platform.PlatformId);
                            }
                        }
                    }

                }
            }
            return result;
        }
    }
}
