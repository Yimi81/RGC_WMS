using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Repositories.Item;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Item
{
    public class ItemModifyFlowRepository : RepositoryBase<ItemModifyFlow>, IItemModifyFlowRepository
    {
        private static IUnitOfWork _unitOfWork;
        private static readonly object _locker = new object();
        /// <summary>
        /// 在售产品字典
        /// </summary>
        public static Dictionary<long, ItemModifyFlow> ItemFlowDict;
        public ItemModifyFlowRepository(DbContext context, IUnitOfWork unitOfWork
            ) : base(context)
        {
            _unitOfWork = unitOfWork;

        }

        public void RefreshItemFlowDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (ItemFlowDict == null || ItemFlowDict.Count == 0)
                {
                    ItemFlowDict = new Dictionary<long, ItemModifyFlow>();
                    ItemFlowDict = GetItemFlowDictFromDB();
                }
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, ItemModifyFlow> GetItemFlowDictFromDB()
        {
            Dictionary<long, ItemModifyFlow> dict = new Dictionary<long, ItemModifyFlow>();
            try
            {
                var list = TableNoTracking.ToList();

                if (list != null && list.Any())
                {
                    dict = list.ToDictionary(p => p.Id);
                }
            }
            catch (Exception ex)
            {

            }
            return dict;
        }

        public int AddFlow(ItemModifyFlow entity)
        {
            int excute = 0;
            entity.CreationTime = DateTime.Now;
            try
            {
                Insert(entity);
                excute = _unitOfWork.SaveChanges();

                if (excute > 0)
                {

                    if (ItemFlowDict == null || ItemFlowDict.Count == 0)
                    {
                        RefreshItemFlowDict();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            if (ItemFlowDict.Keys.Contains(entity.Id))
                            {
                                ItemFlowDict[entity.Id] = entity;
                            }
                            else
                            {
                                ItemFlowDict.Add(entity.Id, entity);

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

        public ItemModifyFlow GetFlow(long itemId, long flowId)
        {
            ItemModifyFlow result = new ItemModifyFlow();
            RefreshItemFlowDict();
            if (flowId > 0 && ItemFlowDict.Keys.Contains(flowId))
            {
                result = ItemFlowDict[itemId];

            }
            else if (itemId > 0)
            {
                result = ItemFlowDict.Values.Where(p => (p.SyncStatus == 0 || p.SyncStatus == 2) && p.ItemId == itemId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
            }
            return result;

        }

        public List<ItemModifyFlow> GetFlowList(List<long> itemIds, List<long> flowIds)
        {
            List<ItemModifyFlow> result = new List<ItemModifyFlow>();
            RefreshItemFlowDict();
            if (flowIds.Count > 0)
            {
                foreach (var flowId in flowIds)
                {
                    if (ItemFlowDict.Keys.Contains(flowId))
                    {
                        result.Add(ItemFlowDict[flowId]);
                    }
                }

            }
            else if (itemIds.Count > 0)
            {
                result = ItemFlowDict.Values.Where(p => (p.SyncStatus == 0 || p.SyncStatus == 2) && itemIds.Contains(p.ItemId)).ToList();
            }
            else
            {
                result = ItemFlowDict.Values.Where(p => (p.SyncStatus == 0 || p.SyncStatus == 2)).ToList();
            }
            result = result.OrderByDescending(p => p.CreationTime).GroupBy(p => p.ItemId).Select(g => g.First()).ToList();
            return result;
        }

        public int UpdateFlow(ItemModifyFlow entity)
        {
            int excute = 0;
            entity.LastModificationTime = DateTime.Now;
            try
            {
                Update(entity, p => p.SyncStatus, p => p.LastModificationTime, p => p.LastModifierUserId, p => p.RequestSyncTime);
                excute = _unitOfWork.SaveChanges();

                if (excute > 0)
                {

                    if (ItemFlowDict == null || ItemFlowDict.Count == 0)
                    {
                        RefreshItemFlowDict();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            if (ItemFlowDict.Keys.Contains(entity.Id))
                            {
                                ItemFlowDict.Add(entity.Id, entity);
                            }
                            else
                            {
                                ItemFlowDict[entity.Id].SyncStatus = entity.SyncStatus;
                                ItemFlowDict[entity.Id].RequestSyncTime = entity.RequestSyncTime;
                                ItemFlowDict[entity.Id].LastModificationTime = entity.LastModificationTime;
                                ItemFlowDict[entity.Id].LastModifierUserId = entity.LastModifierUserId;

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

        public int UpdateSyncStatus(long userId, long flowId)
        {
            int excute = 0;
            ItemModifyFlow entity = new ItemModifyFlow()
            {
                Id = flowId,
                SyncStatus = 3,//忽略
                LastModificationTime = DateTime.Now,
                LastModifierUserId = userId
            };
            try
            {
                Update(entity, p => p.SyncStatus, p => p.LastModificationTime, p => p.LastModifierUserId);
                excute = _unitOfWork.SaveChanges();

                if (excute > 0)
                {

                    if (ItemFlowDict == null || ItemFlowDict.Count == 0)
                    {
                        RefreshItemFlowDict();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            if (ItemFlowDict.Keys.Contains(entity.Id))
                            {
                                ItemFlowDict.Add(entity.Id, entity);
                            }
                            else
                            {
                                ItemFlowDict[entity.Id].SyncStatus = entity.SyncStatus;
                                ItemFlowDict[entity.Id].LastModificationTime = entity.LastModificationTime;
                                ItemFlowDict[entity.Id].LastModifierUserId = entity.LastModifierUserId;
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
    }
}
