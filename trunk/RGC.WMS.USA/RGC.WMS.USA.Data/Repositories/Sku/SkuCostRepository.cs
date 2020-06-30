using HuigeTec.Core.Domain.Repositories;
using HuigeTec.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Do;
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
    /// jerry 2020/6/10
    /// </summary>
    public class SkuCostRepository : RepositoryBase<SkuCost>, ISkuCostRepository
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IOptions<DominBaseConfig> _configuration;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        private static IRepository<SkuCostBatch> _skuCostBatchRepository;
        private static ISkuRepository _skuRepository;
        private static IBmsUserRepository _bmsUserRepository;
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<long, SkuCost> SkuCostDict;
        public static Dictionary<long, SkuCostBatch> SkuCostBatchDict;
        public SkuCostRepository(DbContext context, IUnitOfWork unitOfWork,
            IRepository<SkuCostBatch> skuCostBatchRepository,
            ISkuRepository skuRepository,
            IBmsUserRepository bmsUserRepository,

        IOptions<DominBaseConfig> configuration
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _skuCostBatchRepository = skuCostBatchRepository;
            _skuRepository = skuRepository;
            _bmsUserRepository = bmsUserRepository;
            _configuration = configuration;
        }

        public void RefreshSkuCostDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (SkuCostDict == null || SkuCostDict.Count == 0)
                {
                    SkuCostDict = new Dictionary<long, SkuCost>();
                    SkuCostDict = GetSkuCostDictFromDB();
                }
            }
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        public void ForceRefreshSkuCostDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                SkuCostDict = new Dictionary<long, SkuCost>();
                SkuCostDict = GetSkuCostDictFromDB();
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, SkuCost> GetSkuCostDictFromDB()
        {
            Dictionary<long, SkuCost> dict = new Dictionary<long, SkuCost>();

            var list = TableNoTracking.ToList();
            var batchList = _skuCostBatchRepository.TableNoTracking.ToList();


            if (list != null && list.Any())
            {
                //dict = list.ToDictionary(p => p.Id);

                foreach (var item in list)
                {
                    item.FOBString = ParseHelper.Fen2YuanString(item.FOB);
                    item.DDPString = ParseHelper.Fen2YuanString(item.DDP);
                    item.SeaFreightString = ParseHelper.Fen2YuanString(item.SeaFreight);
                    item.UnloadingChargeString = ParseHelper.Fen2YuanString(item.UnloadingCharge);
                    item.ELCString = ParseHelper.Fen2YuanString(item.ELC);
                    item.Z3FreightString = ParseHelper.Fen2YuanString(item.Z3Freight);
                    item.Z5FreightString = ParseHelper.Fen2YuanString(item.Z5Freight);
                    item.MsrpString = ParseHelper.Fen2YuanString(item.Msrp);
                    item.MapString = ParseHelper.Fen2YuanString(item.Map);

                    item.Sku = _skuRepository.Get(item.SkuId);
                    if (item.Sku != null && item.Sku.Id == item.SkuId)
                    {
                        item.Sku.PrimaryImageSrcFull = string.IsNullOrEmpty(item.Sku.PrimaryImageSrc) ? "" : _configuration.Value.ImgSiteRootAddress + item.Sku.PrimaryImageSrc;
                    }

                    item.SkuCostBatchDict = batchList.Where(p => p.IsDeleted == false && p.SkuCostId == item.Id).ToDictionary(p => p.Id);
                    if (item.SkuCostBatchDict != null && item.SkuCostBatchDict.Count > 0)
                    {
                        item.IsAddBatch = true;
                        foreach (var batch in item.SkuCostBatchDict.Values)
                        {
                            batch.Sku = item.Sku;
                        }
                        item.SkuCostBatchList = item.SkuCostBatchDict.Values.ToList();
                    }

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
        public SkuCost Get(long id)
        {
            SkuCost result = new SkuCost();

            if (SkuCostDict == null || SkuCostDict.Count == 0)
            {
                RefreshSkuCostDict();
            }
            if (SkuCostDict.Keys.Contains(id))
            {
                result = SkuCostDict[id];
            }
            return result;
        }

        public List<SkuCost> GetAllList()
        {
            if (SkuCostDict == null || SkuCostDict.Count == 0)
            {
                RefreshSkuCostDict();
            }

            return SkuCostDict.Values.ToList();
        }
        public SkuCost Add(SkuCost request)
        {
            base.Insert(request);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                var excute1 = 0;
                if (request.SkuCostBatchList.Count > 0)
                {
                    foreach (var item in request.SkuCostBatchList)
                    {
                        item.SkuCostId = request.Id;
                        //item.CreationTime = DateTime.Now;
                        //item.CreatorUserId = adminId;

                        _skuCostBatchRepository.Insert(item);

                    }
                    excute1 = _unitOfWork.SaveChanges();
                }

                if (SkuCostDict == null || SkuCostDict.Count == 0)
                {
                    RefreshSkuCostDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (SkuCostDict.Keys.Contains(request.Id) == false)
                        {
                            if (request.SkuCostBatchList.Count > 0 && excute1 > 0)
                            {
                                request.SkuCostBatchDict = request.SkuCostBatchList.ToDictionary(p => p.Id);
                            }
                            ///var model = Mapper.Map<SkuCost>(request);
                            SkuCostDict.Add(request.Id, request);
                        }
                    }
                }
            }

            //if (excute > 0)
            //{

            //        var sku = _skuRepository.Get(request.SkuId);
            //        if (sku != null && sku.Id > 0)
            //        {
            //            if (sku.IsEditable)
            //            {
            //                sku.IsEditable = false;
            //                Entry<SkuInfo>(sku).State = EntityState.Modified;
            //                excute2 = SaveChanges();
            //            }
            //        }

            //        if (excute2 > 0)
            //        {
            //            if (DbSku.SkuDict == null || DbSku.SkuDict.Count == 0)
            //            {
            //                db.RefreshSkuDict();
            //            }
            //            else
            //            {
            //                lock (_locker)
            //                {
            //                    if (DbSku.SkuDict.Keys.Contains(request.SkuId) == true)
            //                    {

            //                        DbSku.SkuDict[request.SkuId].IsEditable = false;
            //                    }
            //                }
            //            }
            //        }

            //}
            return request;
        }

        public int AddBatch(SkuCostBatch request)
        {
            _skuCostBatchRepository.Insert(request);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (SkuCostDict == null || SkuCostDict.Count == 0)
                {
                    RefreshSkuCostDict();
                }
                else
                {
                    lock (_locker)
                    {

                        if (SkuCostDict.Keys.Contains(request.SkuCostId) == true)
                        {

                            SkuCostDict[request.SkuCostId].SkuCostBatchDict.Add(request.Id, request);
                            SkuCostDict[request.SkuCostId].SkuCostBatchList = SkuCostDict[request.SkuCostId].SkuCostBatchDict.Values.ToList();
                        }
                    }
                }
            }

            return excute;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="obj"></param>
        public int Update(SkuCost obj, long adminId)
        {
            int excute = 0;
            using (var trans = BeginTransaction())
            {
                try
                {
                    Update(obj);
                    //_unitOfWork.SaveChanges();
                    var delBatch_list = obj.SkuCostBatchDict.Values.Where(x => !obj.SkuCostBatchList.Select(a => a.Id).Contains(x.Id)).ToList();
                    delBatch_list.ForEach(model =>
                    {
                        _skuCostBatchRepository.Delete(model);
                    });

                    foreach (var item in obj.SkuCostBatchList)
                    {
                        if (item.Id == 0)
                        {
                            item.CreationTime = DateTime.Now;
                            item.CreatorUserId = adminId;
                            _skuCostBatchRepository.Insert(item);
                        }
                        else
                        {
                            item.LastModificationTime = DateTime.Now;
                            item.LastModifierUserId = adminId;

                            _skuCostBatchRepository.Update(item);
                        }
                    }
                    excute = _unitOfWork.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                if (excute > 0)
                {
                    if (SkuCostDict == null || SkuCostDict.Count == 0)
                    {
                        RefreshSkuCostDict();
                    }
                    else
                    {

                        lock (_locker)
                        {
                            if (SkuCostDict.Keys.Contains(obj.Id))
                            {
                                //foreach (var item in obj.SkuCostBatchList)
                                //{

                                //}
                                obj.SkuCostBatchDict = obj.SkuCostBatchList.ToDictionary(p => p.Id);
                                SkuCostDict[obj.Id] = obj;

                            }
                        }
                    }
                }
            }
            return excute;
        }

        public int UpdateBatchStatus(long adminId, long costId, long batchId, int status)
        {
            int excute = 0;
            var batch = _skuCostBatchRepository.GetById(batchId);

            batch.Status = status;
            batch.LastModificationTime = DateTime.Now;
            batch.LastModifierUserId = adminId;

            _skuCostBatchRepository.Update(batch);

            excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (SkuCostDict == null || SkuCostDict.Count == 0)
                {
                    RefreshSkuCostDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (SkuCostDict.Keys.Contains(costId))
                        {
                            if (SkuCostDict[costId].SkuCostBatchDict.Keys.Contains(batchId))
                            {
                                SkuCostDict[costId].SkuCostBatchDict[batchId] = batch;
                                SkuCostDict[costId].SkuCostBatchList = SkuCostDict[costId].SkuCostBatchDict.Values.ToList();
                            }
                        }
                    }
                }
            }

            return excute;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(long loginId, SkuCost model)
        {
            Delete(model);
            var excute =_unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (SkuCostDict == null || SkuCostDict.Count == 0)
                {
                    RefreshSkuCostDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (SkuCostDict.Keys.Contains(model.Id))
                        {
                            SkuCostDict.Remove(model.Id);
                        }
                    }
                }
            }

            return excute;
        }


        /// <summary>
        /// 分页查询
        /// shane 2020/2/14
        /// </summary>
        /// <param name="key"></param>
        /// <param name="categoryId"></param>
        /// <param name="searchFilter.PageSize"></param>
        /// <param name="searchFilter.CurrentPage"></param>
        /// <returns></returns>
        public List<SkuCost> GetSkuCostList(SearchFilterDo searchFilter, out int count)
        {
            List<SkuCost> result = new List<SkuCost>();
            count = 0;
            if (SkuCostDict == null || SkuCostDict.Count == 0)
            {
                RefreshSkuCostDict();
            }
            if (SkuCostDict.Any())
            {
                if (searchFilter.SearchKey == null)
                {
                    searchFilter.SearchKey = "";
                }
                else
                {
                    searchFilter.SearchKey = searchFilter.SearchKey.ToLower().Trim();
                }

                int total = SkuCostDict.Values.Count(p => p.IsDeleted == false
                                                     && (p.Sku !=null
                                                         && (p.Sku.FullEName.ToLower().Contains(searchFilter.SearchKey.ToLower()) 
                                                             || p.Sku.FactoryModel.ToLower().Contains(searchFilter.SearchKey.ToLower())
                                                            )
                                                         )
                                                     );

                var list = SkuCostDict.Values.Where(p => p.IsDeleted == false
                                                     && (p.Sku != null
                                                         && (p.Sku.FullEName.ToLower().Contains(searchFilter.SearchKey.ToLower())
                                                             || p.Sku.FactoryModel.ToLower().Contains(searchFilter.SearchKey.ToLower())
                                                            )
                                                         ))
                           .OrderByDescending(p => p.CreationTime).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

                result = list;
                count = (int)total;
            }
            return result;
        }

        public List<SkuCostBatch> GetSkuCostBatchList(string key, string batchNo, int pageSize, int currentPage, out int count)
        {
            List<SkuCostBatch> result = new List<SkuCostBatch>();
            count = 0;
            if (SkuCostDict == null || SkuCostDict.Count == 0)
            {
                RefreshSkuCostDict();
            }
            if (SkuCostDict.Any())
            {
                IEnumerable<SkuCost> iSkuCostList;
                var batchList = new List<SkuCostBatch>();
                iSkuCostList = SkuCostDict.Values.Where(p => p.IsDeleted == false).OrderBy(p => p.Sku.FactoryModel).ThenByDescending(p => p.CreationTime);

                foreach (var item in iSkuCostList)
                {
                    batchList.AddRange(item.SkuCostBatchDict.Values.ToList());
                }
                if (!string.IsNullOrEmpty(key))
                {
                    key = key.Trim();
                    batchList = batchList.Where(p => p.Sku != null && p.Sku.FullEName.ToLower().Contains(key.ToLower())
                    || !string.IsNullOrEmpty(p.Sku.FactoryModel) && p.Sku.FactoryModel.ToLower().Contains(key.ToLower())
                    || !string.IsNullOrEmpty(p.BatchNo) && p.BatchNo.ToLower().Contains(key.ToLower())).ToList();
                }

                int total = batchList.Count();
                count = total;
                result = batchList.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList(); ;
    
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

        public List<SkuCostBatch> GetSkuCostBatchList(long skuId, bool isRefreshDict, int pageSize, int currentPage, out int count)
        {
            List<SkuCostBatch> result = new List<SkuCostBatch>();
            count = 0;
            if (SkuCostDict == null || SkuCostDict.Count == 0)
            {
                RefreshSkuCostDict();
            }
            if (SkuCostDict.Any())
            {
                IEnumerable<SkuCost> iSkuCostList;
                var batchList = new List<SkuCostBatch>();
                iSkuCostList = SkuCostDict.Values.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreationTime);

                foreach (var item in iSkuCostList)
                {
                    batchList.AddRange(item.SkuCostBatchDict.Values.ToList());
                }
                int total = batchList.Count();
                count = total;
                result = batchList.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList(); ;

                foreach (var item in result)
                {
                    var user = _bmsUserRepository.SingleGet(item.CreatorUserId);
                    if (user != null && user.Id > 0)
                    {
                        item.CreatorUser = user.FullName;
                    }
                    item.CreationTimeString = item.CreationTime.ToString("yyyy-MM-dd HH:mm");
                    item.LastModificationTimeString = item.LastModificationTime.HasValue ? item.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                    var cost = SkuCostDict.Values.Where(p => p.Id == item.SkuCostId).FirstOrDefault();
                    if (cost != null && cost.Id == item.SkuCostId)
                    {
                        item.SkuCost = new SkuCost();
                        item.SkuCost.Id = cost.Id;
                        item.SkuCost.SkuId = cost.SkuId;
                        item.SkuCost.ProductId = cost.ProductId;
                        item.SkuCost.FOBString = cost.FOBString;
                        item.SkuCost.DDPString = cost.DDPString;
                        item.SkuCost.ELCString = cost.ELCString;
                        item.SkuCost.SeaFreightString = cost.SeaFreightString;
                        item.SkuCost.UnloadingCharge = cost.UnloadingCharge;
                        item.SkuCost.Z3FreightString = cost.Z3FreightString;
                        item.SkuCost.Z5FreightString = cost.Z5FreightString;
                        item.SkuCost.MsrpString = cost.MsrpString;
                        item.SkuCost.MapString = cost.MsrpString;
                        item.SkuCost.Remark = cost.Remark;
                    }
                }
            }
            return result;
        }

    }
}
