namespace RGC.WMS.USA.Application.Warehouse.Dto
{
    public class WarehouseFilterSimpleOutput
    {
        /// <summary>
        /// 仓库主键
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

        #region 用于订单定位到具体某个仓库
        /// <summary>
        /// 邮编前缀
        /// </summary>
        public string PostCodePrefix { get; set; }
        #endregion
    }
}
