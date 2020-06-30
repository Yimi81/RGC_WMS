using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    public class StockInEditInput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 入库类型：采购（发货单）、调拨入库、退货入库（1用户退回？2美国仓退回中国？）
        /// </summary>
        public StockInType StockInType { get; set; }

        /// <summary>
        /// 发货单Id, 其他入库类型 为0
        /// </summary>
        public long PackingId { get; set; }

        /// <summary>
        /// 入库的目标仓库
        /// </summary>
        public long WarehouseId { get; set; }

        /// <summary>
        /// 调拨单需要使用，即从哪个仓库调拨过来
        /// </summary>
        public long FromWarehouseId { get; set; }

        /// <summary>
        /// 入库明细清单
        /// </summary>
        public List<StockInDetailEditInput> Detail { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
