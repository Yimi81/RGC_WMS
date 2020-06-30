using System.Collections.Generic;

namespace RGC.Task.ShipStation.OrderGet.Dto
{
    /// <summary>
    /// shane 2020/4/16
    /// </summary>
    public class AdvancedOptions
    {
        public AdvancedOptions()
        {
            MergedIds = new List<long>();
        }
        public long? WarehouseId { get; set; }
        public bool NonMachinable { get; set; }
        public bool SaturdayDelivery { get; set; }
        public bool ContainsAlcohol { get; set; }
        public long? StoreId { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string Source { get; set; }
        public bool MergedOrSplit { get; set; }
        public List<long> MergedIds { get; set; }
        public long? ParentId { get; set; }
        public string BillToParty { get; set; }
        public string BillToAccount { get; set; }
        public string BillToPostalCode { get; set; }
        public string BillToCountryCode { get; set; }
        public string BillToMyOtherAccount { get; set; }
    }
}
