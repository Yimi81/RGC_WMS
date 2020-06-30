using HuigeTec.Core.Domain.Entities;
using System;

namespace RGC.WMS.USA.Application.Bms.Dto
{
    public class BmsMenuDto
    {
        public Int64 Id { get; set; }

        /// <summary>
        /// 唯一编码
        /// </summary>
        public string AuthorizeCode { get; set; }

        public BmsMenuType Type { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否显示菜单
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
        /// 0：一级菜单
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 图标名称
        /// https://materializecss.com/icons.html
        /// 默认图标：computer
        /// </summary>
        public string IcoName { get; set; }

        public string Remark { get; set; }
    }

    public class BmsMenuSeqNoDto
    {
        public long Id { get; set; }
        public int SeqNo { get; set; }
    }

    public class BmsMenuSystemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string AuthorizeCode { get; set; }
        public string Remark { get; set; }
        public int SeqNo { get; set; }
        public BmsMenuType Type { get; set; }

    }
    public class BmsMenuSimpleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string AuthorizeCode { get; set; }
        public BmsMenuType Type { get; set; }

    }
}
