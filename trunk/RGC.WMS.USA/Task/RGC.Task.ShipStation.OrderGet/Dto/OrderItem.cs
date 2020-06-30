using System.Collections.Generic;

namespace RGC.Task.ShipStation.OrderGet.Dto
{
    /// <summary>
    /// shane 2020/4/16 17:22:53
    /// </summary>
    public class OrderItemDto
    {
        public OrderItemDto()
        {
            //CreateDate = DateTime.Now.ToString();
            //ModifyDate = DateTime.Now.ToString();
            //Weight = new Weight();
        }
        public long OrderItemId { get; set; }
        public string LineItemKey { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Weight Weight { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public string WarehouseLocation { get; set; }
        public List<ItemOption> Options { get; set; }
        public long? ProductId { get; set; }
        public string FulfillmentSku { get; set; }
        public bool Adjustment { get; set; }
        public string Upc { get; set; }
        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }
    }
}
