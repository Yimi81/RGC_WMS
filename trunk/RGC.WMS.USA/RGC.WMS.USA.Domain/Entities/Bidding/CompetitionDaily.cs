using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Item.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Bidding
{
    /// <summary>
    /// item每日报价表
    /// </summary>
    [Table("competition_daily")]
    public class CompetitionDaily
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("competition_id")]
        public long CompetitionId { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("item_id")]
        public long ItemId { get; set; }

        [Column("platform_id")]
        public long PlatformId { get; set; }

        /// <summary>
        /// 1:接口,2:爬虫,3:手工录入
        /// </summary>
        [Column("source")]
        public DailyPriceSource Source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        [Column("factory_model")]
        public string FactoryModel { get; set; }

        /// <summary>
        /// sku产品图片
        /// </summary>
        [Column("src")]
        public string Src { get; set; }

        [NotMapped]
        public string SrcFull { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("platform_name")]
        public string PlatformName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }

        [NotMapped]
        public string DateString
        {
            get { return Date.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("price")]
        public int Price { get; set; }

        [Column("review_score")]
        public string ReviewScore { get; set; }

        [NotMapped]
        public decimal ReviewScoreFloat { get; set; }
        

        [Column("review_count")]
        public int ReviewCount { get; set; }

        [NotMapped]
        public string PriceString { get; set; }
        [Column("remarks")]
        public string Remarks { get; set; }

    }


    public class CompetitionDailyObj
    {
        public int count { get; set; }

        public ItemDailyPrice price { get; set; }

        public CompetitionDailyObj()
        {
            count =0;
            price = new ItemDailyPrice();
        }

    }
}
