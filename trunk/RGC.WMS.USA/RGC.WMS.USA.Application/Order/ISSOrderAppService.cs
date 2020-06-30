using System.Collections.Generic;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.Application.Order
{
    public interface ISSOrderAppService : IAppService
    {
        ResponsePageDto<SSOrder> SSOrderPageGet(SSOrderSearchFilterDo dto, long userId);
        ResponseDto<SSOrder> SSOrderGet(long id, long userId);
        ResponseDto<string> SSOrderSave(List<SSOrder> orders);
        ResponseDto<string> SSOrderStockOut(long id, long userId);
    }
}
