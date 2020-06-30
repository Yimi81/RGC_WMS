using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Warehouse
{
    /// <summary>
    /// 仓库基础表
    /// </summary>
    [Table("warehouse")]
    public class WarehouseInfo : FullEntity
    {
        /// <summary>
        /// 仓库自定义编号
        /// </summary>
        [Column("number")]
        public string Number { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 仓库状态
        /// </summary>
        [Column("status")]
        public WarehouseStatus? Status { get; set; }

        #region 用于订单定位到具体某个仓库
        /// <summary>
        /// 邮编前缀
        /// </summary>
        [Column("post_code_prefix")]
        public string PostCodePrefix { get; set; }

        /// <summary>
        /// 地址经度
        /// </summary>
        [Column("longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// 地址纬度
        /// </summary>
        [Column("latitude")]
        public string Latitude { get; set; }
        #endregion

        /// <summary>
        /// 仓库地址
        /// </summary>
        [Column("address")]
        public string Address { get; set; }

        /// <summary>
        /// 仓库备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }
    }
}
