using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    /// <summary>
    /// 出库明细清单
    /// </summary>
    public class StockOutDetailEditInput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

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
        public int PlanOutQty { get; set; }

        /// <summary>
        /// 实际数量
        /// </summary>
        public int ActOutQty { get; set; }

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
