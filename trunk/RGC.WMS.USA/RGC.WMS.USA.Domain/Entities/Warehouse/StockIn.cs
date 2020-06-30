using RGC.WMS.USA.Domain.Entities.Purchase;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    ///<summary>
    ///
    ///</summary>
    [Table("stock_in")]
    public partial class StockIn : WorkFlowEntity
    {
        public StockIn()
        {
            CreationTime = DateTime.Now;
        }

        /// <summary>
        /// Desc:入库单号，系统自动生成
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("stock_in_num")]
        public string StockInNum { get; set; }

        /// <summary>
        /// Desc:入库类型：采购（发货单）、调拨入库、退货入库（1用户退回？2美国仓退回中国？）
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("stock_in_type")]
        public StockInType StockInType { get; set; }

        /// <summary>
        /// Desc:发货单Id, 其他入库类型 为0
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("packing_id")]
        public long PackingId { get; set; }

        /// <summary>
        /// Desc: 入库的目标仓库
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("warehouse_id")]
        public long WarehouseId { get; set; }

        /// <summary>
        /// Desc: 调拨单需要使用，即从哪个仓库调拨过来
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("from_warehouse_id")]
        public long FromWarehouseId { get; set; }

        /// <summary>
        /// Desc: 总状态 未入库 入库中 已入库
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("stock_in_status")]
        public StockInStatus StockInStatus { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        #region 不需要映射到数据的字段区域
        /// <summary>
        /// PackingList
        /// </summary>
        [NotMapped]
        public PackingListInfo PackingList { get; set; }

        /// <summary>
        /// 明细清单
        /// </summary>
        [NotMapped]
        public Dictionary<long, StockInDetail> DetailDict { get; set; }

        /// <summary>
        /// To Warehouse
        /// </summary>
        [NotMapped]
        public WarehouseInfo Warehouse { get; set; }

        /// <summary>
        /// From Warehouse
        /// </summary>
        [NotMapped]
        public WarehouseInfo FromWarehouse { get; set; }
        #endregion
    }
}