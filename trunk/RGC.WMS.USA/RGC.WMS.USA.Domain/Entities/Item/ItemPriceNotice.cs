using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Item.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Item
{
    [Table("item_price_notice")]
    public class ItemPriceNotice : FullEntity
    {

        [Column("item_id")]
        public long ItemId { get; set; }

        /// <summary>
        /// 中文品名
        /// </summary>
        [Column("item_name")]
        public string ItemName { get; set; }

        [Column("src")]
        public string Src { get; set; }

        [NotMapped]
        public string SrcFull { get; set; }

        /// <summary>
        /// 工厂型号
        /// </summary>
        [Column("factory_model")]
        public string FactoryModel { get; set; }

        [Column("platform_id")]
        public long PlatformId { get; set; }


        [Column("platform_name")]
        public string PlatformName { get; set; }

        /// <summary>
        /// 来源
        /// 1:接口,2:爬虫,3:手工录入
        /// </summary>
        [Column("source")]
        public DailyPriceSource Source { get; set; }

        /// <summary>
        /// 状态
        /// 默认0未发送，1已发送
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 价格获取日期
        /// </summary>
        [Column("price_date")]
        public DateTime PriceDate { get; set; }

        /// <summary>
        /// 当日零售价
        /// </summary>
        [Column("price")]
        public int Price { get; set; }

        [NotMapped]
        public string PriceString { get; set; }

        /// <summary>
        /// 当日最低零售价
        /// </summary>
        [Column("map")]
        public int Map { get; set; }

        [NotMapped]
        public string MapString { get; set; }

        /// <summary>
        /// 邮件发送时间
        /// </summary>
        [Column("send_time")]
        public DateTime? SendTime { get; set; }

    }

}
