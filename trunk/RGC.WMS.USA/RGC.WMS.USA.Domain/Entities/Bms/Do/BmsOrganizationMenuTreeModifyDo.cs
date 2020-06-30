using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Entities.Bms.Do
{
    public class BmsOrganizationMenuTreeModifyDo
    {
        public long OrganizationId { get; set; }

        /// <summary>
        /// 菜单树
        /// </summary>
        public List<Int64> GrantedList { get; set; }

        public BmsOrganizationMenuTreeModifyDo()
        {
            GrantedList = new List<long>();
        }
    }
}
