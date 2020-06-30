using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Item;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Item
{
    public interface IItemRepository : IRepository<ItemInfo>
    {
        ItemInfo Get(long Id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int Add(ItemInfo entity, List<ItemProduct> productList);
       // int AddFlow(ItemModifyFlow entity);
       // ItemModifyFlow GetFlow(long itemId,long flowId);
        //List<ItemModifyFlow> GetFlowList(List<long> flowIds);
        //int UpdateFlow(ItemModifyFlow entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        int SingleUpdate(ItemInfo entity, List<ItemProduct> productList);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="status"></param>
        /// <param name="modifierUserId"></param>
        /// <param name="modifierUserIP"></param>
        /// <returns></returns>
        int UpdateStatus(long Id, long modifierUserId);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="loginIP"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        int Delete(long loginId, long Id);


        /// <summary>
        /// 更新
        /// 加入了写进价格记录表的逻辑
        /// </summary>
        /// <param name="obj"></param>
        int UpdateAndAddRecord(long adminId, long ItemId, int wholeSalePrice, int retailPrice);

        /// <summary>
        /// 价格审核通过并更新item表
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="ItemId"></param>
        /// <param name="priceId"></param>
        /// <param name="retailPrice"></param>
        /// <param name="validTime"></param>
        /// <returns></returns>
        int ApplyPriceAndUpdate(long priceId,long modifierUserId);

        /// <summary>
        /// 某平台下在售产品列表
        /// </summary>
        /// <param name="PlatformId"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        List<ItemInfo> GetPlatformItemList(long PlatformId, string key, string uniqueId, int? status, int pageSize, int currentPage,out int totalCount);

        List<ItemInfo> ItemPageQuery(string key, long platformId, int pageSize, int currentPage, out int totalCount);

        List<(string value, long id)> SearchPageQuery(string key);

        void ForceRefreshItemDict();
    }
}
