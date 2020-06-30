using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Repositories.Item;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Item
{
    /// <summary>
    /// shane 2020/2/17
    /// </summary>
    public class ItemRepository : RepositoryBase<ItemInfo>, IItemRepository
    {
        private static IUnitOfWork _unitOfWork;
        private static readonly object _locker = new object();
        private static IRepository<ItemProduct> _itemProductRepository;
        private static IRepository<ItemModifyFlow> _itemModifyFlowRepository;
        //private static IRepository<ItemPriceRecord> _itemPriceRecordRepository;
        private static IItemPriceRecordRepository _itemPriceRecordRepository;
        /// <summary>
        /// 在售产品字典
        /// </summary>
        public static Dictionary<long, ItemInfo> ItemDict;
        public ItemRepository(DbContext context, IUnitOfWork unitOfWork,
            IRepository<ItemProduct> itemProductRepository,
            IRepository<ItemModifyFlow> itemModifyFlowRepository,
             IItemPriceRecordRepository itemPriceRecordRepository) : base(context)
        {
            _unitOfWork = unitOfWork;
            _itemProductRepository = itemProductRepository;
            _itemPriceRecordRepository = itemPriceRecordRepository;
            _itemModifyFlowRepository = itemModifyFlowRepository;
        }
        public void RefreshItemDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (ItemDict == null || ItemDict.Count == 0)
                {
                    ItemDict = new Dictionary<long, ItemInfo>();
                    ItemDict = GetItemDictFromDB();
                }
            }
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        public void ForceRefreshItemDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                ItemDict = new Dictionary<long, ItemInfo>();
                ItemDict = GetItemDictFromDB();
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, ItemInfo> GetItemDictFromDB()
        {
            Dictionary<long, ItemInfo> dict = new Dictionary<long, ItemInfo>();
            try
            {
                var list = TableNoTracking.ToList();

                var productList = _itemProductRepository.TableNoTracking.ToList();
                var flowList = _itemModifyFlowRepository.TableNoTracking.ToList();

                if (list != null && list.Any())
                {
                    dict = list.ToDictionary(p => p.Id);
                    foreach (var item in dict.Values)
                    {
                        item.ProductDict = productList.Where(p => p.ItemId == item.Id).ToDictionary(p => p.Id);
                        item.FlowDict = flowList.Where(p => p.ItemId == item.Id).ToDictionary(p => p.Id);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dict;
        }
        public int Add(ItemInfo entity, List<ItemProduct> productList)
        {
            int excute = 0;
            entity.CreationTime = DateTime.Now;
            using (var transaction = BeginTransaction())
            {
                try
                {
                    Insert(entity);
                    if (_unitOfWork.SaveChanges() > 0)
                    {
                        foreach (var item in productList)
                        {
                            item.ItemId = entity.Id;

                            _itemProductRepository.Insert(item);
                        }
                        _unitOfWork.SaveChanges();
                        if (ItemDict == null || ItemDict.Count == 0)
                        {
                            RefreshItemDict();
                        }
                        else
                        {
                            lock (_locker)
                            {
                                if (!ItemDict.Keys.Contains(entity.Id))
                                {
                                    ItemDict.Add(entity.Id, entity);

                                    var dic = ItemDict[entity.Id];
                                    foreach (var child in productList)
                                    {
                                        dic.ProductDict.Add(child.Id, child);
                                    }

                                }
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }


            return excute;
        }

        public int SingleUpdate(ItemInfo entity, List<ItemProduct> productList)
        {
            int excute = 0;
            using (var transaction = BeginTransaction())
            {
                try
                {
                    Update(entity);
                    excute = _unitOfWork.SaveChanges();
                    if (excute > 0)
                    {
                        var item = Get(entity.Id);
                        var itemProductIds = item.ProductDict.Values.Select(p => p.Id).ToList();
                        var modifyProductIds = productList.Select(p => p.Id).ToList();
                        var delIds = itemProductIds.Except(modifyProductIds).ToList();

                        foreach (var pro in item.ProductDict.Values)
                        {
                            if (delIds.Contains(pro.Id))
                            {
                                _itemProductRepository.Delete(pro);

                            }
                        }
                        foreach (var pro in productList)
                        {
                            pro.ItemId = entity.Id;
                            if (pro.Id == 0)
                            {
                                item.CreationTime = DateTime.Now;
                                item.CreatorUserId = entity.CreatorUserId;
                                _itemProductRepository.Insert(pro);

                            }
                        }
                        _unitOfWork.SaveChanges();
                        if (ItemDict == null || ItemDict.Count == 0)
                        {
                            RefreshItemDict();
                        }
                        if (ItemDict.Keys.Contains(entity.Id))
                        {



                            if (ItemDict == null || ItemDict.Count == 0)
                            {
                                RefreshItemDict();
                            }
                            else
                            {
                                lock (_locker)
                                {
                                    if (ItemDict.Keys.Contains(entity.Id))
                                    {
                                        var dic = ItemDict[entity.Id];
                                        ItemDict[entity.Id] = entity;
                                        ItemDict[entity.Id].FlowDict = dic.FlowDict;

                                        entity.ProductDict = new Dictionary<long, ItemProduct>();
                                        foreach (var pro in productList)
                                        {
                                            entity.ProductDict.Add(pro.Id, pro);
                                        }
                                        //var dic = ItemDict[entity.Id];
                                    }
                                }
                            }

                            transaction.Commit();

                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            return excute;
        }
        
        public int Delete(long loginId, long id)
        {
            var item = new ItemInfo
            {
                Id = id,
                IsDeleted = true,
                DeleterUserId = loginId,
                DeletionTime = DateTime.Now
            };
            Update(item, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                lock (_locker)
                {
                    if (ItemDict == null || ItemDict.Count == 0)
                    {
                        RefreshItemDict();
                    }
                    else
                    {
                        if (ItemDict.Keys.Contains(id))
                        {
                            ItemDict[id].IsDeleted = true;
                            ItemDict[id].DeleterUserId = loginId;
                            ItemDict[id].DeletionTime = DateTime.Now;
                        }
                    }
                }
            }

            return excute;
        }

        public ItemInfo Get(long Id)
        {
            ItemInfo result = new ItemInfo();

            if (ItemDict == null || ItemDict.Count == 0)
            {
                RefreshItemDict();
            }
            if (ItemDict.Keys.Contains(Id))
            {
                result = ItemDict[Id];
            }
            return result;
        }
        
        public List<ItemInfo> GetPlatformItemList(long PlatformId, string key, string uniqueId, int? status, int pageSize, int currentPage, out int totalCount)
        {
            List<ItemInfo> result = new List<ItemInfo>();
            totalCount = 0;
            if (ItemDict == null || ItemDict.Count == 0)
            {
                RefreshItemDict();
            }
            if (ItemDict.Any())
            {
                if (key == null)
                {
                    key = "";
                }
                else
                {
                    key = key.Trim();
                }
                if (uniqueId == null)
                {
                    uniqueId = "";
                }
                else
                {
                    uniqueId = uniqueId.Trim();
                }
                IEnumerable<ItemInfo> iItemList;
                iItemList = ItemDict.Values.OrderBy(p => p.ProductId).Distinct();

                if (!string.IsNullOrEmpty(key))
                {
                    iItemList = iItemList.Where(p => !string.IsNullOrEmpty(p.ItemName) && p.ItemName.ToLower().Contains(key.ToLower()) || !string.IsNullOrEmpty(p.FactoryModel) && p.FactoryModel.ToLower().Contains(key.ToLower()));
                }
                if (PlatformId != 0)
                {
                    iItemList = iItemList.Where(p => p.PlatformId == PlatformId);

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
                    //            iItemList = iItemList.Where(p => platformIds.Contains(p.PlatformId));
                    //        }

                    //    }
                    //}
                }
                if (!string.IsNullOrEmpty(uniqueId))
                {
                    iItemList = iItemList.Where(p => !string.IsNullOrEmpty(p.UniqueId) && p.UniqueId.ToLower().Contains(uniqueId.ToLower()));
                }
                if (status.HasValue)
                {
                    var IsValid = status.Value == 0 ? true : false;
                    iItemList = iItemList.Where(p => p.IsValid == IsValid);

                }
                int total = iItemList.Count(p => p.IsDeleted == false);

                var list = iItemList.OrderByDescending(p => p.Id).Where(p => p.IsDeleted == false).Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                result = list;
                //Dictionary<long, Product> productDict;
                //using (var db = new DbProduct())
                //{
                //    productDict = db.GetProductDictFromDB();
                //}
                //using (var db = new DbPlatform())
                //{
                //    var platformList = db.GetPlatformInfoDictFromDB();
                //    foreach (var item in list)
                //    {

                //        var model = platformList.Values.Where(p => p.id == item.PlatformId);
                //        if (model != null && model.Count() > 0)
                //        {
                //            item.PlatformName = model.FirstOrDefault().ename;
                //            item.SrcFull = string.IsNullOrWhiteSpace(item.Src) ? "" : BaseConfig.ImgSiteRootAddress + item.Src;
                //            item.WholeSalePriceString = ParseHelper.Fen2YuanString(item.WholeSalePrice);
                //            item.RetailPriceString = ParseHelper.Fen2YuanString(item.RetailPrice);
                //            result.list.Add(item);
                //        }
                //    }

                //}
                totalCount = (int)total;
            }
            return result;
        }
        
        public List<ItemInfo> ItemPageQuery(string key, long platformId, int pageSize, int currentPage, out int totalCount)
        {
            List<ItemInfo> result = new List<ItemInfo>();
            totalCount = 0;
            if (ItemDict == null || ItemDict.Count == 0)
            {
                RefreshItemDict();
            }
            if (ItemDict.Count > 0)
            {
                int total = 0;
                var list = new List<ItemInfo>();

                IEnumerable<ItemInfo> iItemList;
                iItemList = ItemDict.Values.OrderBy(p => p.ProductId).DistinctBy(p => new { p.ProductId, p.ItemName });
                if (platformId != 0)
                {
                    iItemList = iItemList.Where(p => p.PlatformId == platformId);
                }
                if (!string.IsNullOrEmpty(key))
                {
                    iItemList = iItemList.Where(p => !string.IsNullOrEmpty(p.ItemName) && p.ItemName.ToLower().Contains(key.ToLower()) || !string.IsNullOrEmpty(p.FactoryModel) && p.FactoryModel.ToLower().Contains(key.ToLower()));
                }
                total = iItemList.Count();
                list = iItemList.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();

                result = list;

                totalCount = (int)total;
            }
            return result;
        }
               
        public int UpdateAndAddRecord(long adminId, long ItemId, int wholeSalePrice, int retailPrice)
        {
            var excute = 0;

            using (var trans = BeginTransaction())
            {
                try
                {
                    var item = new ItemInfo
                    {
                        Id = ItemId,
                        WholeSalePrice = wholeSalePrice,
                        //RetailPrice = retailPrice,
                        LastModifierUserId = adminId,
                        LastModificationTime = DateTime.Now,
                    };
                    Update(item, x => x.WholeSalePrice, x => x.LastModifierUserId, x => x.LastModificationTime);
                    var obj = Get(ItemId);
                    var model = new ItemPriceRecord();

                    model.CreationTime = DateTime.Now;
                    model.CreatorUserId = adminId;
                    //model.RetailPrice = 0;
                    model.WholeSalePrice = wholeSalePrice;
                    model.RetailPrice = retailPrice;
                    model.ItemId = obj.Id;
                    model.ProductId = obj.ProductId;
                    model.ItemName = obj.ItemName;
                    model.FactoryModel = obj.FactoryModel;
                    model.Src = obj.Src ?? "";
                    model.PlatformId = obj.PlatformId;
                    model.PlatformName = obj.PlatformName;
                    model.Status = 0;
                    excute = _itemPriceRecordRepository.Add(model);
                    //excute = _unitOfWork.SaveChanges();

                    if (excute > 0)
                    {
                        lock (_locker)
                        {
                            if (ItemDict == null || ItemDict.Count == 0)
                            {
                                RefreshItemDict();
                            }
                            else
                            {
                                if (ItemDict.Keys.Contains(ItemId))
                                {
                                    ItemDict[ItemId].WholeSalePrice = wholeSalePrice;
                                    ItemDict[ItemId].RetailPrice = retailPrice;
                                    ItemDict[ItemId].LastModifierUserId = adminId;
                                    ItemDict[ItemId].LastModificationTime = item.LastModificationTime;
                                }
                            }


                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }

            }
            return excute;
        }

        public int ApplyPriceAndUpdate(long priceId, long modifierUserId)
        {
            var excute = 0;

            using (var trans = BeginTransaction())
            {
                try
                {
                    var price = _itemPriceRecordRepository.Get(priceId);
                    var item = new ItemInfo
                    {
                        Id = price.ItemId,
                        PreRetailPrice = price.RetailPrice,
                        ValidTime = price.ValidTime.Value,
                        LastModifierUserId = modifierUserId,
                        LastModificationTime = DateTime.Now,
                    };
                    Update(item, x => x.PreRetailPrice, x => x.ValidTime, x => x.LastModifierUserId, x => x.LastModificationTime);
                    excute = _itemPriceRecordRepository.UpdateVertifyStatus(modifierUserId, priceId, 0);
                    //excute = _unitOfWork.SaveChanges();

                    if (excute > 0)
                    {
                        lock (_locker)
                        {
                            if (ItemDict == null || ItemDict.Count == 0)
                            {
                                RefreshItemDict();
                            }
                            else
                            {
                                if (ItemDict.Keys.Contains(item.Id))
                                {
                                    ItemDict[item.Id].PreRetailPrice = item.PreRetailPrice;
                                    ItemDict[item.Id].ValidTime = item.ValidTime;
                                    ItemDict[item.Id].LastModifierUserId = item.LastModifierUserId;
                                    ItemDict[item.Id].LastModificationTime = item.LastModificationTime;
                                }
                            }
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }

            }
            return excute;
        }
        public int UpdateStatus(long Id, long modifierUserId)
        {
            var item = DeepCopyByReflect<ItemInfo>(Get(Id));
            item.IsValid = !item.IsValid;

            var model = new ItemInfo
            {
                Id = Id,
                IsValid = item.IsValid,
                IsDeleted = true,
                LastModifierUserId = modifierUserId,
                LastModificationTime = DateTime.Now,
            };
            Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                lock (_locker)
                {
                    if (ItemDict == null || ItemDict.Count == 0)
                    {
                        RefreshItemDict();
                    }
                    else
                    {
                        if (ItemDict.Keys.Contains(Id))
                        {
                            ItemDict[Id].IsValid = item.IsValid;
                            ItemDict[Id].LastModifierUserId = modifierUserId;
                            ItemDict[Id].LastModificationTime = DateTime.Now;
                        }
                    }


                }
            }

            return excute;
        }

        public List<(string value, long id)> SearchPageQuery(string key)
        {
            List<(string value, long id)> result = new List<(string value, long id)>();

            var list = new List<(string value, long id)>();
            if (ItemDict == null || ItemDict.Count == 0)
            {
                RefreshItemDict();
            }
            var iList = ItemDict.Values.DistinctBy(p => new { p.ProductId }).ToList();

            if (!string.IsNullOrEmpty(key))
            {
                key = key.ToLower().Trim();
                iList.ForEach(model =>
                {
                    if (model.FactoryModel.ToLower().Contains(key))
                    {
                        list.Add((value:model.FactoryModel, id:model.ProductId));
                    }
                });
            }
            else
            {
                iList.ForEach(model =>
                {
                    list.Add((value: model.FactoryModel, id: model.ProductId));
                });
            }

            result = list;
            //}
            return result;
        }
    }
}
