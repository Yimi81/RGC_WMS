using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsOrganizationTreeDto
    {
        public Int64 Id { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        /// <summary>
        /// 级联名称：部门/职位
        /// </summary>
        public string CascaderName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int ShowOrder { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// 0：一级菜单
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<BmsOrganizationTreeDto> Children { get; set; }

        public BmsOrganizationTreeDto()
        {
            Children = new List<BmsOrganizationTreeDto>();
        }
    }
}
