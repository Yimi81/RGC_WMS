using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Product
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        ProductCategory Get(long id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int Add(ProductCategory request);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="obj"></param>
        int SingleUpdate(ProductCategory request);

        /// <summary>
        /// 单个删除，伪删除
        /// </summary>
        /// <param name="id"></param>
        int Delete(long loginId, long id);
        /// <summary>
        /// 全部列表
        /// </summary>
        /// <returns></returns>
        List<ProductCategory> GetAllList();
        /// <summary>
        /// 全部查询
        /// </summary>
        /// <returns></returns>
        List<ProductCategoryTree> GetAllTree(long id, CategoryType type);
        /// <summary>
        /// 分类阶级 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ProductCategoryCascader> GetCategoryCascader(long id);

    }
}
