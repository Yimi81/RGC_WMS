using HuigeTec.Core.Domain.Entities;
using System;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    public class WarehouseFilterOutput : FullEntity
    {
        /// <summary>
        /// 仓库自定义编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string Name { get; set; }

        #region 用于订单定位到具体某个仓库
        /// <summary>
        /// 邮编前缀
        /// </summary>
        public string PostCodePrefix { get; set; }
        #endregion

        /// <summary>
        /// 仓库地址
        /// </summary>
        public string Address { get; set; }

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
