using System.Collections.Generic;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 高级选项
    /// jerry 2020/6/4
    /// </summary>
    //[Table("ss_advanced_options")]
    public class SSAdvancedOptions
    {
        //[Column("warehouse_id")]
        public long? WarehouseId { get; set; }

        //[Column("non_machinable")]
        public bool NonMachinable { get; set; }

        //[Column("saturday_delivery")]
        public bool SaturdayDelivery { get; set; }

        //[Column("contains_alcohol")]
        public bool ContainsAlcohol { get; set; }

        //[Column("store_id")]
        public long? StoreId { get; set; }

        //[Column("custom_field1")]
        public string CustomField1 { get; set; }

        //[Column("custom_field2")]
        public string CustomField2 { get; set; }

        //[Column("custom_field3")]
        public string CustomField3 { get; set; }

        //[Column("source")]
        public string Source { get; set; }

        //[Column("merged_or_split")]
        public bool MergedOrSplit { get; set; }

        //[Column("merged_ids")]
        public List<long> MergedIds { get; set; }

        //[Column("parent_id")]
        public long? ParentId { get; set; }

        //[Column("bill_to_party")]
        public string BillToParty { get; set; }

        //[Column("bill_to_account")]
        public string BillToAccount { get; set; }

        //[Column("bill_to_postal_code")]
        public string BillToPostalCode { get; set; }

        //[Column("bill_to_country_code")]
        public string BillToCountryCode { get; set; }

        //[Column("bill_to_my_other_account")]
        public string BillToMyOtherAccount { get; set; }
    }
}
