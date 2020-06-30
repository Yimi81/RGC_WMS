using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Warehouse;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// sku库存修改记录表
    /// 
    /// </summary>
    [Table("sku_stock_record")]
    public class SkuStockRecord : FullEntity
    {
        public SkuStockRecord()
        {

        }
        /// <summary>
        /// sku_stock表的Id
        /// </summary>
        [Column("sku_stock_id")]
        public long SkuStockId { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        [Column("sku_id")]
        public long SkuId { get; set; }

        [NotMapped]
        public SkuInfo Sku { get; set; }

        /// <summary>
        /// ProductId
        /// </summary>
        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("sku_cost_id")]
        public long SkuCostId { get; set; }

        [Column("batch_id")]
        public long BatchId { get; set; }

        [Column("batch_no")]
        public string BatchNo { get; set; }

        /// <summary>
        /// 仓库Id
        /// </summary>
        [Column("warehouse_id")]
        public long WarehouseId { get; set; }

        [NotMapped]
        public WarehouseInfo ToWarehouse { get; set; }

        /// <summary>
        /// 现货库存
        /// </summary>
        [Column("current_stock")]
        public int CurrentStock { get; set; }

        [NotMapped]
        public int AvailableStock { get; set; }

        /// <summary>
        /// 不可售库存
        /// </summary>
        [Column("lock_stock")]
        public int LockStock { get; set; }

        /// <summary>
        /// 在途库存
        /// </summary>
        [Column("onway_stock")]
        public int OnWayStock { get; set; }

        /// <summary>
        /// 预入库存
        /// </summary>
        [Column("pre_stock")]
        public int PreStock { get; set; }

        /// <summary>
        /// 预占库存，订单出库前预占的库存
        /// </summary>
        [Column("order_stock")]
        public int OrderStock { get; set; }

        /// <summary>
        /// 安全库存
        /// </summary>
        [Column("safe_stock")]
        public int SafeStock { get; set; }

        /// <summary>
        /// 低库存
        /// </summary>
        [Column("low_stock")]
        public int LowStock { get; set; }

        [NotMapped]
        public string CreatorUser { get; set; }

        [NotMapped]
        public string LastModificationTimeString { get; set; }

        [NotMapped]
        public string CreationTimeString { get; set; }
    }

}
