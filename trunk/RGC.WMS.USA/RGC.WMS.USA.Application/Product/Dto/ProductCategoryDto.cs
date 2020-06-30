using RGC.WMS.USA.Domain.Entities.Product.Enum;

namespace RGC.WMS.USA.Application.Product.Dto
{
    public class ProductCategoryDto
    {
        public ProductCategoryDto()
        {
            IsShow = false;
        }
        public long Id { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public long ParentId { get; set; }

        public CategoryType Type { get; set; }

        /// <summary>
        /// 分类编码，唯一
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageSrc { get; set; }

        public string ImageSrcFull { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SeqNo { get; set; }

        /// <summary>
        /// 是否在前端展示
        /// 针对每个终端平台单位维护
        /// </summary>
        public bool IsShow { get; set; }

        public string byteStr { get; set; }
    }
}
