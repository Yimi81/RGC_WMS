using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Product.Dto
{
    public class ProductPartsDetailDto : FullEntity
    {

        public ProductPartsDetailDto()
        {
            detailList = new List<ProductPartsDetailEx>();
        }
        /// <summary>
        /// 配置总表关联id
        /// </summary>
        public long ConfigId { get; set; }

        /// <summary>
        /// sku配置关联id
        /// </summary>
        public long PackageId { get; set; }

        public long SkuId { get; set; }

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
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        public string ERemarks { get; set; }

        /// <summary>
        /// 材质
        /// </summary>
        public string Material { get; set; }
        public string EMaterial { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public string Surface { get; set; }
        public string ESurface { get; set; }

        /// <summary>
        /// 显示排序 升序，默认0
        /// </summary>
        public virtual int SeqNo { get; set; }

        public int Index { get; set; }

        public List<ProductPartsDetailEx> detailList { get; set; }
    }

    public class ProductComponentTreeDto
    {
        public ProductComponentTreeDto()
        {
            Children = new List<ProductPartsDetailDto>();
        }
        public long Id { get; set; }
        /// <summary>
        /// 组件Id
        /// </summary>
        public long ConfigId { get; set; }

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

        public string Code { get; set; }
        public int Index { get; set; }
        public int SeqNo { get; set; }
        public bool IsDeleted { get; set; }


        public List<ProductPartsDetailDto> Children { get; set; }



    }
}
