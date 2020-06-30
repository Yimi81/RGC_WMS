using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 订单详情dto
    /// jerry 2020/6/28
    /// </summary>
    public class SaleOrderItemDto
    {
        public SaleOrderItemDto()
        {
            SaleOrderId = 0;
            OrderItemId = 0;
            Qty = 0;
            UnitPrice = 0;
            TaxAmount = 0;
            ShippingAmount = 0;
            ExtPrice = 0;
        }

        public long SaleOrderId { get; set; }

        public long OrderItemId { get; set; }

        public long SkuId { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string FactoryModel { get; set; }

        public long SkuStockId { get; set; }

        public long BatchId { get; set; }

        public string BatchNo { get; set; }

        public long WarehouseId { get; set; }

        public string ImageUrl { get; set; }

        public int Qty { get; set; }

        public int UnitPrice { get; set; }

        public string UnitPriceString { get; set; }

        public int ExtPrice { get; set; }

        public string ExtPriceString { get; set; }

        public int TaxAmount { get; set; }

        public string TaxAmountString { get; set; }

        public int ShippingAmount { get; set; }

        public string ShippingAmountString { get; set; }

        public string WarehouseLocation { get; set; }

        public long? ProductId { get; set; }

        public string Upc { get; set; }

    }
}
