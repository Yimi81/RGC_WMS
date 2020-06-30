using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    ///<summary>
    ///
    ///</summary>
    [Table("stock_out_detail")]
    public partial class StockOutDetail : WorkFlowEntity
    {
        public StockOutDetail()
        {

            CreationTime = DateTime.Now;
        }

        /// <summary>
        /// Desc:出库主表Id
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("stock_out_id")]
        public long StockOutId { get; set; }

        /// <summary>
        /// Desc: 明细状态 未出库 出库中 已出库
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("status")]
        public StockOutStatus Status { get; set; }

        /// <summary>
        /// Desc:产品Id
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// Desc:产品Sku Id
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("sku_id")]
        public long SkuId { get; set; }

        /// <summary>
        /// Desc:产品Sku Cost Batch Id
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("sku_cost_batch_id")]
        public long SkuCostBatchId { get; set; }

        /// <summary>
        /// Desc:产品Sku Cost Id
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("sku_cost_id")]
        public long SkuCostId { get; set; }

        /// <summary>
        /// Desc:计划数量
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("plan_out_qty")]
        public int PlanOutQty { get; set; }

        /// <summary>
        /// Desc:实际数量
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("act_out_qty")]
        public int ActOutQty { get; set; }

        /// <summary>
        /// Desc:货架标识
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("storagerack_num")]
        public string StoragerackNum { get; set; }

        /// <summary>
        /// Desc: 备注
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        #region 不需要映射到数据的字段区域
        /// <summary>
        /// Sku
        /// </summary>
        [NotMapped]
        public SkuInfo Sku { get; set; }

        /// <summary>
        /// Sku Cost Batch
        /// </summary>
        [NotMapped]
        public SkuCostBatch SkuCostBatch { get; set; }
        #endregion
    }
}