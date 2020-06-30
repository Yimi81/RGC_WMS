namespace RGC.WMS.USA.Domain.Entities.Item.Do
{
    public class ItemPriceRecordDo
    {
        public long ItemId { get; set; }

        /// <summary>
        /// 默认0已通过审批，1审批未通过，2审批拒绝（这个逻辑暂时不加）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 中文品名
        /// </summary>
        public string ItemName { get; set; }

        public string Src { get; set; }

        public string SrcFull { get; set; }

        /// <summary>
        /// 工厂型号
        /// </summary>
        public string FactoryModel { get; set; }

        public long ProductId { get; set; }
        public long PlatformId { get; set; }

        public string PlatformName { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        public int WholeSalePrice { get; set; }

        public string WholeSalePriceString { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public int RetailPrice { get; set; }

        public string RetailPriceString { get; set; }

        public string CreationTimeString { get; set; }

        public string CreationUserName { get; set; }
    }
}
