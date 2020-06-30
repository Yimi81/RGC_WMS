using HuigeTec.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities
{
    public class WorkFlowEntity : FullEntity
    {
        /// <summary>
        /// Desc:审核状态
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("auditin_status")]
        public int? AuditinStatus { get; set; }

        /// <summary>
        /// Desc:审核人
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("auditin_user_id")]
        public long? AuditinUserId { get; set; }

        /// <summary>
        /// Desc:审核时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("auditin_time")]
        public DateTime? AuditinTime { get; set; }

        /// <summary>
        /// Desc: 审核备注
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("auditin_remark")]
        public string AuditinRemark { get; set; }
    }
}
