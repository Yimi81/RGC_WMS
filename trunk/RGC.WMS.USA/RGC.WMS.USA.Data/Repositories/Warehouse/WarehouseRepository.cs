using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using RGC.WMS.USA.Domain.Repositories.Warehouse;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Warehouse
{
    /// <summary>
    /// MeridianGo 2020/06/11
    /// </summary>
    public class WarehouseRepository : RepositoryBase<WarehouseInfo>, IWarehouseRepository
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IOptions<DominBaseConfig> _configuration;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// WarehouseInfo字典
        /// </summary>
        public static Dictionary<long, WarehouseInfo> WarehouseDict;

        public WarehouseRepository(
            DbContext context,
            IUnitOfWork unitOfWork,
            IOptions<DominBaseConfig> configuration
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        private void RefreshWarehouseDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (WarehouseDict == null || WarehouseDict.Count == 0)
                {
                    WarehouseDict = new Dictionary<long, WarehouseInfo>();
                    WarehouseDict = GetWarehouseDictFromDB();
                }
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// </summary>
        public Dictionary<long, WarehouseInfo> GetWarehouseDictFromDB()
        {
            var dict = new Dictionary<long, WarehouseInfo>();
            var list = TableNoTracking.ToList();
            if (list != null && list.Any())
                dict = list.ToDictionary(p => p.Id);

            return dict;
        }

        /// <summary>
        /// 获取单个实例
        /// </summary>
        public WarehouseInfo Get(long id)
        {
            var result = new WarehouseInfo();
            if (WarehouseDict == null || WarehouseDict.Count == 0)
                RefreshWarehouseDict();
            if (WarehouseDict.ContainsKey(id))
                result = WarehouseDict[id];
            return result;
        }

        /// <summary>
        /// 新增仓库
        /// </summary>
        public void Create(long loginId, WarehouseInfo model)
        {
            using (var transaction = BeginTransaction())
            {
                try
                {
                    model.Id = 0;
                    model.ResetAddModel(loginId);
                    Insert(model);
                    var excute = _unitOfWork.SaveChanges();
                    if (excute > 0)
                    {
                        if (WarehouseDict == null || WarehouseDict.Count == 0)
                            RefreshWarehouseDict();
                        else
                        {
                            lock (_locker)
                            {
                                if (!WarehouseDict.ContainsKey(model.Id))
                                    WarehouseDict.Add(model.Id, model);
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception error)
                {
                    transaction.Rollback();
                    throw error;
                }
            }
        }

        /// <summary>
        /// 更新仓库
        /// </summary>
        public void Update(long loginId, WarehouseInfo model)
        {
            using (var transaction = BeginTransaction())
            {
                try
                {
                    model.ResetModifyModel(loginId);
                    Update(model);
                    var excute = _unitOfWork.SaveChanges();
                    if (excute > 0)
                    {
                        if (WarehouseDict == null || WarehouseDict.Count == 0)
                            RefreshWarehouseDict();
                        else
                        {
                            lock (_locker)
                            {
                                if (!WarehouseDict.ContainsKey(model.Id))
                                    WarehouseDict.Add(model.Id, model);
                                else
                                    WarehouseDict[model.Id] = model;
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception error)
                {
                    transaction.Rollback();
                    throw error;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Delete(long loginId, WarehouseInfo model)
        {
            var id = model.Id;
            model.ResetDeleteModel(loginId);
            Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                lock (_locker)
                {
                    if (WarehouseDict == null || WarehouseDict.Count == 0)
                        RefreshWarehouseDict();
                    else
                    {
                        if (WarehouseDict.ContainsKey(id))
                        {
                            WarehouseDict[id].IsDeleted = model.IsDeleted;
                            WarehouseDict[id].DeleterUserId = model.DeleterUserId;
                            WarehouseDict[id].DeletionTime = model.DeletionTime;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public void Recovery(long loginId, WarehouseInfo model)
        {
            var id = model.Id;
            model.ResetRecoveryModel(loginId);
            Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                lock (_locker)
                {
                    if (WarehouseDict == null || WarehouseDict.Count == 0)
                        RefreshWarehouseDict();
                    else
                    {
                        if (WarehouseDict.ContainsKey(id))
                        {
                            WarehouseDict[id].IsDeleted = model.IsDeleted;
                            WarehouseDict[id].DeleterUserId = model.DeleterUserId;
                            WarehouseDict[id].DeletionTime = model.DeletionTime;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        public void UpdateStatus(WarehouseInfo model, long modifierUserId)
        {
            var id = model.Id;
            model.ResetModifyModel(modifierUserId);
            Update(model, x => x.Status, x => x.LastModifierUserId, x => x.LastModificationTime);
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                lock (_locker)
                {
                    if (WarehouseDict == null || WarehouseDict.Count == 0)
                        RefreshWarehouseDict();
                    else
                    {
                        if (WarehouseDict.ContainsKey(id))
                        {
                            WarehouseDict[id].Status = model.Status;
                            WarehouseDict[id].LastModifierUserId = model.LastModifierUserId;
                            WarehouseDict[id].LastModificationTime = model.LastModificationTime;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<WarehouseInfo> GetPage((string SearchKey, int Status, int IsDeleted, int PageSize, int CurrentPage) searchFilter, out int count)
        {
            var result = new List<WarehouseInfo>();
            count = 0;

            if (WarehouseDict == null || WarehouseDict.Count == 0)
                RefreshWarehouseDict();

            if (WarehouseDict.Any())
            {
                searchFilter.SearchKey = searchFilter.SearchKey.ToEmpty().ToLower();

                var lstQuery = WarehouseDict.Values.Select(x => x);

                if (searchFilter.Status > 0)
                {
                    lstQuery = lstQuery.Where(x => x.Status == (WarehouseStatus)(searchFilter.Status));
                }

                if (searchFilter.IsDeleted > 0)
                {
                    var bIsDeleted = searchFilter.IsDeleted == 2;
                    lstQuery = lstQuery.Where(x => x.IsDeleted == bIsDeleted);
                }

                if (!string.IsNullOrEmpty(searchFilter.SearchKey))
                    lstQuery = lstQuery.Where(x =>
                        x.Number.ToLower().Contains(searchFilter.SearchKey) ||
                        x.Name.ToLower().Contains(searchFilter.SearchKey) ||
                        x.PostCodePrefix.ToLower().Contains(searchFilter.SearchKey));

                count = lstQuery.Count();

                var list = lstQuery
                    .OrderByDescending(p => p.CreationTime)
                    .Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize)
                    .Take(searchFilter.PageSize)
                    .ToList();

                if (list.Any())
                    result = list;
            }
            return result;
        }

        /// <summary>
        /// 判断仓库编号是否存在
        /// </summary>
        public bool IsExistWarehouseNumber(long iUnincludedId, string sNumber)
        {
            if (WarehouseDict == null || WarehouseDict.Count == 0)
                RefreshWarehouseDict();
            if (iUnincludedId > 0)
                return WarehouseDict.Values.Where(x => x.Id != iUnincludedId && x.Number == sNumber).Any();
            return WarehouseDict.Values.Where(x => x.Number == sNumber).Any();
        }

        /// <summary>
        /// MeridianGo 2020/06/11 强制刷新
        /// </summary>
        public void ForceRefreshWarehouseDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                WarehouseDict = new Dictionary<long, WarehouseInfo>();
                WarehouseDict = GetWarehouseDictFromDB();
            }
        }

        /// <summary>
        /// 获取未删除，且正常状态的全部仓库 by MeridianGo 2020/06/22
        /// </summary>
        public List<(long Id, string Number, string Name, string PostCodePrefix)> GetWarehouseSimpleList()
        {
            return GetWarehouseDict()
                .Where(x => x.Status == WarehouseStatus.Normal && !x.IsDeleted)
                .Select(x =>
                    (x.Id, x.Number, x.Name, x.PostCodePrefix)
                ).OrderBy(x => x.Id).ToList();
        }

        /// <summary>
        /// 获取全部仓库 by MeridianGo 2020/06/22
        /// </summary>
        public List<WarehouseInfo> GetWarehouseDict()
        {
            RefreshWarehouseDict();
            return WarehouseDict.Select(x => x.Value).ToList();
        }
    }
}
