using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Application.Warehouse.Dto;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Purchase.Dto
{
    public class PackingListOutput : FullEntity
    {
        /// <summary>
        /// 目标仓库主键
        /// </summary>
        public long ToWarehouseId { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 明细清单
        /// </summary>
        public List<PackingListDetailOutput> Detail { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        #region 不需要映射到数据库
        /// <summary>
        /// Warehouse
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
