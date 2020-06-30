namespace RGC.Task.ShipStation.OrderGet.Dto
{
    /// <summary>
    /// jerry 2020/6/2
    /// </summary>
    public class OrderListRequestDto
    {
        public string customerName { get; set; }
        public string itemKeyword { get; set; }
        public string createDateStart { get; set; }
        public string createDateEnd { get; set; }
        public string modifyDateStart { get; set; }
        public string modifyDateEnd { get; set; }
        public string orderDateStart { get; set; }
        public string orderDateEnd { get; set; }
        public string orderNumber { get; set; }
        public string orderStatus { get; set; }
        public string paymentDateStart { get; set; }
        public string paymentDateEnd { get; set; }
        public string storeId { get; set; }
 
        public string sortBy { get; set; }
        public string sortDir { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
      
    }
}
