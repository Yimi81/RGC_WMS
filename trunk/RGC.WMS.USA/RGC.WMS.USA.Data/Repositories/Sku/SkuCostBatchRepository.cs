using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Repositories.Sku;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Sku
{
    /// <summary>
    /// MeridianGo 2020/6/22
    /// </summary>
    public class SkuCostBatchRepository : RepositoryBase<SkuCostBatch>, ISkuCostBatchRepository
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IOptions<DominBaseConfig> _configuration;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        private static ISkuRepository _skuRepository;
        private static IBmsUserRepository _bmsUserRepository;

        public static Dictionary<long, SkuCostBatch> SkuCostBatchDict;
        public SkuCostBatchRepository(DbContext context, IUnitOfWork unitOfWork,
            ISkuRepository skuRepository,
            IBmsUserRepository bmsUserRepository,
            IOptions<DominBaseConfig> configuration
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _skuRepository = skuRepository;
            _bmsUserRepository = bmsUserRepository;
            _configuration = configuration;
        }

        private void RefreshSkuCostBatchDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (SkuCostBatchDict == null || SkuCostBatchDict.Count == 0)
                {
                    SkuCostBatchDict = new Dictionary<long, SkuCostBatch>();
                    SkuCostBatchDict = GetSkuCostBatchDictFromDB();
                }
            }
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        private void ForceRefreshSkuCostBatchDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                SkuCostBatchDict = new Dictionary<long, SkuCostBatch>();
                SkuCostBatchDict = GetSkuCostBatchDictFromDB();
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, SkuCostBatch> GetSkuCostBatchDictFromDB()
        {
            var dict = new Dictionary<long, SkuCostBatch>();

            var lstSkuCostBatch = TableNoTracking;
            var lstSku = _skuRepository.TableNoTracking;
            var list = (from batch in lstSkuCostBatch
                        join sku in lstSku on batch.SkuId equals sku.Id into tempSkuCostBatch
                        from sku in tempSkuCostBatch.DefaultIfEmpty()
                        select new
                        {
                            batch,
                            sku
                        }).ToList();

            foreach (var item in list)
            {
                if (item.sku != null && item.sku.Id == item.batch.SkuId)
                {
                    item.sku.PrimaryImageSrcFull = string.IsNullOrEmpty(item.sku.PrimaryImageSrc) ? string.Empty : _configuration.Value.ImgSiteRootAddress + item.sku.PrimaryImageSrc;
                    item.batch.Sku = item.sku;
                }
                dict.Add(item.batch.Id, item.batch);
            }

            return dict;
        }

        public List<SkuCostBatch> GetSkuCostBatchList(string searchKey, int pageSize, int currentPage, out int count)
        {
            count = 0;
            if (SkuCostBatchDict == null || SkuCostBatchDict.Count == 0)
                RefreshSkuCostBatchDict();

            var lstResult = new List<SkuCostBatch>();
            if (SkuCostBatchDict.Any())
            {
                var lstStatus = new List<int>();
                //已到货
                lstStatus.Add(2);
                //在售
                lstStatus.Add(3);

                var lstQuery = SkuCostBatchDict.Values.Where(x => lstStatus.Contains(x.Status) && !x.IsDeleted);

                if (!string.IsNullOrEmpty(searchKey))
                {
                    searchKey = searchKey.ToEmpty().ToLower();
                    lstQuery = lstQuery.Where(x =>
                    x.Sku.FactoryModel.ToEmpty().ToLower().Contains(searchKey)
                    || x.BatchNo.ToEmpty().ToLower().Contains(searchKey));
                }

                count = lstQuery.Count();

                lstQuery = lstQuery.OrderBy(p => p.Sku.FactoryModel).ThenByDescending(p => p.Sku.CreationTime);
                var lstQueryResult = lstQuery.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();

                var lstUser = _bmsUserRepository.GetAllUserByKeys(lstQueryResult.Select(x => x.CreatorUserId).ToList());

                foreach (var item in lstQueryResult)
                {
                    item.Sku = item.Sku ?? new SkuInfo();

                    if (item.ProductId <= 0)
                        item.ProductId = item.Sku.ProductId;

                    if (item.CreatorUserId > 0)
                        item.CreatorUser = lstUser.FirstOrDefault(x => x.Id == item.CreatorUserId)?.FullName;
                    item.CreationTimeString = item.CreationTime.ToDateTimeZNString();
                    item.LastModificationTimeString = item.LastModificationTime.ToDateTimeZNString();
                    lstResult.Add(item);
                }
            }
            return lstResult;
        }

        /// <summary>
        /// 获取全部Sku Cost Batch
        /// </summary>
        public List<SkuCostBatch> GetSkuCostBatchDict()
        {
            RefreshSkuCostBatchDict();
            return SkuCostBatchDict.Select(x => x.Value).ToList();
        }
    }
}
