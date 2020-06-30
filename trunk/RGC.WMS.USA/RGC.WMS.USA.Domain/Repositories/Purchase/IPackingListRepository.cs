using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Purchase;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Purchase
{
    /// <summary>
    /// MeridianGo 2020/06/17
    /// </summary>
    public interface IPackingListRepository : IRepository<PackingListInfo>
    {
        PackingListInfo Get(long id);

        int UpdateStatus(PackingListInfo entity, long modifierUserId);

        List<PackingListInfo> GetPage((string SearchType, string SearchKey, long WarehouseId, int IsDeleted, int PageSize, int CurrentPage) searchFilter, out int count);
    }
}
