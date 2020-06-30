using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Product
{
    public interface IProductRepository : IRepository<ProductInfo>
    {
        void ForceRefreshProductDict();
        List<ProductInfo> GetList();

        /// 获取单个实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductInfo Get(long id);

        ProductInfo Add(ProductEditDo entity);

        int SingleUpdate(ProductEditDo entity);

        int Delete(long loginId,long id);

        int Recovery(long loginId, long id);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        List<ProductInfo> PageQuery(SearchFilterDo searchFilter, out int count);

        List<ProductInfo> RecycleQuery(SearchFilterDo searchFilter, out int count);
        List<ProductInfo> GetProductList(List<long> productIds);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="modifierUserId"></param>
        /// <param name="modifierUserIP"></param>
        /// <returns></returns>
        int UpdateStatus(long id, ProductStatus status, long modifierUserId);


        #region 产品分类维护

        /// <summary>
        /// 产品分类
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int AddCategory(long productId, long categoryId);

        /// <summary>
        /// 某分类下product列表
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public List<ProductInfo> GetCategoryProductList(SearchFilterDo searchFilter, out int count);

        /// <summary>
        /// 非该分类下product列表
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public List<ProductInfo> GetOtherCategoryProductList(SearchFilterDo searchFilter, out int count);

        #endregion

        bool IfExistProduct(string factoryModel);
    }
}
