using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Entities.Product.Enum;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class SkuSearchOutput
    {
        /// <summary>
        /// SKU主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 产品主键
        /// </summary>
        public long ProductId { get; set; }

        #region 产品名称
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
        /// 主图全路径
        /// </summary>
        public string PrimaryImageSrcFull { get; set; }

        /// <summary>
        /// 功能分类对象
        /// </summary>
        public ProductCategorySearchOutput FuncCategory { get; set; }

        /// <summary>
        /// SKU状态
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// 工厂型号，正常产品唯一
        /// </summary>
        public string FactoryModel { get; set; }

        /// <summary>
        /// 产品卖点
        /// </summary>
        public string BulletPoint { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public string ModifyTimeString { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTimeString { get; set; }
        
    }

}
