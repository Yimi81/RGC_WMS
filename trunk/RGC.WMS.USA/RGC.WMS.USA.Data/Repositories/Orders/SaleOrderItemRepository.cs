using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Order;
using RGC.WMS.USA.Domain.Repositories.Orders;
using RGC.WMS.USA.Domain.Uow;

namespace RGC.WMS.USA.Data.Repositories.Orders
{
    /// <summary>
    /// 订单详情仓储
    /// 创建：jerry 2020/03/06
    /// </summary>
    public class SaleOrderItemRepository : RepositoryBase<SaleOrderItem>, ISaleOrderItemRepository
    {
        private static IUnitOfWork _unitOfWork;

        //private static readonly object _locker = new object();

        //public static Dictionary<long, SSOrderItem> SSOrderItemDict;

        public SaleOrderItemRepository(
            DbContext context,
            IUnitOfWork unitOfWork
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        //public void RefreshOrderItemDict()
        //{
        //    ///加锁，保证同时只有一个线程访问
        //    lock (_locker)
        //    {
        //        if (SSOrderItemDict == null || SSOrderItemDict.Count == 0)
        //        {
        //            SSOrderItemDict = new Dictionary<long, SSOrderItem>();
        //            SSOrderItemDict = GetSSOrderItemDictFromDB();
        //        }
        //    }
        //}

        /// <summary>
        /// 从数据库获取全部订单详情
        /// 
        /// </summary>
        /// <returns></returns>
        //private Dictionary<long, SSOrderItem> GetSSOrderItemDictFromDB()
        //{
        //    Dictionary<long, SSOrderItem> dict = new Dictionary<long, SSOrderItem>();

        //    var list = TableNoTracking.ToList();
        //    if (list != null && list.Count() > 0)
        //    {
        //        dict = list.ToDictionary(p => p.Id);
        //    }
        //    return dict;
        //}

        /// <summary>
        /// 更新字典
        /// 
        /// </summary>
        //private void UpdateDict(SSOrderItem orderItem)
        //{
        //    if (SSOrderItemDict == null || SSOrderItemDict.Count == 0)
        //    {
        //        RefreshOrderItemDict();
        //    }
        //    lock (_locker)
        //    {
        //        if (SSOrderItemDict.ContainsKey(orderItem.Id))
        //        {
        //            SSOrderItemDict[orderItem.Id] = orderItem;
        //        }
        //        else
        //        {
        //            SSOrderItemDict.Add(orderItem.Id, orderItem);
        //        }
        //    }
        //}

        //public SSOrderItem SingleGetById(long id)
        //{
        //    if(SSOrderItemDict == null || SSOrderItemDict.Count == 0)
        //    {
        //        RefreshOrderItemDict();
        //    }
        //    var orderItem = SSOrderItemDict[id];
        //    return orderItem;
        //}

        //public int SingleUpdate(SSOrderItem orderItem)
        //{
        //    Update(orderItem);
        //    int excute = _unitOfWork.SaveChanges();
        //    if (excute <= 0) return 0;
        //    UpdateDict(orderItem);
        //    return excute;
        //}

        /// <summary>
        /// 根据orderId获取对应的orderitem
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //public List<SSOrderItem> GetOrderItemList(long orderId)
        //{
        //    if (SSOrderItemDict == null || SSOrderItemDict.Count == 0)
        //    {
        //        RefreshOrderItemDict();
        //    }
        //    var list = SSOrderItemDict.Values.Where(p => !p.IsDeleted && p.SSOrderId == orderId).ToList();
        //    return list;
        //}
    }
}
