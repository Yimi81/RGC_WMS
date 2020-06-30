using System.Collections.Generic;
using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.Domain.Repositories.Orders
{
    public interface ISaleOrderRepository : IRepository<SaleOrder>
    {
        SaleOrder SingleInsert(SaleOrder order, long adminId = 0);
        SaleOrder ManulOrderInsert(SaleOrder order, long adminId = 0);
        int SingleUpdateSaleOrder(SaleOrder order);
        List<SaleOrder> OrderPageQuery(SaleOrderSearchFilterDo searchFilter, out int count);
        int ManulOrderStockOut(long adminId, long orderId);
    }
}
