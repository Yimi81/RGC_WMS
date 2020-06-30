using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 出库单 MeridianGo 2020/06/23
    /// </summary>
    [Table("warehouse_outbound_record")]
    public class WarehouseOutboundRecord
    {
        /// <summary>
        /// 出库编号（系统自动生成）
        /// </summary>
        [Column("number")]
        public string Number { get; set; }

        /// <summary>
        /// 出库类型表主键
        /// </summary>
        [Column("warehouse_outbound_type_id")]
        public int WarehouseOutboundTypeId { get; set; }

        #region 业务数据
        /// <summary>
        /// 业务主键
        /// </summary>
        [Column("business_id")]
        public long BusinessId { get; set; }

        /// <summary>
        /// 业务编号
        /// </summary>
        [Column("business_no")]
        public long BusinessNo { get; set; }
        #endregion

        /// <summary>
        /// 发货仓库主键
        /// </summary>
        [Column("from_warehouse_id")]
        public long FromWarehouseId { get; set; }

        #region 收货人信息
        /// <summary>
        /// 收货人
        /// </summary>
        [Column("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// 收货人邮编
        /// </summary>
        [Column("post_code")]
        public string PostCode { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        [Column("address")]
        public string Address { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        [Column("ship_data")]
        public DateTime? ShipData { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        [Column("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }
        #endregion

        #region 不需要映射到数据库
        /// <summary>
        /// 出库单明细
        /// </summary>
        [NotMapped]
        public Dictionary<long, WarehouseOutboundDetail> Detail { get; set; }

        /// <summary>
        /// From Warehouse
        /// </summary>
        [NotMapped]
        public WarehouseInfo FromWarehouse { get; set; }
        #endregion
    }
}
