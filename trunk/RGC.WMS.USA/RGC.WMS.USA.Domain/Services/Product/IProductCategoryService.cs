using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product.Do;

namespace RGC.WMS.USA.Domain.Services.Product
{
    public interface IProductCategoryService : IDomainServiceBase
    {
        ResponseDo<string> Create(ProductCategoryCreateOrUpdateDo category, long loginId);

        ResponseDo<string> Delete(long loginId,  long id);

        ResponseDo<string> Update(ProductCategoryCreateOrUpdateDo category, long loginId);

    }
}
