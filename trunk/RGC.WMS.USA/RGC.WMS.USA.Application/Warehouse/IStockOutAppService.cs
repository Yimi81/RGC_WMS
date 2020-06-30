using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Warehouse.Dto;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;

namespace RGC.WMS.USA.Application.Warehouse
{
    public interface IStockOutAppService : IAppService
    {
        #region 出库操作
        ResponseDto<string> Create(long loginId, StockOutEditInput entity);

        ResponseDto<string> Update(long loginId, StockOutEditInput entity);

        ResponseDto<string> Delete(long loginId, long id);

        ResponseDto<string> Recovery(long loginId, long id);

        ResponseDto<string> UpdateStatus(long id, StockOutStatus status, long modifierUserId);

        #endregion

        #region 出库查看
        ResponseDto<StockOutOutput> Get(long id);

        ResponsePageDto<StockOutOutput> GetPage(StockOutFilterInput searchFilter);
        #endregion
    }
}
