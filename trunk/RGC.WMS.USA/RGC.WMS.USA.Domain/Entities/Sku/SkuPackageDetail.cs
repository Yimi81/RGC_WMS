using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// sku组件表
    /// 存大类（1级）数据
    /// </summary>
    [Table("sku_package_detail")]
    public class SkuPackageDetail : FullEntity
    {
        public SkuPackageDetail()
        {
            child = new List<SkuPartsDetail>();
        }
        /// <summary>
        /// 配置id
        /// </summary>
        [Column("config_id")]
        public long ConfigId { get; set; }

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


        /// <summary>
        /// 显示排序 升序，默认0
        /// </summary>
        [Column("seq_no")]
        public virtual int SeqNo { get; set; }

        [NotMapped]
        public int Index { get; set; }

        [NotMapped]
        public List<SkuPartsDetail> child { get; set; }

    }
}
