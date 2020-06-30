using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Warehouse.Dto;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Warehouse
{
    public interface IWarehouseAppService : IAppService
    {
        #region 仓库操作
        ResponseDto<string> Create(long loginId, WarehouseEditInput entity);

        ResponseDto<string> Update(long loginId, WarehouseEditInput entity);

        ResponseDto<string> Delete(long loginId, long id);

        ResponseDto<string> Recovery(long loginId, long id);

        ResponseDto<string> UpdateStatus(long id, WarehouseStatus status, long modifierUserId);

        #endregion

        #region 仓库查看
        ResponseDto<WarehouseDto> Get(long id);

        ResponsePageDto<WarehouseDto> GetPage(WarehouseFilterInput searchFilter);

        ResponseDto<List<WarehouseFilterSimpleOutput>> GetWarehouseSimpleList();

        ResponseDto<string> ForceRefreshWarehouseDict();
        #endregion
    }
}
