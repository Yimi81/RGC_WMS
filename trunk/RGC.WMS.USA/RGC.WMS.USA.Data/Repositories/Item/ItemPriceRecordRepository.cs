using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Repositories.Item;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Item
{
    public class ItemPriceRecordRepository : RepositoryBase<ItemPriceRecord>, IItemPriceRecordRepository
    {
        private static IUnitOfWork _unitOfWork;
        private static readonly object _locker = new object();

        /// <summary>
        /// Sku字典
        /// </summary>
        public static Dictionary<long, ItemPriceRecord> ItemPriceRecordDict;
        public ItemPriceRecordRepository(DbContext context, IUnitOfWork unitOfWork) :base(context)
        {
            _unitOfWork = unitOfWork;
        }
      


        public void RefreshItemPriceRecordDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (ItemPriceRecordDict == null || ItemPriceRecordDict.Count == 0)
                {
                    ItemPriceRecordDict = new Dictionary<long, ItemPriceRecord>();
                    ItemPriceRecordDict = GetItemPriceRecordDictFromDB();
                }
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, ItemPriceRecord> GetItemPriceRecordDictFromDB()
        {
            Dictionary<long, ItemPriceRecord> dict = new Dictionary<long, ItemPriceRecord>();

            var list = TableNoTracking.ToList();
            if (list != null && list.Any())
            {
                dict = list.ToDictionary(p => p.Id);

            }
            return dict;
        }
        public int Add(ItemPriceRecord request)
        {
            request.CreationTime = DateTime.Now;
            Insert(request);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (ItemPriceRecordDict == null || ItemPriceRecordDict.Count == 0)
                {
                    RefreshItemPriceRecordDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (ItemPriceRecordDict.Keys.Contains(request.Id) == false)
                        {
                            ItemPriceRecordDict.Add(request.Id, request);
                        }
                    }
                }
            }

            return excute;
        }
        
        public ItemPriceRecord Get(long id)
        {
            ItemPriceRecord result = new ItemPriceRecord();

            if (ItemPriceRecordDict == null || ItemPriceRecordDict.Count == 0)
            {
                RefreshItemPriceRecordDict();
            }
            if (ItemPriceRecordDict.Keys.Contains(id))
            {
                result = ItemPriceRecordDict[id];
            }
            return result;
        }


        public List<ItemPriceRecord> GetAllList()
        {
            if (ItemPriceRecordDict == null || ItemPriceRecordDict.Count == 0)
            {
                RefreshItemPriceRecordDict();
            }

            return ItemPriceRecordDict.Values.ToList();
        }

        public List<ItemPriceRecord> GetItemPriceRecordList(long platformId, string key, string itemId, int pageSize, int currentPage, out int totalCount)
        {
            List<ItemPriceRecord> result = new List<ItemPriceRecord>();
            totalCount = 0;
            if (ItemPriceRecordDict == null || ItemPriceRecordDict.Count == 0)
            {
                RefreshItemPriceRecordDict();
            }
            if (ItemPriceRecordDict.Any())
            {
                if (key == null)
                {
                    key = "";
                }
                else
                {
                    key = key.Trim();
                }
                if (itemId == null)
                {
                    itemId = "";
                }
                else
                {
                    itemId = itemId.Trim();
                }
                IEnumerable<ItemPriceRecord> records;
                records = ItemPriceRecordDict.Values;//.OrderByDescending(p => p.creationTime);

                if (!string.IsNullOrEmpty(key))
                {
                    records = records.Where(p => !string.IsNullOrEmpty(p.ItemName) && p.ItemName.ToLower().Contains(key.ToLower()) || !string.IsNullOrEmpty(p.FactoryModel) && p.FactoryModel.ToLower().Contains(key.ToLower()));
                }
                if (platformId != 0)
                {
                    records = records.Where(p => p.PlatformId == platformId);

                }
                else
                {
                    //using (var db = new DbBmsUser())
                    //{
                    //    var user = db.SingleGet(loginId);
                    //    if (user != null && user.userId > 0)
                    //    {
                    //        if (user.loginName.Trim().ToLower() == "admin" || user.UserRoleDict.Values.Select(p => p.roleId).Contains(1))
                    //        {

                    //        }
                    //        else
                    //        {
                    //            var platformIds = user.UserPlatformDict.Values.Select(p => p.platformId).ToList();
                    //            if (platformIds.Count == 0)
                    //            {
                    //                return result;
                    //            }
                    //            records = records.Where(p => platformIds.Contains(p.PlatformId));
                    //        }

                    //    }
                    //}
                }
                if (!string.IsNullOrEmpty(itemId))
                {

                }
                int total = records.Count(p => p.IsDeleted == false);

                result= records.OrderByDescending(p => p.Id).Where(p => p.IsDeleted == false).Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                totalCount = total;

            }
            return result;
        }

        public int UpdateVertifyStatus(long userId,long id,int status)
        {
            var obj = new ItemPriceRecord()
            {
                Id=id,
                Status=status,
                LastModificationTime=DateTime.Now,
                LastModifierUserId= userId
            };
            Update(obj,x=>x.LastModifierUserId,x=>x.Status,x=>x.LastModificationTime);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (ItemPriceRecordDict == null || ItemPriceRecordDict.Count == 0)
                {
                    RefreshItemPriceRecordDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (ItemPriceRecordDict.Keys.Contains(obj.Id))
                        {
                            ItemPriceRecordDict[obj.Id].Status = obj.Status;
                            ItemPriceRecordDict[obj.Id].LastModificationTime = obj.LastModificationTime;
                            ItemPriceRecordDict[obj.Id].LastModifierUserId = obj.LastModifierUserId;
                        }
                    }
                }
            }

            return excute;
        }
        
        public int UpdateSyncInfo(long userId, long id, bool isSync, DateTime syncTime)
        {
            var obj = new ItemPriceRecord()
            {
                Id=id,
                SyncStatus = isSync?1:2,
                RequestSyncTime= syncTime,
                LastModificationTime=DateTime.Now,
                LastModifierUserId= userId
            };
            Update(obj,x=>x.LastModifierUserId,x=>x.SyncStatus, x => x.RequestSyncTime, x=>x.LastModificationTime);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (ItemPriceRecordDict == null || ItemPriceRecordDict.Count == 0)
                {
                    RefreshItemPriceRecordDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (ItemPriceRecordDict.Keys.Contains(obj.Id))
                        {
                            ItemPriceRecordDict[obj.Id].Status = obj.Status;
                            ItemPriceRecordDict[obj.Id].RequestSyncTime = obj.RequestSyncTime;
                            ItemPriceRecordDict[obj.Id].LastModificationTime = obj.LastModificationTime;
                            ItemPriceRecordDict[obj.Id].LastModifierUserId = obj.LastModifierUserId;
                        }
                    }
                }
            }

            return excute;
        }
     public int UpdateSyncStatus(long userId, long flowId)
        {
            var obj = new ItemPriceRecord()
            {
                Id= flowId,
                SyncStatus = 3,//忽略
                LastModificationTime=DateTime.Now,
                LastModifierUserId= userId
            };
            Update(obj,x=>x.LastModifierUserId,x=>x.SyncStatus, x=>x.LastModificationTime);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (ItemPriceRecordDict == null || ItemPriceRecordDict.Count == 0)
                {
                    RefreshItemPriceRecordDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (ItemPriceRecordDict.Keys.Contains(obj.Id))
                        {
                            ItemPriceRecordDict[obj.Id].Status = obj.Status;
                            ItemPriceRecordDict[obj.Id].LastModificationTime = obj.LastModificationTime;
                            ItemPriceRecordDict[obj.Id].LastModifierUserId = obj.LastModifierUserId;
                        }
                    }
                }
            }

            return excute;
        }
    }
}
