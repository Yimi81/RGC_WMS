using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Product
{
    /// <summary>
    /// 产品配置细节表
    /// </summary>
    [Table("product_config_detail")]
    public class ProductConfigDetail : FullEntity
    {
        /// <summary>
        /// 父级配置id
        /// </summary>
        [Column("config_id")]
        public long ConfigId { get; set; }

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
        /// 显示排序 升序，默认0
        /// </summary>
        [Column("seq_no")]
        public virtual int SeqNo { get; set; }

    }
}
