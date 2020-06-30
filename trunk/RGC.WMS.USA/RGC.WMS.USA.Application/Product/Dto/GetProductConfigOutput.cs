using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Product.Dto
{
    public class GetProductConfigOutput
    {
        public GetProductConfigOutput()
        {
            PartChildren = new List<ProductComponentTreeDto>();
            FittingChildren = new List<ProductComponentTreeDto>();
        }



        public List<ProductComponentTreeDto> PartChildren { get; set; }
        public List<ProductComponentTreeDto> FittingChildren { get; set; }
    }
}
