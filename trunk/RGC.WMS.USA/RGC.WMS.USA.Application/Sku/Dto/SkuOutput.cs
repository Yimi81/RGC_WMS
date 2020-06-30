using RGC.WMS.USA.Domain.Entities.Product.Enum;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class SkuOutput
    {
        #region 产品中英文名称
        /// <summary>
        /// 中文品名
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 中文全称
        /// </summary>
        public string FullCName { get; set; }

        /// <summary>
        /// 英文简称
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 英文全称
        /// </summary>
        public string FullEName { get; set; }
        #endregion

        /// <summary>
        /// SKU状态
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// 工厂型号，正常产品唯一
        /// </summary>
        public string FactoryModel { get; set; }

        /// <summary>
        /// 主图全路径
        /// </summary>
        public string PrimaryImageSrcFull { get; set; }
    }
}
