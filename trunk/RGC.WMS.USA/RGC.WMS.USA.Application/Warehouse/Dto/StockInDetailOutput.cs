using RGC.WMS.USA.Application.Purchase.Dto;
using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    /// <summary>
    /// 入库明细清单
    /// </summary>
    public class StockInDetailOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 发货明细Id, 其他入库类型 为0
        /// </summary>
        public long PackingDetailId { get; set; }

        /// <summary>
        /// 明细状态 未入库 入库中 已入库
        /// </summary>
        public StockInStatus Status { get; set; }

        /// <summary>
        /// 产品主键
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// SKU主键
        /// </summary>
        public long SkuId { get; set; }

        /// <summary>
        /// SKU价格表主键
        /// </summary>
        public long SkuCostId { get; set; }

        /// <summary>
        /// SKU批次表主键
        /// </summary>
        public long SkuCostBatchId { get; set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        public int PlanInQty { get; set; }

        /// <summary>
        /// Estimated Time of Departure
        /// 预计离岸时间
        /// </summary>
        public string ETD { get; set; }

        /// <summary>
        /// Estimated Time of Arrival
        /// 预计到港时间
        /// </summary>
        public string ETA { get; set; }

        /// <summary>
        /// 实际数量
        /// </summary>
        public int ActInQty { get; set; }

        /// <summary>
        /// 实际型号
        /// </summary>
        public string ActFactoryModel { get; set; }

        /// <summary>
        /// Actual Time of Departure
        /// 实际离岸时间
        /// </summary>
        public string ATD { get; set; }

        /// <summary>
        /// Actual Time of Arrival -port
        /// 实际到港时间 
        /// </summary>
        public string ATAPort { get; set; }

        /// <summary>
        /// Actual Time of Arrival -warehouse
        /// 实际到仓时间 
        /// </summary>
        public string ATAWarehouse { get; set; }

        /// <summary>
        /// 延迟原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 货架标识
        /// </summary>
        public string StoragerackNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #region 不需要映射到数据库
        /// <summary>
        /// 发货单明细数据
        /// </summary>
        public PackingListDetailOutput PackingDetail { get; set; }
        /// <summary>
        /// Sku
        /// </summary>
        public SkuOutput Sku { get; set; }

        /// <summary>
        /// Sku Cost Batch
        /// </summary>
        public SkuCostBatchOutput SkuCostBatch { get; set; }
        #endregion
    }
}
