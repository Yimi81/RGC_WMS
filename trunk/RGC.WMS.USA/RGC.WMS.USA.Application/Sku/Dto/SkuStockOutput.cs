using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class SkuStockOutput : FullEntity
    {
        /// <summary>
        /// SkuId
        /// </summary>
        public long SkuId { get; set; }

        public SkuOutput Sku { get; set; }

        /// <summary>
        /// ProductId
        /// </summary>
        public long ProductId { get; set; }

        public long SkuCostId { get; set; }

        public SkuCostOutput SkuCost { get; set; }

        public long BatchId { get; set; }

        public string BatchNo { get; set; }

        /// <summary>
        /// 仓库Id
        /// </summary>
        public long WarehouseId { get; set; }

        /// <summary>
        /// Warehouse
        /// </summary>
        public SkuWarehouseOutput Warehouse { get; set; }

        /// <summary>
        /// 现货库存
        /// </summary>
        public int CurrentStock { get; set; }

        public int AvailableStock { get; set; }

        /// <summary>
        /// 不可售库存
        /// </summary>
        public int LockStock { get; set; }

        /// <summary>
        /// 在途库存
        /// </summary>
        public int OnWayStock { get; set; }

        /// <summary>
        /// 预入库存
        /// </summary>
        public int PreStock { get; set; }

        /// <summary>
        /// 安全库存
        /// </summary>
        public int SafeStock { get; set; }

        public int OrderStock { get; set; }

        /// <summary>
        /// 低库存
        /// </summary>
        public int LowStock { get; set; }

        public string CreatorUser { get; set; }

        public string LastModificationTimeString { get; set; }

        public string CreationTimeString { get; set; }
    }
}
