using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Application.Purchase.Dto;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    public class StockInOutput : FullEntity
    {
        /// <summary>
        /// 入库单号
        /// </summary>
        public string StockInNum { get; set; }

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
        /// 总状态 未入库 入库中 已入库
        /// </summary>
        public StockInStatus StockInStatus { get; set; }

        /// <summary>
        /// 入库明细清单
        /// </summary>
        public List<StockInDetailOutput> Detail { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #region 不需要映射到数据库
        /// <summary>
        /// 发货单主数据
        /// </summary>
        public PackingListOutput PackingList { get; set; }

        /// <summary>
        /// To Warehouse
        /// </summary>
        public WarehouseOutput Warehouse { get; set; }

        /// <summary>
        /// From Warehouse
        /// </summary>
        public WarehouseOutput FromWarehouse { get; set; }
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
