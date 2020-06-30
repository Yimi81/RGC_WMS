namespace RGC.WMS.USA.Application.Item.Dto
{
    public class ItemPriceApplyDto
    {
        public long ItemId { get; set; }
        public string WholeSalePriceString { get; set; }
        public string RetailPriceString { get; set; }
        public string ValidTimeString { get; set; }
        public int Level { get; set; }
    }
}
