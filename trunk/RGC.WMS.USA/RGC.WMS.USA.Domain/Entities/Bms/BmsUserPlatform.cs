using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    [Table("bms_user_platform")]
    public class BmsUserPlatform : CreationEntity
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("platform_id")]
        public long PlatformId { get; set; }
    }
}
