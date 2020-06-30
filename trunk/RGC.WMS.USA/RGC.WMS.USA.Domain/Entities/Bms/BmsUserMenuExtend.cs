using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    [Table("bms_user_menu")]
    public class BmsUserMenuExtend: HuigeTec.Core.Domain.Entities.BmsUserMenu
    {
        [Column("organization_id")]
        public long OrganizationId { get; set; }
    }
}
