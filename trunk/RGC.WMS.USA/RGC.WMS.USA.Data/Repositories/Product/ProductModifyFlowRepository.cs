using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Repositories.Product;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Product
{
    /// <summary>
    /// shane 2020/2/12
    /// </summary>
    public class ProductModifyFlowRepository : RepositoryBase<ProductModifyFlow>, IProductModifyFlowRepository
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IOptions<DominBaseConfig> _configuration;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();
        /// <summary>
        /// Product字典
        /// </summary>
        public static Dictionary<long, ProductModifyFlow> FlowDict;

        public ProductModifyFlowRepository(DbContext context, IUnitOfWork unitOfWork,
        IOptions<DominBaseConfig> configuration
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        public void RefreshFlowDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (FlowDict == null || FlowDict.Count == 0)
                {
                    FlowDict = new Dictionary<long, ProductModifyFlow>();
                    FlowDict = GetFlowDictFromDB();
                }
            }
        }

        /// <summary>
        /// shane 2020/2/18 强制刷新
        /// </summary>
        public void ForceRefreshFlowDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                FlowDict = new Dictionary<long, ProductModifyFlow>();
                FlowDict = GetFlowDictFromDB();
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, ProductModifyFlow> GetFlowDictFromDB()
        {
            Dictionary<long, ProductModifyFlow> dict = new Dictionary<long, ProductModifyFlow>();

            var list = TableNoTracking.ToList();


            if (list != null && list.Any())
            {
                dict = list.ToDictionary(p => p.Id);

            }

            return dict;
        }

        public List<ProductModifyFlow> GetAllFlow()
        {
            var result = new List<ProductModifyFlow>();
            RefreshFlowDict();
            result = FlowDict.Values.ToList();
            return result;
        }


        /// <summary>
        /// 获取单个实例
        /// shane 2020/2/28
        /// </summary>
        /// <returns></returns>
        public ProductModifyFlow GetFlow(long productId, long flowId)
        {
            ProductModifyFlow result = new ProductModifyFlow();
            RefreshFlowDict();
            if (flowId > 0)
            {
                if (FlowDict.Keys.Contains(flowId))
                {
                    result = FlowDict[flowId];
                }
                else if (productId > 0)
                {
                    result = FlowDict.Values.Where(p => (p.SyncStatus == 0 || p.SyncStatus == 2) && p.ProductId == productId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
                }
            }
            else if (productId > 0)
            {
                result = FlowDict.Values.Where(p => (p.SyncStatus == 0 || p.SyncStatus == 2) && p.ProductId == productId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
            }
            if (result != null)
                return DeepCopyByReflect(result);
            else
                return result;
        }

        /// <summary>
        /// 增加修改同步流水
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductModifyFlow AddFlow(ProductModifyFlow model)
        {
            model.CreationTime = DateTime.Now;
            try
            {
                Insert(model);
                int excute = _unitOfWork.SaveChanges();

                if (excute > 0)
                {

                    if (FlowDict == null || FlowDict.Count == 0)
                    {
                        RefreshFlowDict();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            if (!FlowDict.Keys.Contains(model.Id))
                            {
                                FlowDict.Add(model.Id, model);
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
            }


            return model;
        }

        public ProductModifyFlow UpdateFlow(ProductModifyFlow model)
        {
            model.LastModificationTime = DateTime.Now;
            try
            {
                Update(model, p => p.SyncStatus, p => p.LastModificationTime, p => p.LastModifierUserId, p => p.RequestSyncTime);
                int excute = _unitOfWork.SaveChanges();

                if (excute > 0)
                {

                    if (FlowDict == null || FlowDict.Count == 0)
                    {
                        RefreshFlowDict();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            if (FlowDict.Keys.Contains(model.Id))
                            {

                                FlowDict[model.Id].SyncStatus = model.SyncStatus;
                                FlowDict[model.Id].RequestSyncTime = model.RequestSyncTime;
                                FlowDict[model.Id].LastModificationTime = model.LastModificationTime;
                                FlowDict[model.Id].LastModifierUserId = model.LastModifierUserId;

                            }
                            else
                            {
                                FlowDict.Add(model.Id, model);
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
            }


            return model;
        }
        public int UpdateSyncStatus(long userId, long flowId)
        {
            var excute = 0;
            ProductModifyFlow model = new ProductModifyFlow()
            {
                Id = flowId,
                SyncStatus = 3,//忽略,
                LastModifierUserId = userId,
                LastModificationTime = DateTime.Now
            };
            try
            {
                Update(model, p => p.SyncStatus, p => p.LastModificationTime, p => p.LastModifierUserId);
                excute = _unitOfWork.SaveChanges();
                if (excute > 0)
                {
                    if (FlowDict == null || FlowDict.Count == 0)
                    {
                        RefreshFlowDict();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            if (FlowDict.Keys.Contains(model.Id))
                            {

                                FlowDict[model.Id].SyncStatus = model.SyncStatus;
                                FlowDict[model.Id].LastModificationTime = model.LastModificationTime;
                                FlowDict[model.Id].LastModifierUserId = model.LastModifierUserId;

                            }
                            else
                            {
                                FlowDict.Add(model.Id, model);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }


            return excute;
        }

        public List<ProductModifyFlow> GetFlowList(List<long> productIds, List<long> flowIds)
        {
            List<ProductModifyFlow> result = new List<ProductModifyFlow>();
            RefreshFlowDict();
            if (flowIds != null && flowIds.Count > 0)
            {
                foreach (var flowId in flowIds)
                {
                    if (FlowDict.Keys.Contains(flowId))
                    {
                        result.Add(FlowDict[flowId]);
                    }
                }

            }
            else if (productIds != null && productIds.Count > 0)
            {
                result = FlowDict.Values.Where(p => (p.SyncStatus == 0 || p.SyncStatus == 2) && productIds.Contains(p.ProductId)).ToList();
            }
            else
            {
                result = FlowDict.Values.Where(p => (p.SyncStatus == 0 || p.SyncStatus == 2)).ToList();
            }
            result = result.OrderByDescending(p => p.CreationTime).GroupBy(p => p.ProductId).Select(g => g.First()).ToList();
            return result;
        }
    }
}
