using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class GetSkuConfigOutput
    {
        public GetSkuConfigOutput()
        {
            PartChildren = new List<SkuComponentTreeDto>();
            FittingChildren = new List<SkuComponentTreeDto>();
        }

        public List<SkuComponentTreeDto> PartChildren { get; set; }
        public List<SkuComponentTreeDto> FittingChildren { get; set; }
    }
}
