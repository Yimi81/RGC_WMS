using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms
{
    public interface IBmsRoleAppService : IAppService
    {
        ResponseDto<List<BmsRoleListDto>> ManageGetRoleList(string key);
        ResponseDto<string> CreateRole(BmsRoleCreateOrUpdateDto createRole, long creatorUserId);
        ResponseDto<BmsRoleCreateOrUpdateDto> GetRoleDetail(long roleId);
        ResponseDto<string> ModifyRole(BmsRoleCreateOrUpdateDto modifyRole, long modifierUserId);
        ResponseDto<string> DeleteRole(long roleId, long deleterUserId);
        ResponsePageDto<BmsRoleSimpleListDto> GetRoleSimpleList();
        ResponsePageDto<BmsUsersimpleListDto> GetGrantedUsers(long roleId, int currentPage);
        ResponseDto<string> ForceRefreshDict();
    }
}
