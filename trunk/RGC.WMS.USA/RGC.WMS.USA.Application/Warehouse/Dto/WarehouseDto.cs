using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    public class WarehouseDto : FullEntity
    {
        /// <summary>
        /// 仓库自定义编号
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 仓库状态
        /// </summary>
        public WarehouseStatus? status { get; set; }

        #region 用于订单定位到具体某个仓库
        /// <summary>
        /// 邮编前缀
        /// </summary>
        public string postCodePrefix { get; set; }

        /// <summary>
        /// 地址经度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 地址纬度
        /// </summary>
        public string latitude { get; set; }
        #endregion

        /// <summary>
        /// 仓库地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 仓库备注
        /// </summary>
        public string remarks { get; set; }

        public string creationTimeString
        {
            get
            {
                return CreationTime.ToDateTimeZNString();
            }
        }
        public string lastModificationTimeString
        {
            get
            {
                return LastModificationTime.ToDateTimeZNString();
            }
        }

        public string deletionTimeString
        {
            get
            {
                return DeletionTime.ToDateTimeZNString();
            }
        }
        public string createUser { get; set; }
        public string lastModifierUser { get; set; }
        public string deleterUser { get; set; }
        
    }
}
