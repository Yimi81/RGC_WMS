using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Application.Bidding.Dto
{
    public class CompetitionEditDto : FullEntity
    {
        public long ItemId { get; set; }
        public long ProductId { get; set; }

        public string Name { get; set; }
        public string BrandName { get; set; }

        public string FactoryModel { get; set; }

        public int Status { get; set; }

        public int RetailPrice { get; set; }

        public string RetailPriceString { get; set; }

        /// <summary>
        /// sku产品图片
        /// </summary>
        public string Src { get; set; }

        public string SrcFull { get; set; }

        public string ImageMain { get; set; }

        /// <summary>
        /// 平台id
        /// </summary>
        public long PlatformId { get; set; }

        public string PlatformName { get; set; }

        /// <summary>
        /// 平台sku链接
        /// </summary>
        public string PlatformUrl { get; set; }

        public string UniqueId { get; set; }

        /// <summary>
        /// 是否有效 无效不抓取、不调接口
        /// </summary>
        public bool IsValid { get; set; }
        public string Remarks { get; set; }

    }
}
