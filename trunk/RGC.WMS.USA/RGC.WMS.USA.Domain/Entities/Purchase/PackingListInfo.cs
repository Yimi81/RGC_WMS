using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Purchase.Enum;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Purchase
{
    /// <summary>
    /// 发货单 MeridianGo 2020/06/17
    /// </summary>
    [Table("packing_list")]
    public class PackingListInfo : FullEntity
    {
        /// <summary>
        /// 目标仓库主键
        /// </summary>
        [Column("to_warehouse_id")]
        public long ToWarehouseId { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        [Column("contract_no")]
        public string ContractNo { get; set; }

        /// <summary>
        /// 发货单整批货物状态
        /// </summary>
        [Column("status")]
        public CargoStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }

        #region 不需要映射到数据库
        /// <summary>
        /// 发货单的物流及货物清单
        /// </summary>
        [NotMapped]
        public Dictionary<long, PackingListDetail> DetailDict { get; set; }

        /// <summary>
        /// To Warehouse
        /// </summary>
        [NotMapped]
        public WarehouseInfo ToWarehouse { get; set; }
        #endregion
    }
}
