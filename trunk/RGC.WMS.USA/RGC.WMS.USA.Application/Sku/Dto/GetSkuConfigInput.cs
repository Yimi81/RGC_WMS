using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class GetSkuConfigInput
    {
        public List<long> ComponentIdList { get; set; }
        public GetSkuConfigInput()
        {
            packageList = new List<SkuPackageDetail>();
            PartChildren = new List<SkuComponentTreeDto>();
            FittingChildren = new List<SkuComponentTreeDto>();
        }

        public long skuId { get; set; }
        public ConfigurationType type { get; set; }


        public List<SkuPackageDetail> packageList { get; set; }

        public List<SkuComponentTreeDto> PartChildren { get; set; }
        public List<SkuComponentTreeDto> FittingChildren { get; set; }
    }
}
