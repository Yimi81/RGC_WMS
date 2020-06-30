using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Domain.Entities.Sku;

namespace RGC.WMS.USA.Application.Sku
{
    public interface ISkuAppService : IAppService
    {
        //ResponseDto<string> CreateSku(long loginId, SkuInfoEditDto entity);

        //ResponseDto<string> Delete(long loginId, long id);

        //ResponseDto<string> Recovery(long loginId, long id);

        //ResponseDto<string> ProductCreateSku(long loginId, long productId);

        //ResponseDto<string> UpdateSku(long loginId, SkuInfoEditDto entity);

        ResponseDto<SkuInfoDto> GetDetailAndParts(long skuId);

        ResponseDto<SkuInfoDto> Get(long id);

        ResponsePageDto<SkuSearchOutput> GetPage(string key, long productId, long categoryId, int pageSize, int currentPage);

        ResponsePageDto<SkuInfoDto> RecycleQuery(string key, long categoryId, int pageSize, int currentPage);

        //ResponseDto<string> AddCategory(long skuId, long categoryId);

        ResponsePageDto<SkuInfoDto> GetCategorySkuList(long categoryId, string key, int pageSize, int currentPage);

        ResponsePageDto<SkuInfoDto> GetOtherCategorySkuList(long categoryId, string key, int pageSize, int currentPage);

        ResponsePageDto<SkuCostBatch> GetSkuCostBatchList(long skuId, bool isRefreshDict, int pageSize, int currentPage);

        ResponsePageDto<SkuStockOutput> GetSkuStockList(long loginId, long warehouseId, string key, int pageSize, int currentPage);

        ResponseDto<SkuStockOutput> GetSkuStock(long loginId, long skuStockId);

        ResponseDto<string> AddSkuStock(long loginId, SkuStock request);

        ResponseDto<string> UpdateSkuStock(long loginId, SkuStock request);

        ResponseDto<string> DeleteSkuStock(long loginId, long id);

        ResponsePageDto<SkuCost> GetSkuCostList(long loginId, string key, int pageSize, int currentPage);

        ResponseDto<string> CreateSkuCost(long loginId, SkuCost dto);

        ResponseDto<string> CreateSkuCostDirect(long loginId, SkuCost dto);

        ResponseDto<SkuCost> GetSkuCostDetail(long loginId, long skuCostId);

        ResponseDto<string> UpdateSkuCost(long loginId, SkuCost dto);

        ResponseDto<string> DeleteSkuCost(long loginId, long SkuCostId);

        ResponseDto<string> AddSkuCostBatch(long loginId, SkuCostBatch dto);

        ResponseDto<string> UpdateSkuCostBatchStatus(long loginId, long costId, long batchId, int status);

        ResponsePageDto<SkuCostBatch> GetSkuCostBatchList(long loginId, string key, string batchNo, int pageSize, int currentPage);

        ResponsePageDto<SkuCostBatchFilterOutput> GetSkuCostBatchList(long loginId, string searchKey, int pageSize, int currentPage);

    }
}
