using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;

namespace RGC.WMS.USA.Application.Product
{
    public interface IProductCategoryAppService : IAppService
    {

        ResponseDto<string> Create(ProductCategoryEditDto category, long loginId);

        ResponseDto<string> Delete(long loginId,  long id);

        ResponseDto<string> Update(ProductCategoryEditDto category, long loginId);

        ResponseDto<ProductCategoryDto> Get(long id);

        ResponsePageDto<ProductCategoryTree> GetAllList(CategoryType type);

        ResponsePageDto<ProductCategoryCascader> GetCategoryCascader(long id);

        ResponseDto<string> ForceRefreshCategoryDict();

        ResponseDto<string> SyncProductCategory();
    }
}
