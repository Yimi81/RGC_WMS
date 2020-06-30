using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms
{
    public interface IBmsMenuAppService : IAppService
    {
        ResponseDto<BmsMenuDto> CreateMenu(BmsMenuDto request, long creatorUserId);

        ResponseDto<string> DeleteMenu(Int64 menuId, long deleterUserId);

        ResponseDto<BmsMenuDto> ModifyMenu(BmsMenuDto request, long modifierUserId);

        ResponseDto<string> ModifyMenuSeqNo(List<BmsMenuSeqNoDto> request, long modifierUserId);

        ResponseDto<List<BmsMenuTreeDto>> GetMenuTree(long parentId);

        ResponseDto<List<BmsMenuTreeDto>> GetPermissionSystemMenuTree(long parentId, long userId, long orgId);

        ResponseDto<BmsMenuTreeDto> GetMenuDetail(long id);

        ResponseDto<List<string>> GetPowerNameList(List<Int64> menuIds);

        ResponseDto<string> ForceRefreshDict();
    }
}
