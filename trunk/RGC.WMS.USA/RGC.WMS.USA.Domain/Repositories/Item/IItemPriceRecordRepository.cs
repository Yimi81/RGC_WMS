using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Item;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Item
{
    public interface IItemPriceRecordRepository:IRepository<ItemPriceRecord>
    {
      
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ItemPriceRecord Get(long id);

        List<ItemPriceRecord> GetAllList();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int Add(ItemPriceRecord request);

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        int UpdateVertifyStatus(long userId, long id, int status);

        /// <summary>
        /// 更新同步信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        int UpdateSyncInfo(long userId, long id, bool isSync,DateTime syncTime);

        int UpdateSyncStatus(long loginId, long flowId);

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="platformId"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        List<ItemPriceRecord> GetItemPriceRecordList(long platformId, string key, string itemId, int pageSize, int currentPage,out int totalCount);
    }
}
