using RGC.WMS.USA.Domain.Entities.Purchase.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 出库单明细 MeridianGo 2020/06/23
    /// </summary>
    [Table("warehouse_outbound_detail")]
    public class WarehouseOutboundDetail
    {
        /// <summary>
        /// 出库单主键
        /// </summary>
        [Column("warehouse_outbound_id")]
        public long WarehouseOutboundId { get; set; }

        /// <summary>
        /// 产品主键
        /// </summary>
        [Column("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// SKU主键
        /// </summary>
        [Column("sku_id")]
        public long SkuId { get; set; }

        /// <summary>
        /// SKU价格表主键
        /// </summary>
        [Column("sku_cost_id")]
        public long SkuCostId { get; set; }

        /// <summary>
        /// SKU批次表主键
        /// </summary>
        [Column("sku_cost_batch_id")]
        public long SkuCostBatchId { get; set; }

        /// <summary>
        /// SKU批次库存主键
        /// </summary>
        [Column("sku_stock_id")]
        public long SkuStockId { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        [Column("stock")]
        public int Stock { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column("type")]
        public UnitType? Type { get; set; }

        #region 不需要映射到数据库
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
