namespace RGC.Task.ShipStation.OrderGet.Dto
{
    /// <summary>
    /// shane 2020/4/16
    /// </summary>
    public class CustomsItem
    {
        public string CustomsItemId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public string HarmonizedTariffCode { get; set; }
        public string CountryOfOrigin { get; set; }
    }
}
