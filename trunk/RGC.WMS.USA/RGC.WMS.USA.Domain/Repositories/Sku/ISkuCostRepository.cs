using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Sku
{
    public interface ISkuCostRepository : IRepository<SkuCost>
    {
        SkuCost Add(SkuCost model);
        int Update(SkuCost model, long adminId);
        SkuCost Get(long id);
        int Delete(long loginId, SkuCost model);
        List<SkuCost> GetSkuCostList(SearchFilterDo searchFilter, out int count);
        List<SkuCostBatch> GetSkuCostBatchList(string key, string batchNo, int pageSize, int currentPage, out int count);
        List<SkuCostBatch> GetSkuCostBatchList(long skuId, bool isRefreshDict, int pageSize, int currentPage, out int count);
        int AddBatch(SkuCostBatch request);
        int UpdateBatchStatus(long adminId, long costId, long batchId, int status);
        List<SkuCost> GetAllList();
    }
}
