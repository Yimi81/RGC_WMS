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
    public class PlatformRepository : RepositoryBase<PlatformInfo>, IPlatformRepository
    {
        private static IUnitOfWork _unitOfWork;

        public PlatformRepository(DbContext context,
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
        public static Dictionary<long, PlatformInfo> PlatformDict;

        public void RefreshPlatformDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (PlatformDict == null || PlatformDict.Count == 0)
                {
                    PlatformDict = new Dictionary<Int64, PlatformInfo>();
                    PlatformDict = GetPlatformDictFromDB();
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
                PlatformDict = new Dictionary<Int64, PlatformInfo>();
                PlatformDict = GetPlatformDictFromDB();
            }
        }

        /// <summary>
        /// 从数据库中获取全部管理员
        /// </summary>
        public Dictionary<Int64, PlatformInfo> GetPlatformDictFromDB()
        {
            var dict = new Dictionary<Int64, PlatformInfo>();

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

        public bool IsENameExists(string name)
        {
            if (PlatformDict == null || PlatformDict.Count == 0)
            {
                RefreshPlatformDict();
            }
            if (PlatformDict.Values.Count(p => p.EName.ToLower() == name.Trim().ToLower() && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsENameExists(string name, long roleId)
        {
            if (PlatformDict == null || PlatformDict.Count == 0)
            {
                RefreshPlatformDict();
            }
            if (PlatformDict.Values.Count(p => p.EName.ToLower() == name.Trim().ToLower() && p.Id != roleId && !p.IsDeleted) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 单个获取
        /// </summary>
        public PlatformInfo SingleGet(Int64 id)
        {
            var obj = new PlatformInfo();

            if (PlatformDict == null || PlatformDict.Count == 0)
            {
                RefreshPlatformDict();
            }
            if (PlatformDict.Keys.Contains(id))
            {
                obj = PlatformDict.Values.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();
            }
            return obj;
        }


        /// <summary>
        /// 单个新增
        /// </summary>
        public PlatformInfo SingleAdd(PlatformInfo request)
        {
            var result = new PlatformInfo();

            Insert(request);
            int _excute = _unitOfWork.SaveChanges();
            if (_excute > 0)
            {
                //新增成功
                if (PlatformDict == null || PlatformDict.Count == 0)
                {
                    RefreshPlatformDict();
                }
                lock (_locker)
                {
                    if (PlatformDict.Keys.Contains(request.Id) == false)
                    {
                        PlatformDict.Add(request.Id, request);
                    }
                }
            }

            return request;
        }

        /// <summary>
        /// 单个更新数据库
        /// </summary>
        public int SingleUpdate(PlatformInfo obj)
        {
            var result = 0;

            Update(obj);
            result = _unitOfWork.SaveChanges();

            if (result > 0)
            {
                if (PlatformDict == null || PlatformDict.Count == 0)
                {
                    RefreshPlatformDict();
                }
                lock (_locker)
                {
                    if (PlatformDict.Keys.Contains(obj.Id))
                    {
                        PlatformDict[obj.Id] = obj;
                    }
                    else
                    {
                        RefreshPlatformDict();
                    }
                }
            }
            return result;

        }

        /// <summary>
        /// 单个删除，伪删除
        /// </summary>
        public int SingleDelete(PlatformInfo platform)
        {
            Update(platform, p => p.IsDeleted,
               p => p.DeleterUserId,
               p => p.DeletionTime);
            var excute = _unitOfWork.SaveChanges();
            if (excute == 1)
            {
                if (PlatformDict == null || PlatformDict.Count == 0)
                {
                    RefreshPlatformDict();
                }
                else
                {
                    if (PlatformDict.ContainsKey(platform.Id))
                    {
                        var _platform = PlatformDict[platform.Id];
                        _platform.IsDeleted = platform.IsDeleted;
                        _platform.DeleterUserId = platform.DeleterUserId;
                        _platform.DeletionTime = platform.DeletionTime;
                    }
                }
            }

            return excute;
        }

        /// <summary>
        /// 全部查询
        /// </summary>
        public List<PlatformInfo> AllListGet()
        {
            var result = new List<PlatformInfo>();
            if (PlatformDict == null || PlatformDict.Count == 0)
            {
                RefreshPlatformDict();
            }
            if (PlatformDict.Count > 0)
            {
                result = PlatformDict.Values.Where(p => p.IsDeleted == false)
                    .OrderBy(p => p.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// 分页查询平台列表
        /// </summary>
        public List<PlatformInfo> PageQuery(SearchFilterDo searchFilter, out int count)
        {
            var result = new List<PlatformInfo>();
            var systemList = AllListGet();
            var total = 0;
            var list = new List<PlatformInfo>();
            if (systemList.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchFilter.SearchKey))
                {
                    searchFilter.SearchKey = searchFilter.SearchKey.Trim().ToLower();
                    systemList = systemList.Where(p => p.Id.ToString() == searchFilter.SearchKey
                    || (!string.IsNullOrEmpty(p.EName) && p.EName.ToLower().Contains(searchFilter.SearchKey))
                    || (!string.IsNullOrEmpty(p.CName) && p.CName.ToLower().Contains(searchFilter.SearchKey))
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
