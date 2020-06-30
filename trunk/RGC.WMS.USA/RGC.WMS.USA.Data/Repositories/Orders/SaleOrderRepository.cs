using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Data.Repositories.Sku;
using RGC.WMS.USA.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Repositories.Orders;
using RGC.WMS.USA.Domain.Repositories.Sku;
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
    public class SaleOrderRepository : RepositoryBase<SaleOrder>, ISaleOrderRepository
    {
        private static IUnitOfWork _unitOfWork;

        private static readonly object _locker = new object();

        //private static Dictionary<long, SaleOrder> SaleOrderDict;
        private static ISaleOrderItemRepository _saleOrderItemRepository;
        private static ISkuStockRepository _skuStockRepository;


        public SaleOrderRepository(
            DbContext context,
            IUnitOfWork unitOfWork,
            ISaleOrderItemRepository saleOrderItemRepository,
            ISkuStockRepository skuStockRepository
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _saleOrderItemRepository = saleOrderItemRepository;
            _skuStockRepository = skuStockRepository;
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

        public List<SaleOrder> OrderPageQuery(SaleOrderSearchFilterDo searchFilter, out int count)
        {
            List<SaleOrder> result = new List<SaleOrder>();

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

            var list = orderList.OrderByDescending(p => p.OrderDate).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();
            result = list;
            count = (int)total;

            return result;
        }

        public SaleOrder SingleInsert(SaleOrder order, long adminId = 0)
        {
            var excute = 0;
            using (var trans = BeginTransaction())
            {
                try
                {
                    order.CreatorUserId = adminId;
                    order.CreationTime = DateTime.Now;
                    //order.LastModificationTime = null;
                    //order.LastModifierUserId = 0;
                    base.Insert(order);
                    _unitOfWork.SaveChanges();
                    foreach (var item in order.Items)
                    {
                        item.CreationTime = DateTime.Now;
                        item.CreatorUserId = adminId;
                        //item.LastModificationTime = DateTime.MinValue;
                        //item.LastModifierUserId = 0;
                        item.SaleOrderId = order.Id;
                        _saleOrderItemRepository.Insert(item);
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

        public SaleOrder ManulOrderInsert(SaleOrder order, long adminId = 0)
        {
            var excute = 0;
            var listStockId = new List<long>();
            var listStock = new List<SkuStock>();
            using (var trans = BeginTransaction())
            {
                try
                {
                    order.CreatorUserId = adminId;
                    order.CreationTime = DateTime.Now;
                    //order.LastModificationTime = null;
                    //order.LastModifierUserId = 0;
                    base.Insert(order);
                    _unitOfWork.SaveChanges();
                    foreach (var item in order.Items)
                    {
                        item.CreationTime = DateTime.Now;
                        item.CreatorUserId = adminId;
                        //item.LastModificationTime = DateTime.MinValue;
                        //item.LastModifierUserId = 0;
                        item.SaleOrderId = order.Id;
                        _saleOrderItemRepository.Insert(item);
                        var stock = _skuStockRepository.Get(item.SkuStockId);
                        //stock.CurrentStock = stock.CurrentStock - item.Qty;
                        stock.OrderStock = stock.OrderStock + item.Qty;
                        _skuStockRepository.Update(stock);
                        listStockId.Add(item.SkuStockId);
                        listStock.Add(stock);
                    }
                    excute = _unitOfWork.SaveChanges();

                    if (excute > 0)
                    {
                        //新增成功
                        if (SkuStockRepository.SkuStockDict == null || SkuStockRepository.SkuStockDict.Count == 0)
                        {
                            _skuStockRepository.RefreshSkuStockDict();
                        }
                        lock (_locker)
                        {
                            foreach (var item in listStockId)
                            {
                                if (SkuStockRepository.SkuStockDict.Keys.Contains(item) == false)
                                {

                                }
                                else
                                {
                                    var stock = listStock.FirstOrDefault(p => p.Id == item);
                                    if (stock != null )
                                    {
                                        SkuStockRepository.SkuStockDict[item] = stock;
                                    }
                                   
                                }

                            }
                         
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
            return order;
        }

        public int SingleUpdateSaleOrder(SaleOrder order)
        {
            var excute = 0;
            base.Update(order);
            excute = _unitOfWork.SaveChanges();
            return excute;
        }

        public int ManulOrderStockOut(long adminId, long orderId)
        {
            var excute = 0;
            var orderItem = _saleOrderItemRepository.TableNoTracking.Where(p => p.SaleOrderId == orderId).ToList();
            if (orderItem != null && orderItem.Count > 0)
            {
                foreach (var item in orderItem)
                {
                    var stock = _skuStockRepository.Get(item.SkuStockId);
                    stock.CurrentStock = stock.CurrentStock - item.Qty;
                    stock.OrderStock = stock.OrderStock - item.Qty;
                    _skuStockRepository.Update(stock);

                }
            }
         
            return excute;

        }


    }
}
