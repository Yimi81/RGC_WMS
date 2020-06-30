namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    /// <summary>
    /// 入库明细清单
    /// </summary>
    public class StockInDetailEditInput
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
        /// 产品Id
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 产品Sku Id
        /// </summary>
        public long SkuId { get; set; }

        /// <summary>
        /// 产品Sku Cost Batch Id
        /// </summary>
        public long SkuCostBatchId { get; set; }

        /// <summary>
        /// 产品Sku Cost Id
        /// </summary>
        public long SkuCostId { get; set; }

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
    }
}
