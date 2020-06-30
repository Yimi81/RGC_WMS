using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;

namespace RGC.WMS.USA.Application.Product
{
    public interface IProductConfigAppService:IAppService
    {
        ResponseDto<string> Create(ProductConfigEditDto config);
        ResponseDto<string> CreateDetail(ProductConfigDetail config);

        ResponseDto<string> Delete(long loginId,long id);
        ResponseDto<string> DeleteDetail(long loginId,long id, long configId);

        ResponseDto<string> Update(ProductConfigEditDto config);
        ResponseDto<string> UpdateDetail(ProductConfigDetail config);

        ResponseDto<ProductConfig> Get(long id);
        ResponsePageDto<ProductConfigDto> GetChild(long id, int pageSize, int currentPage);
        ResponsePageDto<ProductComponentTreeDto> GetChildList(ConfigurationType type);

        ResponsePageDto<ProductPartsDetailEx> GetFittingDetail(long id);
        ResponseDto<GetProductConfigOutput> GetChildEdit(GetProductConfigInput input);

        ResponsePageDto<ProductConfigTree> GetAllList(long id);

        ResponseDto<string> ForceRefreshSkuConfigDict();
    }
}
