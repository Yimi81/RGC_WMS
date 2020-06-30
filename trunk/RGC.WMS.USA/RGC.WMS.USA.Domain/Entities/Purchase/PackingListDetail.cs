using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Purchase.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Purchase
{
    /// <summary>
    /// 发货单中的物流及货物清单 MeridianGo 2020/06/22
    /// </summary>
    [Table("packing_list_detail")]
    public class PackingListDetail : FullEntity
    {
        /// <summary>
        /// 发货单主键
        /// </summary>
        [Column("packing_list_id")]
        public long PackingListId { get; set; }

        /// <summary>
        /// 集装箱号
        /// </summary>
        [Column("container_no")]
        public string ContainerNo { get; set; }

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
        /// 数量
        /// </summary>
        [Column("qty")]
        public int Qty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column("type")]
        public UnitType? Type { get; set; }

        /// <summary>
        /// ETD（预计开航时间）estimated time of departure
        /// </summary>
        [Column("etd")]
        public DateTime? ETD { get; set; }

        /// <summary>
        /// ETA（预计到达时间）estimated time of arrival
        /// </summary>
        [Column("eta")]
        public DateTime? ETA { get; set; }

        /// <summary>
        /// 单一货物状态
        /// </summary>
        [Column("status")]
        public CargoStatus Status { get; set; }

        /// <summary>
        /// 实际入库数量
        /// </summary>
        [Column("act_in_qty")]
        public int ActInQty { get; set; }

        #region 不需要映射到数据库
        /// <summary>
        /// Sku
        /// </summary>
        [NotMapped]
        public SkuInfo Sku { get; set; }

        /// <summary>
        /// SkuCostBatch
        /// </summary>
        [NotMapped]
        public SkuCostBatch SkuCostBatch { get; set; }
        #endregion
    }
}
