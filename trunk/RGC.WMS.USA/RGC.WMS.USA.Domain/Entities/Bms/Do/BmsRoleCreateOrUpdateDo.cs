using System;
using System.Collections.Generic;
using System.Text;

namespace RGC.WMS.USA.Domain.Entities.Bms.Do
{
    public class BmsRoleCreateOrUpdateDo
    {
        public long Id { get; set; }
        public bool IsStatic { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Desc { get; set; }
    }
}
