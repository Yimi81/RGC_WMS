using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.System
{
    [Table("platform_info")]
    public class PlatformInfo: FullEntity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Column("e_name")]
        public string EName { get; set; }

        [Column("c_name")]
        public string CName { get; set; }
    }
}
