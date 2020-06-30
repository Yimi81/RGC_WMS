namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class SkuCostBatchFilterOutput
    {
        /// <summary>
        /// 目标仓库主键
        /// </summary>
        //public long WarehouseId { get; set; }

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
        /// 批次号码
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 当前批次的状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTimeString { get; set; }

        #region 不需要映射到数据库
        /// <summary>
        /// Warehouse
        /// </summary>
        //public PackingListWarehouseOutput Warehouse { get; set; }

        /// <summary>
        /// Sku
        /// </summary>
        public SkuOutput Sku { get; set; }
        #endregion
    }
}
