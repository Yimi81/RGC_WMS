using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Entities.Bms.Do
{
    public class BmsOrganizationCascaderDo
    {
        public BmsOrganizationCascaderDo()
        {
            Disabled = true;
            Children = new List<BmsOrganizationCascaderDo>();
        }

        public Int64 OrganizationId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool Disabled { get; set; }

        public List<BmsOrganizationCascaderDo> Children { get; set; }
    }
}
