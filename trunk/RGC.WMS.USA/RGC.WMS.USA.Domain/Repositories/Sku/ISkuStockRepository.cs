using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Sku
{
    public interface ISkuStockRepository : IRepository<SkuStock>
    {
        void RefreshSkuStockDict();
        SkuStock Get(long id);
        SkuStock Add(SkuStock model);
        int Update(long loginId, SkuStock model);
        List<SkuStock> GetSkuStockList(long warehouseId, string key, int pageSize, int currentPage, out int count);
        List<SkuStock> GetAllList();
    }
}
