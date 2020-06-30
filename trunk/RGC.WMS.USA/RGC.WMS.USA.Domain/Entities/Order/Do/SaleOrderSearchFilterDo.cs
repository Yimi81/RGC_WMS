using System;
using System.Collections.Generic;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.Domain.Entities
{
    public class SaleOrderSearchFilterDo : SearchFilterDo
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<int> OrderStatusList { get; set; }
        public SaleOrderSearchFilterDo()
        {
            OrderStatusList = new List<int>();
        }
    }
}
