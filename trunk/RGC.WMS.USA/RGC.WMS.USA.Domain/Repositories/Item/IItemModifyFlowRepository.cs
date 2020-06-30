using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Item;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Item
{
    public interface IItemModifyFlowRepository : IRepository<ItemModifyFlow>
    {
        int AddFlow(ItemModifyFlow entity);
        ItemModifyFlow GetFlow(long itemId,long flowId);
        List<ItemModifyFlow> GetFlowList(List<long> itemIds,List<long> flowIds);
        int UpdateFlow(ItemModifyFlow entity);
        int UpdateSyncStatus(long loginId, long flowId);
    }
}
