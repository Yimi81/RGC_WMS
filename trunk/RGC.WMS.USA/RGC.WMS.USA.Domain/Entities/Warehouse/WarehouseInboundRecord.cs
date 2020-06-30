using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Purchase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 入库单 MeridianGo 2020/06/23
    /// </summary>
    [Table("warehouse_inbound_record")]
    public class WarehouseInboundRecord : FullEntity
    {
        /// <summary>
        /// 入库编号（系统自动生成）
        /// </summary>
        [Column("number")]
        public string Number { get; set; }

        /// <summary>
        /// 入库类型表主键
        /// </summary>
        [Column("warehouse_inbound_type_id")]
        public int WarehouseInboundTypeId { get; set; }

        #region 业务数据
        /// <summary>
        /// 业务主键（目前仅一种发货单入库）
        /// </summary>
        [Column("business_id")]
        public long BusinessId { get; set; }
        #endregion

        #region 不需要映射到数据库
        /// <summary>
        /// 入库单明细
        /// </summary>
        [NotMapped]
        public Dictionary<long, WarehouseInboundDetail> Detail { get; set; }

        /// <summary>
        /// PackingList
        /// </summary>
        [NotMapped]
        public PackingListInfo PackingListInfo { get; set; }
        #endregion
    }
}
