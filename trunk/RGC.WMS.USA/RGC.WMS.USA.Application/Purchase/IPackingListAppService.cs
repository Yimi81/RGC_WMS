using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Purchase.Dto;
using RGC.WMS.USA.Domain.Entities.Purchase.Enum;

namespace RGC.WMS.USA.Application.Purchase
{
    public interface IPackingListAppService : IAppService
    {
        ResponseDto<string> UpdateStatus(long id, CargoStatus status, long loginId);

        ResponseDto<PackingListOutput> Get(long id);

        ResponsePageDto<PackingListOutput> GetPage(PackingListFilterInput searchFilter);
    }
}
