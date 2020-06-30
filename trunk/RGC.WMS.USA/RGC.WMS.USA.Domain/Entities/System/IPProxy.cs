using HuigeTec.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.System
{
    [Table("ip_proxy")]
    public class IPProxy : FullEntity
    {
        [Column("ip_address")]
        public string IpAddress { get; set; }
        [Column("port")]
        public int Port { get; set; }
        /// <summary>
        /// 0初始1国内2国外
        /// </summary>
        [Column("type")]
        public int Type { get; set; }
        [Column("account_id")]
        public string AccountId { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("remarks")]
        public string Remarks { get; set; }
        [Column("connection_count")]
        public int ConnectionCount { get; set; }
        [Column("connection_time")]
        public DateTime? ConnectionTime { get; set; }

        /// <summary>
        /// 0=可用,1=不可用,2=wayfair失效thd可用,3=thd失效wayfair可用
        /// </summary>
        [Column("response_type")]
        public int ResponseType { get; set; }

        [Column("response_remarks")]
        public string ResponseRemarks { get; set; }

        [Column("response_time")]
        public DateTime? ResponseTime { get; set; }
        [NotMapped]
        public string ConnectionTimeString => ConnectionTime.ToString();
        [NotMapped]
        public string ResponseTimeString => ResponseTime.ToString();
        [NotMapped]
        public string CreationTimeString => ConnectionTime.ToString();
        [NotMapped]
        public int FailNumber { get; set; }
    }
}
