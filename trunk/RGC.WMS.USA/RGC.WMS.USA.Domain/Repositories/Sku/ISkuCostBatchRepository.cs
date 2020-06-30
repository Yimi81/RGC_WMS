using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Sku
{
    public interface ISkuCostBatchRepository : IRepository<SkuCostBatch>
    {
        List<SkuCostBatch> GetSkuCostBatchList(string searchKey, int pageSize, int currentPage, out int count);
        List<SkuCostBatch> GetSkuCostBatchDict();
    }
}
