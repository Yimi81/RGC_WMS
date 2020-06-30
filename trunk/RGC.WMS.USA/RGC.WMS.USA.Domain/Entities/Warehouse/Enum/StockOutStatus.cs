namespace RGC.WMS.USA.Domain.Entities.Warehouse.Enum
{
    public enum StockOutStatus
    {
        /// <summary>
        /// 初始状态 未出库
        /// </summary>
        Initial = 1,

        /// <summary>
        /// 出库中
        /// </summary>
        Extracting = 2,

        /// <summary>
        /// 已出库
        /// </summary>
        Extracted = 3
    }
}
