using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 订单
    /// jerry 2020/6/4
    /// </summary>
    [Table("ss_order")]
    public class SSOrder : FullEntity
    {
        public SSOrder()
        {
            Items = new List<SSOrderItem>();
            BillTo = new SSAddress();
            ShipTo = new SSAddress();
            //ItemDict = new Dictionary<long, SSOrderItem>();
            //OrderDate = DateTime.Now.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("order_id")]
        public long OrderId { get; set; }

        [Column("order_number")]
        public string OrderNumber { get; set; }

        [Column("order_key")]
        public string OrderKey { get; set; }

        [Column("order_date")]
        public string OrderDate { get; set; }

        [NotMapped]
        public string OrderDateString => DateTime.Parse(OrderDate).ToString("yyyy-MM-dd HH:mm");

        [NotMapped]
        public string OrderDateString2 => DateTime.Parse(OrderDate).ToString("M/d/yyyy");

        [Column("create_date")]
        public string CreateDate { get; set; }

        [Column("modify_date")]
        public string ModifyDate { get; set; }

        [NotMapped]
        public string ModifyDateString => DateTime.Parse(ModifyDate).ToString("yyyy-MM-dd HH:mm");

        [Column("payment_date")]
        public string PaymentDate { get; set; }

        [NotMapped]
        public string PaymentDateString => DateTime.Parse(PaymentDate).ToString("M/d/yyyy");

        [Column("ship_by_date")]
        public string ShipByDate { get; set; }

        [Column("order_status")]
        public string OrderStatus { get; set; }

        [Column("customer_id")]
        public long? CustomerId { get; set; }

        [Column("customer_user_name")]
        public string CustomerUsername { get; set; }

        [Column("customer_email")]
        public string CustomerEmail { get; set; }

        [Column("bill_to_json")]
        public string BillToJSON { get; set; }

        [NotMapped]
        public SSAddress BillTo { get; set; }

        [Column("ship_to_json")]
        public string ShipToJSON { get; set; }

        [NotMapped]
        public SSAddress ShipTo { get; set; }

        [NotMapped]
        public List<SSOrderItem> Items { get; set; }

        //[NotMapped]
        //public Dictionary<long, SSOrderItem> ItemDict { get; set; }

        //订单所有金额相加
        [Column("order_total")]
        public decimal OrderTotal { get; set; }

        //产品总金额
        [NotMapped]
        public decimal SubTotal { get; set; }

        //实付金额
        [Column("amount_paid")]
        public decimal AmountPaid { get; set; }

        [Column("tax_amount")]
        public decimal TaxAmount { get; set; }

        [Column("shipping_amount")]
        public decimal ShippingAmount { get; set; }

        [Column("customer_notes")]
        public string CustomerNotes { get; set; }

        [Column("internal_notes")]
        public string InternalNotes { get; set; }

        [Column("gift")]
        public bool Gift { get; set; }

        [Column("gift_number")]
        public string GiftMessage { get; set; }

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

        [Column("confirmation")]
        public string Confirmation { get; set; }

        [Column("ship_date")]
        public string ShipDate { get; set; }

        [Column("hold_until_date")]
        public string HoldUntilDate { get; set; }

        [Column("weight_json")]
        public string WeightJSON { get; set; }

        [NotMapped]
        public SSWeight Weight { get; set; }


        [Column("dimensions_json")]
        public string DimensionsJSON { get; set; }

        [NotMapped]
        public SSDimensions Dimensions { get; set; }

        [Column("insurance_options_json")]
        public string InsuranceOptionsJSON { get; set; }

        [NotMapped]
        public SSInsuranceOptions InsuranceOptions { get; set; }

        [Column("international_options_json")]
        public string InternationalOptionsJSON { get; set; }

        [NotMapped]
        public SSInternationalOptions InternationalOptions { get; set; }

        [Column("advanced_options_json")]
        public string AdvancedOptionsJSON { get; set; }

        [NotMapped]
        public SSAdvancedOptions AdvancedOptions { get; set; }

        [Column("tag_ids_json")]
        public string TagIdsJSON { get; set; }

        [NotMapped]
        public List<decimal> TagIds { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("externally_fulfilled")]
        public bool ExternallyFulfilled { get; set; }

        [Column("externally_fulfilled_by")]
        public string ExternallyFulfilledBy { get; set; }
    }
}
