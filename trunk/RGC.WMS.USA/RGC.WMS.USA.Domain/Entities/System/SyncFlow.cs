using HuigeTec.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.System
{
    [Table("sync_flow")]
    public class SyncFlow:FullEntity
    {
        [Column("sync_type")]
        public int SyncType { get; set; }

        [Column("sync_status")]
        public int SyncStatus { get; set; }

        [Column("flow_id")]
        public long FlowId { get; set; }

        [Column("request_sync_time")]
        public DateTime? RequestSyncTime { get; set; }
    }
}
