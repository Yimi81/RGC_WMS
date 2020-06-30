using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 订单详情
    /// jerry 2020/6/19
    /// </summary>
    [Table("sale_order_item")]
    public class SaleOrderItem : FullEntity
    {
        public SaleOrderItem()
        {
            //CreateDate = DateTime.Now.ToString();
            //ModifyDate = DateTime.Now.ToString();
            //Weight = new Weight();
        }

        [Column("sale_order_id")]
        public long SaleOrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("order_item_id")]
        public long OrderItemId { get; set; }

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

        [Column("factory_model")]
        public string FactoryModel { get; set; }

        [Column("sku_stock_id")]
        public long SkuStockId { get; set; }

        [Column("batch_id")]
        public long BatchId { get; set; }

        [Column("batch_no")]
        public string BatchNo { get; set; }

        [Column("warehouse_id")]
        public long WarehouseId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("qty")]
        public int Qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("unit_price")]
        public int UnitPrice { get; set; }

        [NotMapped]
        public string UnitPriceString { get; set; }

        [NotMapped]
        public int ExtPrice { get; set; }

        [NotMapped]
        public string ExtPriceString { get; set; }

        [Column("tax_amount")]
        public int TaxAmount { get; set; }

        [NotMapped]
        public string TaxAmountString { get; set; }

        [Column("shipping_amount")]
        public int ShippingAmount { get; set; }

        [NotMapped]
        public string ShippingAmountString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("warehouse_location")]
        public string WarehouseLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("product_id")]
        public long? ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("upc")]
        public string Upc { get; set; }

    }
}
