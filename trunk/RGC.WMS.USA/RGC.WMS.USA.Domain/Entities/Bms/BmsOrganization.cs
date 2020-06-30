using HuigeTec.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Bms
{
    [Table("bms_organization")]
    public class BmsOrganization:FullEntity
    {
        public BmsOrganization()
        {
            OrganizationMenuDict = new Dictionary<long, BmsOrganizationMenu>();
            Children = new List<BmsOrganization>();
        }

        [Column("code")]
        public int Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("show_order")]
        public int ShowOrder { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// 0：一级菜单
        /// </summary>
        [Column("parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// 组织架构菜单权限对象
        /// 索引：menuId
        /// </summary>
        [NotMapped]
        public virtual Dictionary<Int64, BmsOrganizationMenu> OrganizationMenuDict { get; set; }

        [NotMapped]
        public List<BmsOrganization> Children { get; set; }

    }
}
