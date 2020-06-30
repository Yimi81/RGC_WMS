using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Order
{
    /// <summary>
    /// 地址
    /// jerry 2020/6/4
    /// </summary>
    //[Table("ss_address")]
    public class SSAddress
    {
        public SSAddress()
        {
            Country = "US";
            PostalCode = "";
        }
        /// <summary>
        /// 
        /// </summary>
        //[Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("company")]
        public string Company { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("street1")]
        public string Street1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("street2")]
        public string Street2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("street3")]
        public string Street3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("city")]
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("state")]
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("country")]
        public string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("residential")]
        public string Residential { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Column("address_verified")]
        public string AddressVerified { get; set; }
    }
}
