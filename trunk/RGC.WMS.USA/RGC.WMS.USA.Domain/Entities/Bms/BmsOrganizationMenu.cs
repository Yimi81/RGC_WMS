using HuigeTec.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    [Table("bms_organization_menu")]
    public class BmsOrganizationMenu : CreationEntity
    {
        [Column("organization_id")]
        public Int64 OrganizationId { get; set; }

        [Column("menu_id")]
        public Int64 MenuId { get; set; }

    }
}
