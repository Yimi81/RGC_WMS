using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.System.Dto;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.System
{
    public interface IPlatformAppService: IAppService
    {
        ResponsePageDto<PlatformListDto> GetPlatformList(SearchFilterDto searchFilter);
        ResponseDto<List<PlatformSimpleListDto>> GetPlatformSimpleList();
        ResponseDto<string> Create(PlatformCreateOrUpdateDto createPlatform, long creatorUserId);
        ResponseDto<PlatformCreateOrUpdateDto> GetPlatformDetail(long id);
        ResponseDto<string> ModifyRole(PlatformCreateOrUpdateDto modifyPlatform, long modifierUserId);
        ResponseDto<string> DeleteRole(long id, long deleterUserId);
        ResponsePageDto<BmsUsersimpleListDto> GetGrantedUsers(long platformId,int currentPage);
        ResponseDto<string> ForceRefreshDict();
    }
}
