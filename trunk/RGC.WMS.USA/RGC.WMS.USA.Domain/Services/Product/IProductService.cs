using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;

namespace RGC.WMS.USA.Domain.Services.Product
{
    public interface IProductService : IDomainServiceBase
    {
        public ResponseDo<string> CreateProduct(long loginId, ProductEditDo dto);
        public ResponseDo<string> AddProductFlow(long loginId, ProductModifyFlow entity);

        public ResponseDo<string> UpdateProductFlow(long loginId, ProductModifyFlow entity);
        /// <summary>
        /// 删除产品
        /// shane 2020/2/11
        /// <param name="loginId"></param>
        /// <param name="loginIP"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDo<string> Delete(long loginId, long id);

        /// <summary>
        /// 恢复sku
        /// shane 2020/2/11
        /// <param name="loginId"></param>
        /// <param name="loginIP"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDo<string> Recovery(long loginId,  long id);

        /// <summary>
        /// 修改主表
        /// <param name="sku"></param>
        /// <returns></returns>
        public ResponseDo<string> UpdateProduct(long loginId,  ProductEditDo dto);

        /// <summary>
        /// 单独获取产品
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDo<ProductDo> Get(long id);

        /// <summary>
        /// 产品分类
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ResponseDo<string> AddCategory(long productId, long categoryId);

        /// <summary>
        /// 修改状态
        /// </summary>
        public ResponseDo<string> UpdateStatus(long id, ProductStatus status, long modifierUserId);

        ResponseDo<string> UpdateFlowStatus(long userId,long flowId);
    }
}
