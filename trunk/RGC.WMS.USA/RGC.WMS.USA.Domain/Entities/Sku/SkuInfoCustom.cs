using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// 基本信息自定义表
    /// </summary>
    [Table("sku_info_custom")]
    public class SkuInfoCustom : FullEntity
    {
        [Column("sku_id")]
        public long SkuId { get; set; }

        /// <summary>
        /// 英文参数名
        /// </summary>
        [Column("e_name")]
        public virtual string EName { get; set; }

        /// <summary>
        /// 中文参数名
        /// </summary>
        [Column("c_name")]
        public string CName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Column("value")]
        public virtual string Value { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("seq_no")]
        public virtual int SeqNo { get; set; }
    }
}
