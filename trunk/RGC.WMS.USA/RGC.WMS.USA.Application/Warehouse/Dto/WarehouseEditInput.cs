using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;

namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    public class WarehouseEditInput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 仓库自定义编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 仓库状态
        /// </summary>
        public WarehouseStatus? Status { get; set; }

        #region 用于订单定位到具体某个仓库
        /// <summary>
        /// 邮编前缀
        /// </summary>
        public string PostCodePrefix { get; set; }

        /// <summary>
        /// 地址经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 地址纬度
        /// </summary>
        public string Latitude { get; set; }
        #endregion

        /// <summary>
        /// 仓库地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 仓库备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
