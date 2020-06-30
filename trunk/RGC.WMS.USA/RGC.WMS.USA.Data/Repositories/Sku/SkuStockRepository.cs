using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Repositories.Sku;
using RGC.WMS.USA.Domain.Repositories.Warehouse;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Sku
{
    /// <summary>
    /// jerry 2020/6/10
    /// </summary>
    public class SkuStockRepository : RepositoryBase<SkuStock>, ISkuStockRepository
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IOptions<DominBaseConfig> _configuration;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();


        private static ISkuRepository _skuRepository;
        private static IWarehouseRepository _warehouseRepository;
        private static IBmsUserRepository _bmsUserRepository;
        private static ISkuCostRepository _skuCostRepository;
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<long, SkuStock> SkuStockDict;

        public SkuStockRepository(DbContext context, IUnitOfWork unitOfWork,

            ISkuRepository skuRepository,
            IWarehouseRepository warehouseRepository,
            IBmsUserRepository bmsUserRepository,
            ISkuCostRepository skuCostRepository,

        IOptions<DominBaseConfig> configuration
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _skuRepository = skuRepository;
            _warehouseRepository = warehouseRepository;
            _bmsUserRepository = bmsUserRepository;
            _skuCostRepository = skuCostRepository;
            _configuration = configuration;
        }

        public void RefreshSkuStockDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (SkuStockDict == null || SkuStockDict.Count == 0)
                {
                    SkuStockDict = new Dictionary<long, SkuStock>();
                    SkuStockDict = GetSkuStockDictFromDB();
                }
            }
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        public void ForceRefreshSkuStockDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                SkuStockDict = new Dictionary<long, SkuStock>();
                SkuStockDict = GetSkuStockDictFromDB();
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, SkuStock> GetSkuStockDictFromDB()
        {
            Dictionary<long, SkuStock> dict = new Dictionary<long, SkuStock>();

            var list = TableNoTracking.ToList();
            var warehouseList = _warehouseRepository.GetWarehouseDict();
            var costList = _skuCostRepository.GetAllList();
            if (list != null && list.Any())
            {
                //dict = list.ToDictionary(p => p.Id);

             
                foreach (var item in list)
                {
                    item.Sku = _skuRepository.Get(item.SkuId);
                    if (item.Sku != null && item.Sku.Id == item.SkuId)
                    {
                        item.Sku.PrimaryImageSrcFull = string.IsNullOrEmpty(item.Sku.PrimaryImageSrc) ? "" : _configuration.Value.ImgSiteRootAddress + item.Sku.PrimaryImageSrc;
                    }
                    item.Warehouse = warehouseList.FirstOrDefault(x => x.Id == item.WarehouseId) ?? new WarehouseInfo();
                    item.SkuCost = costList.FirstOrDefault(x => x.Id == item.SkuCostId) ?? new SkuCost();
                    dict.Add(item.Id, item);
                }

            }

            return dict;
        }

        /// <summary>
        /// 获取单个实例
        /// jerry 2020/6/10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SkuStock Get(long id)
        {
            SkuStock result = new SkuStock();

            if (SkuStockDict == null || SkuStockDict.Count == 0)
            {
                RefreshSkuStockDict();
            }
            if (SkuStockDict.Keys.Contains(id))
            {
                result = SkuStockDict[id];
            }
            return result;
        }

        public List<SkuStock> GetAllList()
        {
            if (SkuStockDict == null || SkuStockDict.Count == 0)
            {
                RefreshSkuStockDict();
            }

            return SkuStockDict.Values.ToList();
        }
        public SkuStock Add(SkuStock request)
        {
            base.Insert(request);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (SkuStockDict == null || SkuStockDict.Count == 0)
                {
                    RefreshSkuStockDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (SkuStockDict.Keys.Contains(request.Id) == false)
                        {
                            request.Warehouse = _warehouseRepository.GetWarehouseDict().FirstOrDefault(x => x.Id == request.WarehouseId) ?? new WarehouseInfo();
                            SkuStockDict.Add(request.Id, request);
                        }
                    }
                }
            }
            return request;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="obj"></param>
        public int Update(long loginId, SkuStock obj)
        {
            int excute = 0;
            Update(obj);
            excute =_unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (SkuStockDict == null || SkuStockDict.Count == 0)
                {
                    RefreshSkuStockDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (SkuStockDict.Keys.Contains(obj.Id))
                        {
                            SkuStockDict[obj.Id] = obj;
                        }
                    }
                }
            }

            return excute;
        }

        public List<SkuStock> GetSkuStockList(long warehouseId, string key, int pageSize, int currentPage, out int count)
        {
            List<SkuStock> result = new List<SkuStock>();
            count = 0;
            if (SkuStockDict == null || SkuStockDict.Count == 0)
            {
                RefreshSkuStockDict();
            }
            if (SkuStockDict.Any())
            {
                IEnumerable<SkuStock> iSkuStockList;
                iSkuStockList = SkuStockDict.Values.Where(p => p.IsDeleted == false).OrderBy(p => p.Sku.FactoryModel).ThenBy(p => p.WarehouseId).ThenByDescending(p => p.CreationTime);

                if (!string.IsNullOrEmpty(key))
                {
                    key = key.Trim();
                    iSkuStockList = iSkuStockList.Where(p => p.Sku != null && p.Sku.FullEName.ToLower().Contains(key.ToLower()) || !string.IsNullOrEmpty(p.Sku.FactoryModel) && p.Sku.FactoryModel.ToLower().Contains(key.ToLower()));
                }
                if (warehouseId != 0)
                {
                    iSkuStockList = iSkuStockList.Where(p => p.WarehouseId == warehouseId);

                }
               
                int total = iSkuStockList.Count();
                count = total;
                result = iSkuStockList.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList(); ;
    
                    foreach (var item in result)
                    {
                        var user = _bmsUserRepository.SingleGet(item.CreatorUserId);
                        if (user != null && user.Id > 0)
                        {
                            item.CreatorUser = user.FullName;
                        }
                        item.CreationTimeString = item.CreationTime.ToString("yyyy-MM-dd HH:mm");
                        item.LastModificationTimeString = item.LastModificationTime.HasValue ? item.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                    }    
            }
            return result;
        }

    }
}
