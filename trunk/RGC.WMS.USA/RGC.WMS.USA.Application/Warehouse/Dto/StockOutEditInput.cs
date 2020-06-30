using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    public class StockOutEditInput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 出库订单
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 出库类型:订单出库 调拨出库 退货出库（退至中国仓） 其他
        /// </summary>
        public StockOutType StockOutType { get; set; }

        /// <summary>
        /// 出库源仓库
        /// </summary>
        public long WarehouseId { get; set; }

        /// <summary>
        /// 调拨单需要使用 出库到目标仓库
        /// </summary>
        public long ToWarehouseId { get; set; }

        /// <summary>
        /// 出库明细清单
        /// </summary>
        public List<StockOutDetailEditInput> Detail { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
