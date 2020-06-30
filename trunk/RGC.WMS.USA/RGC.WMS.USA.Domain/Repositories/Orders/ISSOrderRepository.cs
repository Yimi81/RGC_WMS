using System.Collections.Generic;
using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.Domain.Repositories.Orders
{
    public interface ISSOrderRepository : IRepository<SSOrder>
    {
        //SSOrder SingleGetById(long orderId);

        SSOrder SingleInsert(SSOrder order);
        int BatchInsertOrUpdate(List<SSOrder> listOrder);
        int SingleUpdateSSOrder(SSOrder order);
        List<SSOrder> OrderPageQuery(SSOrderSearchFilterDo searchFilter, out int count);
    }
}
