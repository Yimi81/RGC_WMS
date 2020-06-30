using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Product
{
    public interface IProductConfigRepository: IRepository<ProductConfig>
    {
        /// <summary>
        /// 单个获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductConfig Get(long id);

        /// <summary>
        /// 单个获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ProductConfigDetail> GetDetailList(long proConfigId);



        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int Add(ProductConfig request);

        /// <summary>
        /// 新增细节
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int AddDetail(ProductConfigDetail request);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="obj"></param>
        int SingleUpdate(ProductConfig request);

        /// <summary>
        /// 同步更新字段
        /// shane 2019/11/5
        /// </summary>
        /// <param name="obj"></param>
        int SyncUpdateDetail(ProductConfigDetail request);

        /// <summary>
        /// 更新细节
        /// </summary>
        /// <param name="obj"></param>
        int UpdateDetail(ProductConfigDetail request);

        /// <summary>
        /// 单个删除，伪删除
        /// </summary>
        /// <param name="id"></param>
        int Delete(long loginId,long id);

        /// <summary>
        /// 单个删除，伪删除
        /// </summary>
        /// <param name="id"></param>
        int DeleteDetail(long loginId,long id, long proConfigId);

        /// <summary>
        /// 全部查询
        /// </summary>
        /// <returns></returns>
        List<ProductConfigTree> GetAllList(long id);

        List<ProductPartsDetailEx> GetAllDetail(long id);
        /// <summary>
        /// 全部查询
        /// </summary>
        /// <returns></returns>
        List<ProductComponentTreeDo> GetChildList(ConfigurationType type);

        List<ProductConfig> GetChild(long id, int pageSize, int currentPage, out int count);
        bool IfExist(ProductConfig request);
    }
}
