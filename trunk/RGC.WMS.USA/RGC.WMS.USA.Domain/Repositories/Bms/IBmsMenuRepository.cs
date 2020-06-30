using HuigeTec.Core.Domain.Entities;
using HuigeTec.Core.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Bms
{
    public interface IBmsMenuRepository : IRepository<BmsMenu>
    {
        void ForceRefreshDict();
        int SingleInsert(BmsMenu request);

        bool SingleDelete(long menuId, long deleterUserId);

        int SingleUpdate(BmsMenu request);

        BmsMenu SingleGet(long id);

        bool SingleUpdateMenuSeqNo(long modifierUserId, List<Tuple<long, int>> list);

        List<BmsMenu> GetAll();

        List<BmsMenu> GetTree(long parentId);

        List<BmsMenu> GetChildren(long parentId);

        List<BmsMenu> GetAuthorizationMenu(long systemId, List<long> menuIds);
    }
}
