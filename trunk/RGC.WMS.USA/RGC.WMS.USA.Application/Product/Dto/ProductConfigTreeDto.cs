using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Application.Product.Dto
{

    [NotMapped]
    public class ProductConfigTreeDto
    {
        /// <summary>
        /// Configuration Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 父级配置id
        /// 
        /// </summary>
        public long ParentId { get; set; }

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

        /// <summary>
        /// 子节点
        /// </summary>
        public List<ProductConfigTreeDto> Children { get; set; }

        public ProductConfigTreeDto()
        {
            Children = new List<ProductConfigTreeDto>();
        }

    }

}
