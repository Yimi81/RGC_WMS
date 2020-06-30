using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.Application.Order.Dto
{
    /// <summary>
    /// 销售订单
    /// jerry 2020/6/19
    /// </summary>

    public class SaleOrderDto
    {
        public SaleOrderDto()
        {
            Id = 0;
            OrderStatus = 0;
            OrderSource = 0;
            CustomerId = 0;
            OrderTotal = 0;
            AmountPaid = 0;
            TaxAmount = 0;
            ShippingAmount = 0;
        }
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateString { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentDateString { get; set; }
        public DateTime? ShipByDate { get; set; }
        public int OrderStatus { get; set; }
        public int OrderSource { get; set; }
        public long CustomerId { get; set; }
        public string CustomerUsername { get; set; }
        public string CustomerEmail { get; set; }
        public string ShipToName { get; set; }
        public string ShipToCompany { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToPhone { get; set; }
        public string ShipToZipcode { get; set; }
        public string ShipToEmail { get; set; }
        public bool IsAddressSame { get; set; }
        public string BillToName { get; set; }
        public string BillToCompany { get; set; }
        public string BillToAddress1 { get; set; }
        public string BillToAddress2 { get; set; }
        public string BillToCity { get; set; }
        public string BillToState { get; set; }
        public string BillToCountry { get; set; }
        public string BillToPhone { get; set; }
        public string BillToZipcode { get; set; }
        public string BillToEmail { get; set; }
        public List<SaleOrderItemDto> Items { get; set; }
        public int OrderTotal { get; set; }
        public string OrderTotalString { get; set; }
        public int AmountPaid { get; set; }
        public string AmountPaidString { get; set; }
        public int TaxAmount { get; set; }
        public string TaxAmountString { get; set; }
        public int ShippingAmount { get; set; }
        public string ShippingAmountString { get; set; }
        public string CustomerNotes { get; set; }
        public string PaymentMethod { get; set; }
        public string RequestedShippingService { get; set; }
        public string CarrierCode { get; set; }
        public string ServiceCode { get; set; }
        public string PackageCode { get; set; }
        public int StockStatus { get; set; }
        public int PrintStatus { get; set; }
    }
}
