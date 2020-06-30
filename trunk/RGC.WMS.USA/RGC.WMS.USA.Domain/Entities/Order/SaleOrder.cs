using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 销售订单
    /// jerry 2020/6/19
    /// </summary>
    [Table("sale_order")]
    public class SaleOrder : FullEntity
    {
        public SaleOrder()
        {
            Items = new List<SaleOrderItem>();
            ItemDict = new Dictionary<long, SaleOrderItem>();
        }

        [Column("order_number")]
        public string OrderNumber { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [NotMapped]
        public string OrderDateString { get; set; }

        [Column("payment_date")]
        public DateTime PaymentDate { get; set; }

        [NotMapped]
        public string PaymentDateString { get; set; }

        [Column("ship_by_date")]
        public DateTime? ShipByDate { get; set; }

        [Column("order_status")]
        public int OrderStatus { get; set; }

        [Column("order_source")]
        public int OrderSource { get; set; }

        [Column("customer_id")]
        public long? CustomerId { get; set; }

        [Column("customer_user_name")]
        public string CustomerUsername { get; set; }

        [Column("customer_email")]
        public string CustomerEmail { get; set; }

        [Column("ship_to_name")]
        public string ShipToName { get; set; }

        [Column("ship_to_company")]
        public string ShipToCompany { get; set; }

        [Column("ship_to_address1")]
        public string ShipToAddress1 { get; set; }

        [Column("ship_to_address2")]
        public string ShipToAddress2 { get; set; }

        [Column("ship_to_city")]
        public string ShipToCity { get; set; }

        [Column("ship_to_state")]
        public string ShipToState { get; set; }

        [Column("ship_to_country")]
        public string ShipToCountry { get; set; }

        [Column("ship_to_phone")]
        public string ShipToPhone { get; set; }

        [Column("ship_to_zipcode")]
        public string ShipToZipcode { get; set; }

        [Column("ship_to_email")]
        public string ShipToEmail { get; set; }

        [Column("bill_to_name")]
        public string BillToName { get; set; }

        [Column("bill_to_company")]
        public string BillToCompany { get; set; }

        [Column("bill_to_address1")]
        public string BillToAddress1 { get; set; }

        [Column("bill_to_address2")]
        public string BillToAddress2 { get; set; }

        [Column("bill_to_city")]
        public string BillToCity { get; set; }

        [Column("bill_to_state")]
        public string BillToState { get; set; }

        [Column("bill_to_country")]
        public string BillToCountry { get; set; }

        [Column("bill_to_phone")]
        public string BillToPhone { get; set; }

        [Column("bill_to_zipcode")]
        public string BillToZipcode { get; set; }

        [Column("bill_to_email")]
        public string BillToEmail { get; set; }

        [NotMapped]
        public List<SaleOrderItem> Items { get; set; }

        [JsonIgnoreAttribute]
        [NotMapped]
        public Dictionary<long, SaleOrderItem> ItemDict { get; set; }

        [Column("order_total")]
        public int OrderTotal { get; set; }

        [NotMapped]
        public string OrderTotalString{ get; set; }

        [Column("amount_paid")]
        public int AmountPaid { get; set; }

        [NotMapped]
        public string AmountPaidString { get; set; }

        [Column("tax_amount")]
        public int TaxAmount { get; set; }

        [NotMapped]
        public string TaxAmountString { get; set; }

        [Column("shipping_amount")]
        public int ShippingAmount { get; set; }

        [NotMapped]
        public string ShippingAmountString { get; set; }

        [Column("customer_notes")]
        public string CustomerNotes { get; set; }

        [Column("payment_method")]
        public string PaymentMethod { get; set; }

        [Column("requested_shipping_service")]
        public string RequestedShippingService { get; set; }

        [Column("carrier_code")]
        public string CarrierCode { get; set; }

        [Column("service_code")]
        public string ServiceCode { get; set; }

        [Column("package_code")]
        public string PackageCode { get; set; }

        /// <summary>
        /// 出库状态：0未出库 1已出库
        /// </summary>
        [Column("stock_status")]
        public int StockStatus { get; set; }

        /// <summary>
        /// 打印状态：0未打印 1打印成功 2打印失败 
        /// </summary>
        [Column("print_status")]
        public int PrintStatus { get; set; }

    }
}
