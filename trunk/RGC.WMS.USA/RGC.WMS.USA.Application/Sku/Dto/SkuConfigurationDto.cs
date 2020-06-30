using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class SkuConfigurationDto 
    {

        public SkuConfigurationDto()
        {
            children = new List<SkuConfigurationDetail>();
        }
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

        public List<SkuConfigurationDetail> children { get; set; }



    }
}
