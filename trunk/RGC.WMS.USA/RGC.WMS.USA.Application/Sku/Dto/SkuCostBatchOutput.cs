namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class SkuCostBatchOutput
    {
        /// <summary>
        /// 批次号码
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 当前批次的状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
