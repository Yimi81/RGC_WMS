using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.System
{
    [Table("system_info")]
    public class SystemInfo : FullEntity
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; }
        
        /// <summary>
        /// 域名
        /// </summary>
        [Column("domain_name")]
        public string DomainName { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        [Column("ip_address")]
        public string IPAddress { get; set; }

        /// <summary>
        /// 是否常量，常量不能修改
        /// </summary>
        [Column("is_static")]
        public bool isStatic { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("desc")]
        public string Desc { get; set; }
    }
}
