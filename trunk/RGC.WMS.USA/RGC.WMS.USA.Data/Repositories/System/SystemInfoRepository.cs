using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System;
using RGC.WMS.USA.Domain.Repositories.System;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.System
{
    public class SystemInfoRepository : RepositoryBase<SystemInfo>, ISystemInfoRepository
    {
        private static IUnitOfWork _unitOfWork;

        public SystemInfoRepository(DbContext context,
            IUnitOfWork unitOfWork
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        private static readonly object _locker = new object();
        private static Dictionary<long, SystemInfo> SystemDict;

        #region 数据字典
        /// <summary>
        /// RGC所有系统数据字典
        /// </summary>
        public Dictionary<long, SystemInfo> GetSystemDictFromDB()
        {
            var dict = new Dictionary<long, SystemInfo>();

            var list = TableNoTracking.ToList();

            if (list != null && list.Count > 0)
            {
                dict = list.ToDictionary(p => p.Id);
            }

            return dict;
        }

        /// <summary>
        /// 更新字典
        /// </summary>
        public void UpdateSystemDict(SystemInfo system)
        {
            //加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (!SystemDict.ContainsKey(system.Id))
                {
                    SystemDict.Add(system.Id, system);
                }
                else
                {
                    SystemDict[system.Id] = system;
                }
            }
        }

        /// <summary>
        /// 刷新数据字典
        /// </summary>
        public void RefreshSystemDict()
        {
            //加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (SystemDict == null || SystemDict.Count == 0)
                {
                    SystemDict = new Dictionary<long, SystemInfo>();
                    SystemDict = GetSystemDictFromDB();
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
                SystemDict = new Dictionary<Int64, SystemInfo>();
                SystemDict = GetSystemDictFromDB();
            }
        }

        #endregion

        public bool IsNameExists(string name)
        {
            if (SystemDict == null || SystemDict.Count == 0)
            {
                RefreshSystemDict();
            }
            if (SystemDict.Values.Count(p => p.Name.ToLower() == name.Trim().ToLower() && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }
        public bool IsNameExists(string name, long systemId)
        {
            if (SystemDict == null || SystemDict.Count == 0)
            {
                RefreshSystemDict();
            }
            if (SystemDict.Values.Count(p => p.Name.ToLower() == name.Trim().ToLower() && p.Id != systemId && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据id获取系统
        /// </summary>
        public SystemInfo SingleGet(long Id)
        {
            var system = new SystemInfo();

            if (SystemDict == null || SystemDict.Count == 0)
            {
                RefreshSystemDict();
            }
            if (SystemDict.ContainsKey(Id))
            {
                system = SystemDict[Id];
                if (system.IsDeleted)
                    system = new SystemInfo();
            }
            return system;
        }

        /// <summary>
        /// 获取全部系统列表
        /// </summary>
        public List<SystemInfo> AllGet()
        {
            if (SystemDict == null || SystemDict.Count == 0)
            {
                RefreshSystemDict();
            }
            return SystemDict.Values.Where(p => !p.IsDeleted).ToList();
        }

        /// <summary>
        /// 新增系统数据
        /// </summary>
        public bool SingleInsert(SystemInfo system)
        {
            var result = false;
            Insert(system);
            var _excute = _unitOfWork.SaveChanges();
            if (_excute > 0)
            {
                result = true;
                if (SystemDict == null || SystemDict.Count == 0)
                {
                    RefreshSystemDict();
                }
                else
                {
                    UpdateSystemDict(system);

                }
            }
            return result;
        }

        /// <summary>
        /// 更新系统数据
        /// </summary>
        public bool SingleUpdate(SystemInfo system)
        {
            var result = false;
            Update(system);
            var _excute = _unitOfWork.SaveChanges();
            if (_excute > 0)
            {
                result = true;
                if (SystemDict == null || SystemDict.Count == 0)
                {
                    RefreshSystemDict();
                }
                else
                {
                    UpdateSystemDict(system);

                }
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除系统
        /// </summary>
        public bool SigleDelete(SystemInfo system)
        {
            var result = false;
            Update(system, p => p.IsDeleted,
                p => p.DeleterUserId,
                p => p.DeletionTime);
            var excute = _unitOfWork.SaveChanges();
            if (excute == 1)
            {
                result = true;
                if (SystemDict == null || SystemDict.Count == 0)
                {
                    RefreshSystemDict();
                }
                else
                {
                    if (SystemDict.ContainsKey(system.Id))
                    {
                        var _user = SystemDict[system.Id];
                        _user.IsDeleted = system.IsDeleted;
                        _user.DeleterUserId = system.DeleterUserId;
                        _user.DeletionTime = system.DeletionTime;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 分页查询系统列表
        /// </summary>
        public List<SystemInfo> PageQuery(SearchFilterDo searchFilter, out int count)
        {
            var result = new List<SystemInfo>();
            var systemList = AllGet();
            int total = 0;
            var list = new List<SystemInfo>();
            if (systemList.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchFilter.SearchKey))
                {
                    searchFilter.SearchKey = searchFilter.SearchKey.Trim().ToLower();
                    systemList = systemList.Where(p => p.Id.ToString() == searchFilter.SearchKey
                    || (!string.IsNullOrEmpty(p.Name) && p.Name.ToLower().Contains(searchFilter.SearchKey))
                    || (!string.IsNullOrEmpty(p.DisplayName) && p.DisplayName.ToLower().Contains(searchFilter.SearchKey))
                    || (!string.IsNullOrEmpty(p.DomainName) && p.DomainName.ToLower().Contains(searchFilter.SearchKey))
                    || (!string.IsNullOrEmpty(p.IPAddress) && p.IPAddress.ToLower().Contains(searchFilter.SearchKey))
                    ).ToList();
                }
                total = systemList.Count();
                list = systemList.Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();
            }
            count = total;
            result = list;

            return result;
        }
    }
}
