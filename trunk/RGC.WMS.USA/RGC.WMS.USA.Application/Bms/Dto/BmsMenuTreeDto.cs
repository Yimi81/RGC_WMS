using HuigeTec.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsMenuTreeDto
    {
        public Int64 Id { get; set; }

        public string AuthorizeCode { get; set; }

        public BmsMenuType Type { get; set; }
        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SeqNo { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// 0:一级菜单
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 图标名称
        /// </summary>
        public string IcoName { get; set; }
        public string Remark { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<BmsMenuTreeDto> Children { get; set; }

        /// <summary>
        /// 是否授权
        /// </summary>
        public bool IsGranted { get; set; }

        /// <summary>
        /// 是否用户单独授权
        /// </summary>
        public bool IsUserGranted { get; set; }

        /// <summary>
        /// 是否用户单独移除
        /// </summary>
        public bool IsUserRemoved { get; set; }
        public BmsMenuTreeDto()
        {
            Children = new List<BmsMenuTreeDto>();
            Name = "全部页面";
        }
    }
}
