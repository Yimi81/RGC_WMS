using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 国际
    /// jerry 2020/6/4
    /// </summary>
    public class SSInternationalOptions
    {
        public string Contents { get; set; }

        public List<SSCustomsItem> CustomsItems { get; set; }
        public string NonDelivery { get; set; }
    }
}
