using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Product;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Product
{
    public interface IProductModifyFlowRepository : IRepository<ProductModifyFlow>
    {
        /// <summary>
        /// 获取单个实例
        /// shane 2020/2/28
        /// </summary>
        /// <returns></returns>
        ProductModifyFlow GetFlow(long productId, long flowId);
        List<ProductModifyFlow> GetFlowList(List<long> productIds, List<long> flowIds);
        List<ProductModifyFlow> GetAllFlow();


        /// <summary>
        /// 增加修改同步流水
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ProductModifyFlow AddFlow(ProductModifyFlow model);

        ProductModifyFlow UpdateFlow(ProductModifyFlow model);
        int UpdateSyncStatus(long userId,long flowId);
    }
}
