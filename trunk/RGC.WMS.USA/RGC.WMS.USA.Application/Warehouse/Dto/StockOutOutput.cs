using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    public class StockOutOutput : FullEntity
    {
        /// <summary>
        /// 出库单号
        /// </summary>
        public string StockOutNum { get; set; }

        /// <summary>
        /// 出库订单
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 入库类型：采购（发货单）、调拨入库、退货入库（1用户退回？2美国仓退回中国？）
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
        /// 总状态 未出库 出库中 已出库
        /// </summary>
        public StockOutStatus StockOutStatus { get; set; }

        /// <summary>
        /// 明细清单
        /// </summary>
        public List<StockOutDetailOutput> Detail { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #region 不需要映射到数据库

        /// <summary>
        /// From Warehouse
        /// </summary>
        public WarehouseOutput Warehouse { get; set; }

        /// <summary>
        /// To Warehouse
        /// </summary>
        public WarehouseOutput ToWarehouse { get; set; }
        #endregion

        #region 创建信息
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间 字符串格式
        /// </summary>
        public string CreationTimeString
        {
            get
            {
                return CreationTime.ToDateTimeZNString();
            }
        }
        #endregion

        #region 最后修改信息
        /// <summary>
        /// 最后修改人名称
        /// </summary>
        public string LastModifierUser { get; set; }

        /// <summary>
        /// 最后修改时间 字符串格式
        /// </summary>
        public string LastModificationTimeString
        {
            get
            {
                return LastModificationTime.ToDateTimeZNString();
            }
        }
        #endregion

        #region 最后删除信息
        /// <summary>
        /// 最后删除名称
        /// </summary>
        public string DeleterUser { get; set; }

        /// <summary>
        /// 最后删除时间 字符串格式
        /// </summary>
        public string DeletionTimeString
        {
            get
            {
                return DeletionTime.ToDateTimeZNString();
            }
        }
        #endregion
    }
}
