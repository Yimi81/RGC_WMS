using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain.Entities.Bms;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms
{
    public interface IBmsUserAppService : IAppService
    {
        ResponseDto<BmsUserExtend> CanLogin(string loginName, string password);
        ResponseDto<string> CreateBmsUser(BmsUserCreateOrUpdateDto createUser, long creatorUserId);
        ResponsePageDto<BmsUserListDto> GetBmsUserList(SearchFilterDto searchFilter);
        ResponseDto<BmsUserCreateOrUpdateDto> GetBmsUserDetail(long Id);
        ResponseDto<string> UpdateBmsUser(BmsUserCreateOrUpdateDto createUser, long ModifierUserId);
        ResponseDto<string> UpdateBmsUserStatus(BmsUserCreateOrUpdateDto updateUser, long ModifierUserId);
        ResponseDto<string> ChangeBmsUserPassword(BmsUserChangePwdDto input, long ModifierUserId);
        ResponseDto<string> DeleteBmsUser(long userId, long ModifierUserId);
        ResponseDto<BmsUserMenuTreeDto> GetUserWholeMenuTree(long userId, long organizationId);
        ResponseDto<int> ManageModifyUserMenuTree(BmsUserMenuTreeModifyDto request, long userId);
        ResponseDto<List<long>> GetUserSystemIds(long userId);
        ResponseDto<string> UpdateGrantedSystem(List<long> systemIds, long userId, long modifierUserId);
        ResponseDto<List<long>> GetUserRoleIds(long userId);
        ResponseDto<string> UpdateGrantedRole(List<long> roleIds, long userId, long modifierUserId);
        ResponseDto<List<long>> GetUserPlatformIds(long userId);
        ResponseDto<string> UpdateGrantedPlatform(List<long> platformIds, long userId, long modifierUserId);
        ResponseDto<string> ForceRefreshDict();
        ResponseDto<BmsUserDto> GetCurrentUserDetail(long userId);
    }
}
