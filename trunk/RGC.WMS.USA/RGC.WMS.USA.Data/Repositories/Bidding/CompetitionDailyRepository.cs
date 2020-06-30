using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Entities.Item.Enum;
using RGC.WMS.USA.Domain.Repositories.Bidding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Bidding
{
    public class CompetitionDailyRepository : RepositoryBase<CompetitionDaily>, ICompetitionDailyRepository
    {
        public CompetitionDailyRepository(DbContext context):base(context)
        {

        }

        public List<CompetitionDaily> PageQuery(List<long> platformIds, DateTime date, List<DateTime> dateRange, string key, long itemId, List<DailyPriceSource> sources, bool isOnly, int pageSize, int currentPage, DateTime startTime, DateTime endTime, out int count)
        {
            List<CompetitionDaily> result = new List<CompetitionDaily>();
         
            int total = 0;
            var list = new List<CompetitionDaily>();

            var iPriceList = TableNoTracking.AsQueryable();

            if (itemId != 0)
            {
                iPriceList = iPriceList.Where(p => p.ItemId == itemId);
            }

            if (!string.IsNullOrEmpty(key))
            {
                iPriceList = iPriceList.Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.ToLower() == key.ToLower() || !string.IsNullOrEmpty(p.FactoryModel) && p.FactoryModel.ToLower() == key.ToLower());
            }
            if (date != null && date != DateTime.MinValue)
            {
                iPriceList = iPriceList.Where(p => p.Date != null && p.Date.Day == date.Day);
            }
            if (sources.Any())
            {
                iPriceList = iPriceList.Where(p => sources.Contains(p.Source));

            }
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
