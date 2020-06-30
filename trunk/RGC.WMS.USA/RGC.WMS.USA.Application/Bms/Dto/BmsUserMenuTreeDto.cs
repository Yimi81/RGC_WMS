using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsUserMenuTreeDto
    {
        public long UserId { get; set; }

        public BmsOrganizationDto Organization { get; set; }

        /// <summary>
        /// 菜单树
        /// </summary>
        public BmsMenuTreeDto MenuTree { get; set; }

        public BmsUserMenuTreeDto()
        {
            MenuTree = new BmsMenuTreeDto();
            MenuTree.Id = 0;
            MenuTree.Name = "全部页面";
        }
    }

    public class BmsUserMenuTreeModifyDto
    {
        public long UserId { get; set; }

        public long OrganizationId { get; set; }

        /// <summary>
        /// 菜单树
        /// </summary>
        public List<Int64> GrantedList { get; set; }

        public BmsUserMenuTreeModifyDto()
        {
            GrantedList = new List<long>();
        }


    }
}
