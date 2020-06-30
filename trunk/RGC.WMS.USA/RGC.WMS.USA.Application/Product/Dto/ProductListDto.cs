using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;

namespace RGC.WMS.USA.Application.Product.Dto
{
    public class ProductListDto
    {   
        public long Id { get; set; }
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

        public ProductStatus Status { get; set; }
        public string PrimaryImageSrcFull { get; set; }
        /// <summary>
        /// 工厂型号，正常产品唯一
        /// </summary>
        public string FactoryModel { get; set; }

        /// <summary>
        /// 功能分类对象
        /// </summary>
        public ProductCategory FuncCategory { get; set; }

        public string ImageMain { get; set; }
        public string CreationTimeString { get; set; }
        public string LastModificationTimeString { get; set; }
        public string CreateUser { get; set; }
        public long? DeleterUserId { get; set; }

    }
}
