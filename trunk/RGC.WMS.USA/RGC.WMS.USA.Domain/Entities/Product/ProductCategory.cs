using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Product
{
    /// <summary>
    /// 产品分类表
    /// </summary>
    [Table("product_category")]
    public class ProductCategory : FullEntity
    {
        public ProductCategory()
        {
            IsShow = false;
        }

        /// <summary>
        /// 父级Id
        /// </summary>
        [Column("parent_id")]
        public long ParentId { get; set; }

        [Column("type")]
        public CategoryType Type { get; set; }

        /// <summary>
        /// 分类编码，唯一
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

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
        /// 图片地址
        /// </summary>
        [Column("image_src")]
        public string ImageSrc { get; set; }

        [NotMapped]
        public string ImageSrcFull { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("seq_no")]
        public int SeqNo { get; set; }

        /// <summary>
        /// 是否在前端展示
        /// 针对每个终端平台单位维护
        /// </summary>
        [Column("is_show")]
        public bool IsShow { get; set; }

        [NotMapped]
        public string byteStr { get; set; }


    }
    public class ProductCategoryTree
    {
        public long Id { get; set; }

        public bool IsShow { get; set; }

        public string Code { get; set; }

        public string EName { get; set; }
        public CategoryType Type { get; set; }

        public string CName { get; set; }

        public long ParentId { get; set; }

        public int SeqNo { get; set; }

        public string ImageSrc { get; set; }

        public string ImageSrcFull { get; set; }

        public List<ProductCategoryTree> Children { get; set; }

        public ProductCategoryTree()
        {
            Children = new List<ProductCategoryTree>();
        }
    }

    public class ProductCategoryCascader
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public long value { get; set; }

        public long code { get; set; }

        public string imageSrc { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public List<ProductCategoryCascader> children { get; set; }
    }
}
