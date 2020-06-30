using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    [Table("bms_user_system")]
    public class BmsUserSystem : CreationEntity
    {
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("system_id")]
        public long SystemId { get; set; }
    }
}
