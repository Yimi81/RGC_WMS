using RGC.WMS.USA.Domain.Entities.Product;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Product.Dto
{
    public class ProductListSynchronizeDto
    {
        public List<ProductInfo> ProductList { get; set; }
        public List<ProductCategory> Categories { get; set; }
    }
}
