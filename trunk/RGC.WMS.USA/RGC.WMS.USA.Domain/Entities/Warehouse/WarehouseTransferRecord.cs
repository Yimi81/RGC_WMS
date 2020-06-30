using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 仓库出入库记录表(当前使用时间内的时段记录)
    /// </summary>
    [Table("warehouse_transfer_record")]
    public class WarehouseTransferRecord : FullEntity
    {
        /// <summary>
        /// 出入库编号（系统自动生成）
        /// </summary>
        [Column("number")]
        public string Number { get; set; }

        /// <summary>
        /// 库存表主键
        /// </summary>
        [Column("warehouse_stock_id")]
        public long WarehouseStockId { get; set; }

        /// <summary>
        /// 出入库类型表主键
        /// </summary>
        [Column("warehouse_transfer_type_id")]
        public int WarehouseTransferTypeId { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        [Column("type")]
        public StockType Type { get; set; }

        /// <summary>
        /// 出入库数量
        /// </summary>
        [Column("stock")]
        public int Stock { get; set; }

        /// <summary>
        /// 出入库备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }
    }
}
