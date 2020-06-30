using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsOrganizationCascaderDto
    {
        public BmsOrganizationCascaderDto()
        {
            Disabled = true;
        }

        public Int64 OrganizationId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool Disabled { get; set; }

        public List<BmsOrganizationCascaderDto> Children { get; set; }
    }
}
