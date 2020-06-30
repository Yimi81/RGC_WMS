using RGC.WMS.USA.Domain.Entities.Purchase;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    ///<summary>
    ///
    ///</summary>
    [Table("stock_in_detail")]
    public partial class StockInDetail : WorkFlowEntity
    {
        public StockInDetail()
        {
            CreationTime = DateTime.Now;
        }

        /// <summary>
        /// Desc:入库主表Id
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("stock_in_id")]
        public long StockInId { get; set; }

        /// <summary>
        /// Desc:发货明细Id, 其他入库类型 为0
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("packing_detail_id")]
        public long PackingDetailId { get; set; }

        /// <summary>
        /// Desc: 明细状态 未入库 入库中 已入库
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("status")]
        public StockInStatus Status { get; set; }

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
        [Column("plan_in_qty")]
        public int PlanInQty { get; set; }

        /// <summary>
        /// Estimated Time of Departure
        /// 预计离岸时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("etd")]
        public DateTime? ETD { get; set; }

        /// <summary>
        /// Estimated Time of Arrival
        /// 预计到港时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("eta")]
        public DateTime? ETA { get; set; }

        /// <summary>
        /// Desc:实际数量
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("act_in_qty")]
        public int ActInQty { get; set; }

        /// <summary>
        /// 实际型号
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("act_factory_model")]
        public string ActFactoryModel { get; set; }

        /// <summary>
        /// Actual Time of Departure
        /// 实际离岸时间
        /// 预留
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("atd")]
        public DateTime? ATD { get; set; }

        /// <summary>
        /// Actual Time of Arrival -port
        /// 实际到港时间 
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("ata_port")]
        public DateTime? ATAPort { get; set; }

        /// <summary>
        /// Actual Time of Arrival -warehouse
        /// 实际到仓时间 
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("ata_warehouse")]
        public DateTime? ATAWarehouse { get; set; }

        /// <summary>
        /// Desc:延迟原因
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Desc:货架标识
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("storagerack_num")]
        public string StoragerackNum { get; set; }
        //public long StoragerackId { get; set; }

        /// <summary>
        /// Desc: 备注
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        #region 不需要映射到数据的字段区域
        /// <summary>
        /// PackingDetail
        /// </summary>
        [NotMapped]
        public PackingListDetail PackingDetail { get; set; }

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