using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product;

namespace RGC.WMS.USA.Domain.Services.Product
{
    public interface IProductConfigService:IDomainServiceBase
    {
        ResponseDo<string> Create(ProductConfig config);
        ResponseDo<bool> IfExistConfig(ProductConfig config);
        ResponseDo<string> CreateDetail(ProductConfigDetail config);

        ResponseDo<string> Delete(long loginId,long id);
        ResponseDo<string> DeleteDetail(long loginId,long id, long proConfigId);

        ResponseDo<string> Update(ProductConfig config);
        ResponseDo<string> UpdateDetail(ProductConfigDetail config);

    }
}
