using HuigeTec.Core.Domain.Entities;
using System;

namespace RGC.WMS.USA.Application.System.Dto
{
    public class SyncConfigDto : FullEntity
    {
        /// <summary>
        /// 同步数据表名
        /// </summary>
        public string SyncTable { get; set; }

        public string SyncTableDetail { get; set; }


        /// <summary>
        /// 同步目标系统
        /// </summary>
        public int SyncSystem { get; set; }

        public string SyncSystemString { get; set; }
        /// <summary>
        /// 同步时间
        /// </summary>
        public TimeSpan ExcuteTime { get; set; }

        public string ExcuteTimeString
        {
            get
            {
                return ExcuteTime.ToString();
            }
        }
        /// <summary>
        /// 同步时区
        /// </summary>
        public string ExcuteTimeZone { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public string PlanTime { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        public string creationTimeString
        {
            get
            {
                return CreationTime.ToString();
            }
        }
    }
}
