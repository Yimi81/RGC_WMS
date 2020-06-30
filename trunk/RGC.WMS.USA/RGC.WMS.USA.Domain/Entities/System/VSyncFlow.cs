using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.System
{

    [Table("v_sync_flow")]
    public class VSyncFlow
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("sync_table")]
        public string SyncTable { get; set; }

        [Column("sync_status")]
        public int SyncStatus { get; set; }

        [Column("flow_id")]
        public long FlowId { get; set; }

        [Column("source_id")]
        public long SourceId { get; set; }

        [Column("request_sync_time")]
        public DateTime? RequestSyncTime { get; set; }

        [Column("creation_time")]
        public DateTime? CreationTime { get; set; }

        [Column("creator_user_id")]
        public long CreatorUserId { get; set; }
        [NotMapped]
        public string RequestSyncTimeString
        {
            get
            {
                return RequestSyncTime.ToString();
            }
        }
        [NotMapped]
        public string CreationTimeString
        {
            get
            {
                return CreationTime.ToString();
            }
        }
    }
}
