using RGC.WMS.USA.Domain.Entities.Product.Enum;

namespace RGC.WMS.USA.Application.Product.Dto
{
    public class ProductConfigEditDto
    {
        public long Id { get; set; }
        public long ConfigId { get; set; }
        /// <summary>
        /// 父级配置id
        /// </summary>
        public long PackageId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public ConfigurationType Type { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 显示排序 升序，默认0
        /// </summary>
        public virtual int SeqNo { get; set; }
    }
}
