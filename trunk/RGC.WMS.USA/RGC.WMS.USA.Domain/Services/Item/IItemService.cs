using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Item.Do;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Services.Item
{
    public interface IItemService:IDomainServiceBase
    {
        ResponseDo<string> CreateItem(ItemEditDo entity);
        ResponseDo<string> AddItemFlow(ItemModifyFlow entity);
        ResponseDo<string> Delete(long loginId, long id);

        ResponseDo<string> UpdateItem(ItemEditDo obj);

        ResponseDo<string> UpdateStatus(long id, long modifierUserId);

        ResponseDo<string> UpdateAndAddRecord(long adminId, long ItemId, string wholeSalePriceString, string retailPriceString);
        /// <summary>
        /// 更新审核状态
        /// </summary>
        ResponseDo<string> UpdateItemPriceVertifyStatus(long id, long modifierUserId, int status);
        ResponseDo<string> PriceApplyAndAddRecord(long adminId, long ItemId, string retailPriceString, DateTime validTime,int level);
        ResponseDo<string> UpdatePlatformStatus(long loginId, long id, int status);
        ResponsePageDo<(string value, long id)> GetItemSearchList(string key);

    }
}
