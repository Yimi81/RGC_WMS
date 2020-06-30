using System.Collections.Generic;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Order.Dto;
using RGC.WMS.USA.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.Application.Order
{
    public interface ISaleOrderAppService : IAppService
    {
        ResponsePageDto<SaleOrder> SaleOrderPageGet(SaleOrderSearchFilterDo dto, long userId);
        ResponseDto<SaleOrder> SaleOrderGet(long id, long userId);
        ResponseDto<string> SaleOrderSave(SaleOrder order, long adminId);
        ResponseDto<string> SaleOrderManualAdd(SaleOrderDto order, long adminId);
        ResponseDto<string> OrderManualStockOut(long adminId, long orderId);
    }
}
