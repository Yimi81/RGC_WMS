using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Entities.Bms.Do
{
    public class BmsUserMenuTreeDo
    {
        public long UserId { get; set; }

        public BmsOrganization Organization { get; set; }

        /// <summary>
        /// 菜单树
        /// </summary>
        public BmsMenuTreeDo MenuTree { get; set; }

        public BmsUserMenuTreeDo()
        {
            MenuTree = new BmsMenuTreeDo();
            MenuTree.Id = 0;
            MenuTree.Name = "全部页面";
        }
    }

    public class BmsUserMenuTreeModifyDo
    {
        public long UserId { get; set; }

        public long OrganizationId { get; set; }

        /// <summary>
        /// 菜单树
        /// </summary>
        public List<Int64> GrantedList { get; set; }

        public BmsUserMenuTreeModifyDo()
        {
            GrantedList = new List<long>();
        }


    }
}
