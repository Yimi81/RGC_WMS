using HuigeTec.Core.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Bidding
{
    [Table("competition")]
    public class Competition : FullEntity
    {

        [Column("name")]
        public string Name { get; set; }

        [Column("brand_name")]
        public string BrandName { get; set; }

        [Column("factory_model")]
        public string FactoryModel { get; set; }

        [Column("item_id")]
        public long ItemId { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        [NotMapped]
        public int Status { get; set; }

        [Column("retail_price")]
        public int RetailPrice { get; set; }

        [NotMapped]
        public string RetailPriceString { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        [Column("src")]
        public string Src { get; set; }

        [NotMapped]
        public string SrcFull { get; set; }

        /// <summary>
        /// 平台id
        /// </summary>
        [Column("platform_id")]
        public long PlatformId { get; set; }

        [NotMapped]
        public string PlatformName { get; set; }

        /// <summary>
        /// 平台链接
        /// </summary>
        [Column("platform_url")]
        public string PlatformUrl { get; set; }

        [Column("unique_id")]
        public string UniqueId { get; set; }

        /// <summary>
        /// 是否有效 无效不抓取、不调接口
        /// </summary>
        [Column("is_valid")]
        public bool IsValid { get; set; }

        
        /// <summary>
        /// 是否有效 无效不抓取、不调接口
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }

    }


    public class PlatformCompetitionObj
    {
        public long platformId { get; set; }
        public string platformName { get; set; }
        public bool isProxy { get; set; }

        public List<Competition> list { get; set; }

        public PlatformCompetitionObj()
        {
            isProxy = false;
            platformId = 0;
            platformName = "";
            list = new List<Competition>();
        }
    }
}
