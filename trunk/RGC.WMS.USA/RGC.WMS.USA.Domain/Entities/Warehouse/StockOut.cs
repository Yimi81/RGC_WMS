using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    ///<summary>
    ///
    ///</summary>
    [Table("stock_out")]
    public partial class StockOut : WorkFlowEntity
    {
        public StockOut()
        {
            
            CreationTime = DateTime.Now;
        }

        /// <summary>
        /// Desc:出库单号，系统自动生成
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("stock_out_num")]
        public string StockOutNum { get; set; }

        /// <summary>
        /// Desc:出库订单
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("order_num")]
        public string OrderNum { get; set; }

        /// <summary>
        /// Desc:出库类型:订单出库 调拨出库 退货出库（退至中国仓） 其他
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("stock_out_type")]
        public StockOutType StockOutType { get; set; }

        /// <summary>
        /// Desc: 总状态 未出库 出库中 已出库
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("stock_out_status")]
        public StockOutStatus StockOutStatus { get; set; }

        /// <summary>
        /// 出库源仓库
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("warehouse_id")]
        public long WarehouseId { get; set; }

        /// <summary>
        /// 调拨单需要使用 出库到目标仓库
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("to_warehouse_id")]
        public long ToWarehouseId { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        #region 不需要映射到数据的字段区域
        /// <summary>
        /// 明细清单
        /// </summary>
        [NotMapped]
        public Dictionary<long, StockOutDetail> DetailDict { get; set; }
        /// <summary>
        /// From Warehouse
        /// </summary>
        [NotMapped]
        public WarehouseInfo Warehouse { get; set; }
        /// <summary>
        /// To Warehouse
        /// </summary>
        [NotMapped]
        public WarehouseInfo ToWarehouse { get; set; }
        #endregion
    }
}