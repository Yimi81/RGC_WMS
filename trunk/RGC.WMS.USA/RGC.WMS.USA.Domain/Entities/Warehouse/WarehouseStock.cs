using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 库存类型表
    /// </summary>
    [Table("warehouse_stock")]
    public class WarehouseStock : FullEntity
    {
        /// <summary>
        /// 仓库主键
        /// </summary>
        [Column("warehouse_id")]
        public long WarehouseId { get; set; }

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
        /// Sku价格表主键
        /// </summary>
        [Column("sku_cost_id")]
        public long SkuCostId { get; set; }

        /// <summary>
        /// Sku批次表主键
        /// </summary>
        [Column("sku_cost_batch_id")]
        public long SkuCostBatchId { get; set; }

        /// <summary>
        /// 现货库存
        /// </summary>
        [Column("current_qty")]
        public int CurrentQty { get; set; }

        /// <summary>
        /// 锁定库存(不售卖的数量)
        /// </summary>
        [Column("lock_qty")]
        public int LockQty { get; set; }

        /// <summary>
        /// 预占库存，订单出库前预占的库存
        /// </summary>
        [Column("order_qty")]
        public int OrderQty { get; set; }

        /// <summary>
        /// 安全库存（防止超）
        /// </summary>
        [Column("safety_qty")]
        public int SafetyQty { get; set; }

        /// <summary>
        /// 在途库存
        /// </summary>
        [Column("intransit_qty")]
        public int IntransitStock { get; set; }

        /// <summary>
        /// 可用库存，可用库存=现货库存-锁定库存-预占库存-安全库存
        /// </summary>
        [Column("saleable_qty")]
        public int SaleableQty { get; set; }
    }
}
