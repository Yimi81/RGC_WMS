using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsOrganizationMenuTreeModifyDto
    {
        public long OrganizationId { get; set; }

        /// <summary>
        /// 菜单树
        /// </summary>
        public List<Int64> GrantedList { get; set; }

        public BmsOrganizationMenuTreeModifyDto()
        {
            GrantedList = new List<long>();
        }
    }
}
