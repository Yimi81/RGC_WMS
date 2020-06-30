using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.System.Dto;

namespace RGC.WMS.USA.Application.System
{
    public interface ISystemInfoAppService: IAppService
    {
        ResponseDto<string> CreateSystem(SystemCreateOrUpdateDto createSystem, long creatorUserId);
        ResponsePageDto<SystemListDto> GetSystemList(SearchFilterDto searchFilter);
        ResponseDto<SystemCreateOrUpdateDto> GetSystemDetail(long systemId);
        ResponseDto<string> ModifySystem(SystemCreateOrUpdateDto modifySystem, long modifierUserId);
        ResponseDto<string> DeleteSystem(long systemId, long deleterUserId);
        ResponsePageDto<SystemSimpleListDto> GetAllSystemList();
        ResponseDto<string> ForceRefreshDict();
    }
}
