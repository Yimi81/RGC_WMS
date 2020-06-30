using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// 部件/配件 - 细节表
    /// </summary>
    [Table("sku_part_detail_ex")]
    public class SkuPartsDetailEx : FullEntity
    {

        /// <summary>
        /// sku配件主表id
        /// </summary>
        [Column("part_detail_id")]
        public long PartDetailId { get; set; }

        /// <summary>
        /// 主表id
        /// </summary>
        [Column("config_detail_id")]
        public long ConfigDetailId { get; set; }


        [Column("sku_id")]
        public long SkuId { get; set; }


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
        /// 显示排序 升序，默认0
        /// </summary>
        [Column("seq_no")]
        public virtual int SeqNo { get; set; }

        [NotMapped]
        public int Index { get; set; }

    }
}
