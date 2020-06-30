using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// sku成本批次表
    /// 
    /// </summary>
    [Table("sku_cost_batch")]
    public class SkuCostBatch : FullEntity
    {
        public SkuCostBatch()
        {
           
        }

        /// <summary>
        /// SkuCostId
        /// </summary>
        [Column("sku_cost_id")]
        public long SkuCostId { get; set; }

        [NotMapped]
        public SkuCost SkuCost { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        [Column("sku_id")]
        public long SkuId { get; set; }

        [NotMapped]
        public SkuInfo Sku { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        [Column("batch_no")]
        public string BatchNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        [NotMapped]
        public string CreatorUser { get; set; }

        [NotMapped]
        public string LastModificationTimeString { get; set; }

        [NotMapped]
        public string CreationTimeString { get; set; }
    }
}
