using RGC.WMS.USA.Domain.Entities.Product.Enum;

namespace RGC.WMS.USA.Application.Product.Dto
{
    public class ProductStatusSynchronizeDto
    {
        public long ProductId { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
