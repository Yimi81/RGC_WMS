using System;
using System.Collections.Generic;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.Domain.Entities
{
    public class SSOrderSearchFilterDo : SearchFilterDo
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<string> OrderStatusList { get; set; }
        public SSOrderSearchFilterDo()
        {
            OrderStatusList = new List<string>();
        }
    }
}
