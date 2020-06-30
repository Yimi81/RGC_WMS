using HuigeTec.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.System
{
    [Table("sync_config")]
    public class SyncConfig : FullEntity
    {
        /// <summary>
        /// 同步数据表名
        /// </summary>
        [Column("sync_table")]
        public string SyncTable { get; set; }

        /// <summary>
        /// 同步目标系统
        /// </summary>
        [Column("sync_system")]
        public int SyncSystem { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        [Column("excute_time")]
        public TimeSpan ExcuteTime { get; set; }

        /// <summary>
        /// 同步时区
        /// </summary>
        [Column("excute_time_zone")]
        public string ExcuteTimeZone { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        [Column("plan_time")]
        public string PlanTime { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [Column("is_active")]
        public bool IsActive { get; set; }

    
    }
}
