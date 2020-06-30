using RGC.WMS.USA.Domain.Entities.Item.Enum;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Entities.Item.Do
{
    public class ItemSearchDo
    {
        public ItemSearchDo()
        {
            key = "";
            isOnly = false;
            pageSize = 10;
            currentPage = 1;
            itemId = 0;
            sources = new List<DailyPriceSource>();
            platformIds = new List<long>();
            date = DateTime.MinValue;
            startTime = DateTime.MinValue;
            endTime = DateTime.MinValue;
            dateRange = new List<DateTime>();
            status = -1;
        }
        public List<long> platformIds { get; set; }
    
        public DateTime date { get; set; }

        public List<DateTime> dateRange { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public string key { get; set; }

        public long itemId { get; set; }

        public int status { get; set; }

        public List<DailyPriceSource> sources { get; set; }

        public int pageSize { get; set; }

        public int currentPage { get; set; }

        /// <summary>
        /// 是否过滤重复价格
        /// </summary>
        public bool isOnly { get; set; }
    }
}
