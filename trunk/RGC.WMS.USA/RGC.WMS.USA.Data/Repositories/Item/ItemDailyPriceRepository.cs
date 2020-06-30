using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Item.Enum;
using RGC.WMS.USA.Domain.Repositories.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Item
{
    public class ItemDailyPriceRepository: RepositoryBase<ItemDailyPrice>, IItemDailyPriceRepository
    {
        public ItemDailyPriceRepository(DbContext context):base(context)
        {

        }

        public List<ItemDailyPrice> PageQuery(List<long> platformIds, DateTime date, List<DateTime> dateRange, string key, long itemId, List<DailyPriceSource> sources, bool isOnly, int pageSize, int currentPage, DateTime startTime, DateTime endTime, out int count)
        {
            List<ItemDailyPrice> result = new List<ItemDailyPrice>();
         
            int total = 0;
            var list = new List<ItemDailyPrice>();

            //IEnumerable<sku_daily_price> iPriceList;
            //iPriceList = SkuDailyPriceDict.Values.OrderByDescending(p=> p.platformId).ThenByDescending(p=>p.source).ThenByDescending(p => p.date);
            // var iPriceList = SkuDailyPrice.AsNoTracking().OrderByDescending(p => p.platformId).ThenByDescending(p => p.source).ThenByDescending(p => p.date).AsQueryable();
            var iPriceList = TableNoTracking.AsQueryable();
            //var iPriceList = from u in SkuDailyPrice
            //                 group u by new { u.platformId, u.skuId, u.price}
            //                 into mygroup
            //                 select mygroup.FirstOrDefault();

            if (itemId != 0)
            {
                iPriceList = iPriceList.Where(p => p.ItemId == itemId);
            }

            if (!string.IsNullOrEmpty(key))
            {
                iPriceList = iPriceList.Where(p => !string.IsNullOrEmpty(p.ItemName) && p.ItemName.ToLower() == key.ToLower() || !string.IsNullOrEmpty(p.FactoryModel) && p.FactoryModel.ToLower() == key.ToLower());
            }
            if (date != null && date != DateTime.MinValue)
            {
                iPriceList = iPriceList.Where(p => p.Date != null && p.Date.Day == date.Day);
            }
            if (sources.Any())
            {
                iPriceList = iPriceList.Where(p => sources.Contains(p.Source));

            }
            //if (startTime > DateTime.MinValue)
            //{
            //    iPriceList = iPriceList.Where(p => p.Date != null && p.Date >= startTime);
            //}
            //if (endTime > DateTime.MinValue)
            //{
            //    endTime = DateTime.Parse(endTime.ToString() + "23:59:59");
            //    iPriceList = iPriceList.Where(p => p.Date != null && p.Date <= endTime);
            //}
            if (dateRange[0] > DateTime.MinValue)
            {
                iPriceList = iPriceList.Where(p => p.Date != null && p.Date >= dateRange[0]);

            }
            if (dateRange[1] > DateTime.MinValue)
            {
                dateRange[1] = dateRange[1].AddDays(1);
                iPriceList = iPriceList.Where(p => p.Date != null && p.Date <= dateRange[1]);

            }
            if (platformIds.Any())
            {
                iPriceList = iPriceList.Where(p => p.PlatformId != 0 && platformIds.Contains(p.PlatformId));
            }
            if (isOnly)
            {
                //iPriceList = iPriceList.GroupBy(p=>new { p.skuId,p.platformId,p.price,p.date}).Select(p=>p.FirstOrDefault());
            }
            iPriceList = iPriceList.OrderByDescending(p => p.PlatformId).ThenByDescending(p => p.Source).ThenByDescending(p => p.Date);
            total = iPriceList.Count();
            list = iPriceList.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
           
            result= list;

            count = (int)total;
            //}
            return result;
        }
    }
}
