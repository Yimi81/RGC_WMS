using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    /// <summary>
    /// 出库明细清单
    /// </summary>
    public class StockOutDetailOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 明细状态 未出库 出库中 已出库
        /// </summary>
        public StockOutStatus Status { get; set; }

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

        #region 不需要映射到数据库
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
