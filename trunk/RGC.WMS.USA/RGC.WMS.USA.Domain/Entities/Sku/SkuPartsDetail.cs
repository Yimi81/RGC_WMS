using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// 产品部件/配件表
    /// 2级
    /// </summary>
    [Table("sku_part_detail")]
    public class SkuPartsDetail : FullEntity
    {

        public SkuPartsDetail()
        {
            detailList = new List<SkuPartsDetailEx>();
        }
        /// <summary>
        /// 配置总表关联id
        /// </summary>
        [Column("config_id")]
        public long ConfigId { get; set; }

        /// <summary>
        /// sku配置关联id
        /// </summary>
        [Column("package_id")]
        public long PackageId { get; set; }

        [Column("sku_id")]
        public long SkuId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Column("type")]
        public ConfigurationType Type { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        [Column("c_name")]
        public string CName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [Column("e_name")]
        public string EName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Column("value")]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("desc")]
        public string Desc { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("e_remarks")]
        public string ERemarks { get; set; }

        /// <summary>
        /// 材质
        /// </summary>
        [Column("material")]
        public string Material { get; set; }

        [Column("e_material")]
        public string EMaterial { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        [Column("surface")]
        public string Surface { get; set; }

        [Column("e_surface")]
        public string ESurface { get; set; }

        /// <summary>
        /// 显示排序 升序，默认0
        /// </summary>
        [Column("seq_no")]
        public virtual int SeqNo { get; set; }

        [NotMapped]
        public int Index { get; set; }

        [NotMapped]
        public List<SkuPartsDetailEx> detailList { get; set; }

    }
}
