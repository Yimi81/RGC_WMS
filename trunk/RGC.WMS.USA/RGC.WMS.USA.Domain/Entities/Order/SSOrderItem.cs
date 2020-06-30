using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 订单详情
    /// jerry 2020/3/4
    /// </summary>
    [Table("ss_order_item")]
    public class SSOrderItem : FullEntity
    {
        public SSOrderItem()
        {
            //CreateDate = DateTime.Now.ToString();
            //ModifyDate = DateTime.Now.ToString();
            //Weight = new Weight();
        }

        [Column("ss_order_id")]

        public long SSOrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("order_item_id")]
    
        public long OrderItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("line_item_key")]
        public string LineItemKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("weight_json")]
        public string WeightJSON { get; set; }

        [NotMapped]
        public SSWeight Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("unit_price")]
        public float UnitPrice { get; set; }

        [NotMapped]
        public float ExtPrice => UnitPrice * Quantity;
        /// <summary>
        /// 
        /// </summary>
        [Column("tax_amount")]
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("shipping_amount")]
        public decimal ShippingAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("warehouse_location")]
        public string WarehouseLocation { get; set; }

        [Column("options_json")]
        public string OptionsJSON { get; set; }

        [NotMapped]
        public List<SSItemOption> Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("product_id")]
        public long? ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("fulfillment_sku")]
        public string FulfillmentSku { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("adjustment")]
        public bool Adjustment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("upc")]
        public string Upc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("create_date")]
        public string CreateDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("modify_date")]
        public string ModifyDate { get; set; }

    }
}
