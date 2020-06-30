using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Warehouse.Dto;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;

namespace RGC.WMS.USA.Application.Warehouse
{
    public interface IStockInAppService : IAppService
    {
        #region 入库操作
        ResponseDto<string> Create(long loginId, StockInEditInput entity);

        ResponseDto<string> Update(long loginId, StockInEditInput entity);

        ResponseDto<string> Delete(long loginId, long id);

        ResponseDto<string> Recovery(long loginId, long id);

        ResponseDto<string> UpdateStatus(long id, StockInStatus status, long modifierUserId);

        #endregion

        #region 入库查看
        ResponseDto<StockInOutput> Get(long id);

        ResponsePageDto<StockInOutput> GetPage(StockInFilterInput searchFilter);
        #endregion
    }
}
