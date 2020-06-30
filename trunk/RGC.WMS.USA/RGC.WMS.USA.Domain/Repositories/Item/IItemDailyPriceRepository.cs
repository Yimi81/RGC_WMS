using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Item.Enum;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Item
{
    public interface IItemDailyPriceRepository: IRepository<ItemDailyPrice>
    {
        public List<ItemDailyPrice> PageQuery(List<long> platformIds, DateTime date, List<DateTime> dateRange, string key, long itemId, List<DailyPriceSource> sources, bool isOnly, int pageSize, int currentPage, DateTime startTime, DateTime endTime,out int count);
    }
}
