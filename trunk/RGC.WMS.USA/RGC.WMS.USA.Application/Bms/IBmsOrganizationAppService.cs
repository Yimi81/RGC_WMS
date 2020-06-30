using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms
{
    public interface IBmsOrganizationAppService:IAppService
    {

        ResponseDto<BmsOrganizationDto> ManageCreateOrganization(BmsOrganizationDto request, long userId);

        ResponseDto<BmsOrganizationDto> ManageDeleteOrganization(Int64 menuId, long userId);

        ResponseDto<BmsOrganizationDto> ManageModifyOrganization(BmsOrganizationDto request, long userId);

        ResponseDto<List<BmsOrganizationTreeDto>> ManageGetOrganizationTree();

        ResponseDto<List<BmsOrganizationDto>> ManageGetChildrenOrganization(long organizationId);

        ResponseDto<BmsOrganizationDto> ManageGetOrganizationDetail(long organizationId);

        ResponsePageDto<BmsUserDto> GetOrganizationUserList(long orgId, string key, int pageSize, int currentPage);

        ResponsePageDto<BmsUserDto> GetOtherOrganizationUserList(long orgId, string key, int pageSize, int currentPage);

        ResponseDto<string> AddUser(BmsOrganizationUserCreateDto request, long loginId);

        ResponseDto<string> RemoveUser(long userId, long organizationId,long loginId);

        ResponseDto<BmsMenuTreeDto> GetWholeMenuTree(long organizationId);

        ResponseDto<int> ModifyMenuTree(long userId, BmsOrganizationMenuTreeModifyDto request);

        ResponseDto<string> ForceRefreshDict();
    }
}
