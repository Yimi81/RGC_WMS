using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Domain.Entities.Purchase.Enum;

namespace RGC.WMS.USA.Application.Purchase.Dto
{
    /// <summary>
    /// 发货的货物信息
    /// </summary>
    public class PackingListDetailOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 集装箱号
        /// </summary>
        public string ContainerNo { get; set; }

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
        /// SKU批次库存主键
        /// </summary>
        public long SkuStockId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public UnitType? Type { get; set; }

        /// <summary>
        /// ETD（预计开航时间）estimated time of departure
        /// </summary>
        public string ETD { get; set; }

        /// <summary>
        /// ETA（预计到达时间）estimated time of arrival
        /// </summary>
        public string ETA { get; set; }

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
