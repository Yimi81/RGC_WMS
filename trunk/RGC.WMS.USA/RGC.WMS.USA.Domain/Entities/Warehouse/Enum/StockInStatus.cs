namespace RGC.WMS.USA.Domain.Entities.Warehouse.Enum
{
    public enum StockInStatus
    {
        /// <summary>
        /// 初始状态 未入库
        /// </summary>
        Initial = 1,

        /// <summary>
        /// 入库中
        /// </summary>
        Receiving = 2,

        /// <summary>
        /// 已入库
        /// </summary>
        Received = 3
    }
}
