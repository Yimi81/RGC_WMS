namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 海关
    /// 
    ///jerry 2020/6/4
    /// </summary>
    public class SSCustomsItem
    {
        public string CustomsItemId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public string HarmonizedTariffCode { get; set; }
        public string CountryOfOrigin { get; set; }
    }
}
