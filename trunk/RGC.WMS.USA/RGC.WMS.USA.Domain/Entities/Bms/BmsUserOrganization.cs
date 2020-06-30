using System;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    [Table("bms_user_organization")]
    public class BmsUserOrganization : CreationEntity
    {
        [Column("user_id")]
        public Int64 UserId { get; set; }
        [Column("organization_id")]
        public Int64 OrganizationId { get; set; }
        [Column("org_ids")]
        public string OrgIds { get; set; }

    }
}
