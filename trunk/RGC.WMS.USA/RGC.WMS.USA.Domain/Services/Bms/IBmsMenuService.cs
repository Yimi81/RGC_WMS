using HuigeTec.Core.Domain.Entities;
using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Services.Bms
{
    public interface IBmsMenuService:IDomainServiceBase
    {
        ResponseDo<BmsMenu> CreateMenu(BmsMenu request);

        ResponseDo<string> DeleteMenu(Int64 menuId, long deleterUserId);

        ResponseDo<BmsMenu> ModifyMenu(BmsMenu request);

        ResponseDo<string> ModifyMenuSeqNo(List<Tuple<long, int>> list, long modifierUserId);

        ResponseDo<List<BmsMenu>> GetMenuTree(long parentId);

        ResponseDo<BmsMenu> GetMenuDetail(long id);

        ResponseDo<List<BmsMenu>> GetChildren(long parentId);

        ResponseDo<List<BmsMenuTreeDo>> GetWholeTree(long parentId, List<Int64> menuIds);

        ResponseDo<List<BmsMenuTreeDo>> GetStstemTree(long parentId, List<Int64> menuIds);

        List<string> GetPowerNameList(List<Int64> menuIds);

        List<long> MenuIdsGetWithParentId(List<long> list);

        List<long> RecursionGetParentMenuIds(List<long> list);

        List<long> MenuIdsGetBySuperAdmin();

        List<BmsMenuTreeDo> WholeTreeGet(Int64 parentMenuId, List<Int64> menuIds, List<Int64> removedMenuIds, List<Int64> grantedMenuIds);
    }

}
