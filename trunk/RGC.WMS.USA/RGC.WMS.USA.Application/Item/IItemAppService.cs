using RGC.WMS.USA.Application.Bidding.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Item.Dto;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Item.Enum;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Item
{
    public interface IItemAppService:IAppService
    {
        ResponseDto<string> CreateItem(long loginId, ItemEditDto dto);

        ResponseDto<string> Delete(long loginId, long id);

        ResponseDto<string> UpdateItem(long loginId, ItemEditDto dto);

        ResponseDto<string> SyncUpdateItem(long loginId, ItemEditDto dto);

        ResponseDto<string> UpdateItemStatus(long id, long modifierUserId);

        ResponseDto<ItemDto> Get(long id);

        ResponsePageDto<ItemDto> GetPlatformItemList(long platformId, string key, string uniqueId, int? status, int pageSize, int currentPage);

        ResponsePageDto<ItemPriceRecordDto> GetItemPriceRecordList(long platformId, string key, string itemId, int pageSize, int currentPage);

        ResponseDto<string> UpdateItemPlatformPrice(long adminId, long ItemId, string wholeSalePriceString, string retailPriceString, DateTime validTime,int level);

        ResponseDto<string> DeleteItem(long loginId, long id);
        ResponseDto<string> UpdatePlatformStatus(long loginId, long id, int status);

        ResponseDto<string> ForceRefreshItemDict();

        ResponsePageDto<ItemDto> GetItemList(string key, long platformId, int pageSize, int currentPage);

        ResponseDto<string> UpdateItemPriceStatus(long id, long modifierUserId, int status);
        
        #region 每日价格

        ResponsePageDto<SearchOutput> GetItemDailyPriceSearchList(string key);

        ResponsePageDto<ItemDailyPrice> GetItemDailyPriceList(List<long> platformIds, DateTime date, List<DateTime> dateRange, string key, long ItemId, List<DailyPriceSource> sources, bool isOnly, int pageSize, int currentPage, DateTime startTime, DateTime endTime);

        ResponsePageDto<ItemDailyReview> GetItemDailyReviewList(List<long> platformIds, DateTime date, List<DateTime> dateRange, string key, long ItemId, List<DailyPriceSource> sources, bool isOnly, int pageSize, int currentPage, DateTime startTime, DateTime endTime);

        ResponseDto<string> UpdateItemDailyPrice(ItemDailyPrice entity);

        ResponseDto<string> AddItemDailyPrice(ItemDailyPrice entity);

        ResponseDto<string> DeleteItemDailyPrice(long loginId, long id);
        #endregion


    }
}
