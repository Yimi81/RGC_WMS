using System.Collections.Generic;

namespace RGC.Task.ShipStation.OrderGet.Dto
{
    public class OrderListResponse
    {
        public List<OrderDto> Orders { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
    }
}
