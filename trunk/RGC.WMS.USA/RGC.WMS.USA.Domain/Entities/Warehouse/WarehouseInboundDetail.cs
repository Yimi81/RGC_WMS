using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Purchase;
using RGC.WMS.USA.Domain.Entities.Purchase.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 入库单明细 MeridianGo 2020/06/23
    /// </summary>
    [Table("warehouse_inbound_detail")]
    public class WarehouseInboundDetail : FullEntity
    {
        /// <summary>
        /// 入库单主键
        /// </summary>
        [Column("warehouse_inbound_id")]
        public long WarehouseInboundId { get; set; }

        #region 业务数据
        /// <summary>
        /// 业务明细主键
        /// </summary>
        [Column("business_detail_id")]
        public long BusinessDetailId { get; set; }
        #endregion

        #region 入库单的真正入库的数据
        /// <summary>
        /// ATA（实际到港时间）actual time of arrival
        /// </summary>
        [Column("ata")]
        public DateTime? ATA { get; set; }

        /// <summary>
        /// AWD（实际到仓日期）actual warehousing date
        /// </summary>
        [Column("awd")]
        public DateTime? AWD { get; set; }

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
        /// 入库数量
        /// </summary>
        [Column("stock")]
        public int Stock { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column("type")]
        public UnitType? Type { get; set; }

        /// <summary>
        /// 延迟备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }
        #endregion

        #region 不需要映射到数据库
        /// <summary>
        /// PackingListDetail
        /// </summary>
        [NotMapped]
        public PackingListDetail PackingListDetail { get; set; }

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
