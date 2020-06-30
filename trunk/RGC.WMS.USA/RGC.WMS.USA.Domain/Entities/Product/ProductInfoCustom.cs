using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Product
{
    /// <summary>
    /// 基本信息自定义表
    /// </summary>
    [Table("product_info_custom")]
    public class ProductInfoCustom : FullEntity
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
