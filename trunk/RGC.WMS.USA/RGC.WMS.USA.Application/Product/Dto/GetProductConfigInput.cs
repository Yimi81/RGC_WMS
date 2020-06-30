using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Product.Dto
{
    public class GetProductConfigInput
    {
        public List<long> ComponentIdList { get; set; }
        public GetProductConfigInput()
        {
            PackageList = new List<ProductPackageDetail>();
            PartChildren = new List<ProductComponentTreeDto>();
            FittingChildren = new List<ProductComponentTreeDto>();
        }

        public long skuId { get; set; }
        public ConfigurationType Type { get; set; }


        public List<ProductPackageDetail> PackageList { get; set; }

        public List<ProductComponentTreeDto> PartChildren { get; set; }
        public List<ProductComponentTreeDto> FittingChildren { get; set; }
    }
}
