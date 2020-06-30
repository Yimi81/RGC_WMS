using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Product
{
    public interface IProductAppService : IAppService
    {
        ResponseDto<string> CreateProduct(long loginId,ProductEditDto entity);

        ResponseDto<string> Delete(long loginId, long id);

        ResponseDto<string> Recovery(long loginId, long id);

        ResponseDto<string> UpdateProduct(long loginId, ProductEditDto entity);

        ResponseDto<ProductEditDto> Get(long id);

        ResponsePageDto<ProductListDto> GetPage(string key, long categoryId, int pageSize, int currentPage);

        ResponsePageDto<ProductListDto> RecycleQuery(string key, long categoryId, int pageSize, int currentPage);

        ResponseDto<string> AddCategory(long productId, long categoryId);

        ResponsePageDto<ProductListDto> GetCategoryProductList(long categoryId, string key, int pageSize, int currentPage);

        ResponsePageDto<ProductListDto> GetOtherCategoryProductList(long categoryId, string key, int pageSize, int currentPage);

        ResponseDto<ProductListSynchronizeDto> GetProductList(List<long> productIds);

        ResponseDto<string> UpdateStatus(long id, int status, long modifierUserId, bool isSync);

        ResponseDto<string> ForceRefreshProductDict();
    }
}
