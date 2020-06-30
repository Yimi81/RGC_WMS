using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Sku
{
    public interface ISkuRepository : IRepository<SkuInfo>
    {
        SkuInfo Get(long id);
        List<SkuInfo> PageQuery(SearchFilterDo searchFilter, out int count);
        List<SkuInfo> RecycleQuery(SearchFilterDo searchFilter, out int count);
        List<SkuInfo> GetCategorySkuList(SearchFilterDo searchFilter, out int count);
        List<SkuInfo> GetOtherCategorySkuList(SearchFilterDo searchFilter, out int count);
        List<SkuInfo> GetSkuInfoDict();
    }
}
