using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Bms
{
    public class BmsRoleRepository : RepositoryBase<BmsRole>, IBmsRoleRepository
    {
        private static IUnitOfWork _unitOfWork;

        public BmsRoleRepository(DbContext context,
            IUnitOfWork unitOfWork
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// 锁对象，确保单线程
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// 角色信息数据字典，索引RoleId
        /// </summary>
        public static Dictionary<long, BmsRole> RoleDict;

        public void RefreshRoleDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (RoleDict == null || RoleDict.Count == 0)
                {
                    RoleDict = new Dictionary<Int64, BmsRole>();
                    RoleDict = GetRoleDictFromDB();
                }
            }
        }

        /// <summary>
        /// 从数据库中获取全部管理员
        /// </summary>
        public Dictionary<Int64, BmsRole> GetRoleDictFromDB()
        {
            var dict = new Dictionary<Int64, BmsRole>();

            var list = TableNoTracking.ToList();

            if (list != null && list.Count() > 0)
            {
                foreach (var obj in list)
                {
                    //obj.RoleMenuDict = roleMenuList.Where(p => p.roleId == obj.id).ToDictionary(p => p.menuId);
                    if (dict.Keys.Contains(obj.Id) == false)
                    {
                        dict.Add(obj.Id, obj);
                    }
                }

            }

            return dict;
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        public void ForceRefreshDict()
        {
            ///加锁，保证同时只有一个线程访问 
            lock (_locker)
            {
                RoleDict = new Dictionary<Int64, BmsRole>();
                RoleDict = GetRoleDictFromDB();
            }
        }

        public bool IsNameExists(string name)
        {
            if (RoleDict == null || RoleDict.Count == 0)
            {
                RefreshRoleDict();
            }
            if (RoleDict.Values.Count(p => p.Name.ToLower() == name.Trim().ToLower() && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }
        public bool IsNameExists(string name, long roleId)
        {
            if (RoleDict == null || RoleDict.Count == 0)
            {
                RefreshRoleDict();
            }
            if (RoleDict.Values.Count(p => p.Name.ToLower() == name.Trim().ToLower() && p.Id != roleId && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 单个获取
        /// </summary>
        public BmsRole SingleGet(Int64 id)
        {
            var obj = new BmsRole();

            if (RoleDict == null || RoleDict.Count == 0)
            {
                RefreshRoleDict();
            }
            if (RoleDict.Keys.Contains(id))
            {
                obj = RoleDict.Values.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();
            }
            return obj;
        }


        /// <summary>
        /// 单个新增
        /// </summary>
        public BmsRole SingleAdd(BmsRole request)
        {
            var result = new BmsRole();

            Insert(request);
            var _excute = _unitOfWork.SaveChanges();
            if (_excute > 0)
            {
                //新增成功
                if (RoleDict == null || RoleDict.Count == 0)
                {
                    RefreshRoleDict();
                }
                lock (_locker)
                {
                    if (RoleDict.Keys.Contains(request.Id) == false)
                    {
                        RoleDict.Add(request.Id, request);
                    }
                }
            }

            return request;
        }

        /// <summary>
        /// 单个更新数据库
        /// </summary>
        public int SingleUpdate(BmsRole obj)
        {
            Update(obj);
            var result = _unitOfWork.SaveChanges();
            if (result > 0)
            {
                if (RoleDict == null || RoleDict.Count == 0)
                {
                    RefreshRoleDict();
                }
                lock (_locker)
                {
                    if (RoleDict.Keys.Contains(obj.Id))
                    {
                        RoleDict[obj.Id] = obj;
                    }
                    else
                    {
                        RefreshRoleDict();
                    }
                }
            }
            return result;

        }

        /// <summary>
        /// 单个删除，伪删除
        /// </summary>
        public int SingleDelete(BmsRole role)
        {
            Update(role, p => p.IsDeleted,
               p => p.DeleterUserId,
               p => p.DeletionTime);
            var excute = _unitOfWork.SaveChanges();
            if (excute == 1)
            {
                if (RoleDict == null || RoleDict.Count == 0)
                {
                    RefreshRoleDict();
                }
                else
                {
                    if (RoleDict.ContainsKey(role.Id))
                    {
                        var _role = RoleDict[role.Id];
                        _role.IsDeleted = role.IsDeleted;
                        _role.DeleterUserId = role.DeleterUserId;
                        _role.DeletionTime = role.DeletionTime;
                    }
                }
            }

            return excute;
        }

        /// <summary>
        /// 全部查询
        /// </summary>
        public List<BmsRole> AllListGet(string key)
        {
            var result = new List<BmsRole>();
            if (RoleDict == null || RoleDict.Count == 0)
            {
                RefreshRoleDict();
            }
            if (RoleDict.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = key.Trim().ToLower();
                }

                result = RoleDict.Values.Where(p => p.IsDeleted == false)
                    .Where(p => string.IsNullOrEmpty(key) ? true : (p.Name.ToLower().Contains(key) || p.DisplayName.ToLower().Contains(key)))
                    .OrderBy(p => p.Id).ToList();
            }
            return result;
        }
    }
}
