using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 出入库类型表
    /// </summary>
    [Table("warehouse_transfer_type")]
    public class WarehouseTransferType : FullEntity
    {
        /// <summary>
        /// 库存类型名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 库存类型状态
        /// </summary>
        [Column("type")]
        public StockType Type { get; set; }

        /// <summary>
        /// 库存类型备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }
    }
}
