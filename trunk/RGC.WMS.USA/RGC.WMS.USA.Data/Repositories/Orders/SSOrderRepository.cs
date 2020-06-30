using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;
using RGC.WMS.USA.Domain.Repositories.Orders;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Orders
{
    /// <summary>
    /// 订单仓储
    /// 创建：jerry 2020/03/06
    /// </summary>
    public class SSOrderRepository : RepositoryBase<SSOrder>, ISSOrderRepository
    {
        private static IUnitOfWork _unitOfWork;

        private static readonly object _locker = new object();

        //private static Dictionary<long, SSOrder> SSOrderDict;
        private static ISSOrderItemRepository _ssOrderItemRepository;


        public SSOrderRepository(
            DbContext context,
            IUnitOfWork unitOfWork,
            ISSOrderItemRepository ssOrderItemRepository

            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _ssOrderItemRepository = ssOrderItemRepository;

        }

        //public void RefreshSSOrderDict()
        //{
        //    ///加锁，保证同时只有一个线程访问
        //    lock (_locker)
        //    {
        //        if (SSOrderDict == null || SSOrderDict.Count == 0)
        //        {
        //            SSOrderDict = new Dictionary<long, SSOrder>();
        //            SSOrderDict = GetSSOrderDictFromDB();
        //        }
        //    }
        //}

        /// <summary>
        /// 从数据库获取全部订单
        /// jerry 2020/03/06
        /// </summary>
        /// <returns></returns>
        //private Dictionary<long, SSOrder> GetSSOrderDictFromDB()
        //{
        //    Dictionary<long, SSOrder> dict = new Dictionary<long, SSOrder>();

        //    var list = TableNoTracking.ToList();
        //    var itemList = _ssOrderItemRepository.TableNoTracking.ToList();


        //    if (list != null && list.Count() > 0)
        //    {
        //        dict = list.ToDictionary(p => p.Id);
        //        foreach (var item in dict.Values)
        //        {
        //            item.ItemDict = itemList.Where(p => p.SSOrderId == item.Id).ToDictionary(p => p.Id);

        //        }
        //    }
        //    return dict;
        //}

        /// <summary>
        /// 
        /// jerry 2020/03/06
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //public SSOrder SingleGetById(long orderId)
        //{
        //    if (SSOrderDict == null || SSOrderDict.Count == 0)
        //    {
        //        RefreshSSOrderDict();
        //    }
        //    if (SSOrderDict.ContainsKey(orderId))
        //    {
        //        return SSOrderDict[orderId];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public List<SSOrder> OrderPageQuery(SSOrderSearchFilterDo searchFilter, out int count)
        {
            List<SSOrder> result = new List<SSOrder>();

            count = 0;
            var orderList = TableNoTracking;

            if (!string.IsNullOrEmpty(searchFilter.SearchKey))
            {
                orderList = orderList.Where(p => p.OrderNumber.ToLower().Contains(searchFilter.SearchKey.ToLower().Trim()));
            }
            if (searchFilter.OrderStatusList != null && searchFilter.OrderStatusList.Count > 0)
            {
                orderList = orderList.Where(p => searchFilter.OrderStatusList.Contains(p.OrderStatus));
            }
            if (searchFilter.StartTime.HasValue)
            {
                orderList = orderList.Where(p => Convert.ToDateTime(p.OrderDate) >= searchFilter.StartTime.Value);
            }
            if (searchFilter.EndTime.HasValue)
            {
                orderList = orderList.Where(p => Convert.ToDateTime(p.OrderDate) < searchFilter.EndTime.Value);
            }

            int total = orderList.Count();

            var list = orderList.OrderByDescending(p => p.ModifyDate).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();
            result = list;
            count = (int)total;

            return result;
        }

        public SSOrder SingleInsert(SSOrder order)
        {
            var excute = 0;
            using (var trans = BeginTransaction())
            {
                try
                {
                    order.CreatorUserId = 0;
                    order.CreationTime = DateTime.Now;
                    //order.LastModificationTime = null;
                    //order.LastModifierUserId = 0;
                    base.Insert(order);
                    _unitOfWork.SaveChanges();
                    foreach (var item in order.Items)
                    {
                        item.CreationTime = DateTime.Now;
                        item.CreatorUserId = 0;
                        //item.LastModificationTime = DateTime.MinValue;
                        //item.LastModifierUserId = 0;
                        item.SSOrderId = order.Id;
                        _ssOrderItemRepository.Insert(item);
                    }
                    excute = _unitOfWork.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return order;
        }

        public int BatchInsertOrUpdate(List<SSOrder> orderList)
        {
            var excute = 0;
            using (var trans = BeginTransaction())
            {
                try
                {
                    foreach (var order in orderList)
                    {
                        var ssorder = TableNoTracking.Where(p => p.OrderId == order.OrderId).FirstOrDefault();
                        if (ssorder != null)//update
                        {
                            var cTime = ssorder.CreationTime;
                            var cId = ssorder.CreatorUserId;
                            var id = ssorder.Id;
                            ssorder = order;
                            ssorder.Id = id;
                            ssorder.CreationTime = cTime;
                            ssorder.CreatorUserId = cId;
                            ssorder.LastModificationTime = DateTime.Now;
                            ssorder.LastModifierUserId = 0;
                            Update(ssorder);
                            _unitOfWork.SaveChanges();
                            foreach (var item in order.Items)
                            {
                                var ssitem = _ssOrderItemRepository.TableNoTracking.Where(p => p.OrderItemId == item.OrderItemId && p.SSOrderId == ssorder.Id).FirstOrDefault();
                                if (ssitem !=null)
                                {
                                    var icTime = ssitem.CreationTime;
                                    var icId = ssitem.CreatorUserId;
                                    var iId = ssitem.Id;
                                    ssitem = item;
                                    ssitem.Id = iId;
                                    ssitem.CreationTime = icTime;
                                    ssitem.CreatorUserId = icId;
                                    ssitem.LastModificationTime = DateTime.Now;
                                    ssitem.LastModifierUserId = 0;
                                    _ssOrderItemRepository.Update(ssitem);
                                }
                               
                            }
                            excute = _unitOfWork.SaveChanges();
                        }
                        else
                        {
                            order.CreatorUserId = 0;
                            order.CreationTime = DateTime.Now;
                            //order.LastModificationTime = null;
                            //order.LastModifierUserId = 0;
                            base.Insert(order);
                            _unitOfWork.SaveChanges();
                            foreach (var item in order.Items)
                            {
                                item.CreationTime = DateTime.Now;
                                item.CreatorUserId = 0;
                                //item.LastModificationTime = DateTime.MinValue;
                                //item.LastModifierUserId = 0;
                                item.SSOrderId = order.Id;
                                _ssOrderItemRepository.Insert(item);
                            }
                            excute = _unitOfWork.SaveChanges();
                        }
                     
                      
                    }
                  
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return excute;
        }

        public int SingleUpdateSSOrder(SSOrder order)
        {
            var excute = 0;
            base.Update(order);
            excute = _unitOfWork.SaveChanges();
            return excute;
        }


    }
}
