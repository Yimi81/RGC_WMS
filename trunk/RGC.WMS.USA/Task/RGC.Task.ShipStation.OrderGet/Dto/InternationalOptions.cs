using System.Collections.Generic;

namespace RGC.Task.ShipStation.OrderGet.Dto
{
    /// <summary>
    /// shane 2020/4/16
    /// </summary>
    public class InternationalOptions
    {
        public string Contents { get; set; }
        public List<CustomsItem> CustomsItems { get; set; }
        public string NonDelivery { get; set; }
    }
}
