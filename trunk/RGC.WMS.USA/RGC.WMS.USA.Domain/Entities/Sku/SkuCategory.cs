using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    [Table("category_info")]
    public class SkuCategory : FullEntity
    {
        public SkuCategory()
        {
            IsShow = false;
        }

        /// <summary>
        /// 分类编码，唯一
        /// chauncey 2019/3/1
        /// </summary>
        [Column("code")]
        public long Code { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [Column("e_name")]
        public string EName { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        [Column("c_name")]
        public string CName { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        [Column("parent_Id")]
        public long ParentId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Column("src")]
        public string Src { get; set; }

        [NotMapped]
        public string SrcFull { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("show_order")]
        public int ShowOrder { get; set; }

        /// <summary>
        /// 是否在前端展示
        /// 针对每个终端平台单位维护
        /// </summary>
        [Column("is_show")]
        public bool IsShow { get; set; }

        [NotMapped]
        public string ByteStr { get; set; }
    }

    public class CategoryTree
    {
        public long Id { get; set; }

        public bool IsShow { get; set; }

        public long Code { get; set; }

        public string EName { get; set; }

        public string CName { get; set; }

        public long ParentId { get; set; }

        public int ShowOrder { get; set; }

        public string Src { get; set; }

        public string SrcFull { get; set; }

        public List<CategoryTree> Child { get; set; }

        public CategoryTree()
        {
            Child = new List<CategoryTree>();
        }
    }

    public class CategoryCascader
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public long Value { get; set; }

        public long Code { get; set; }

        public string SrcFull { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public List<CategoryCascader> Children { get; set; }
    }
}
