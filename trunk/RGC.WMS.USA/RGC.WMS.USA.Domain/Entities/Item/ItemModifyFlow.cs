using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Bidding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Item
{
    [Table("item_modify_flow")]
    public class ItemModifyFlow : FullEntity
    {
        public ItemModifyFlow()
        {
            ProductDict = new Dictionary<long, ItemProduct>();
            CompetitionDict = new Dictionary<long, Competition>();
        }
        [Column("item_id")]
        public long ItemId { get; set; }
        /// <summary>
        /// 主产品id
        /// </summary>
        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("item_name")]
        public string ItemName { get; set; }

        [Column("factory_model")]
        public string FactoryModel { get; set; }

        [Column("upc")]
        public string UPC { get; set; }

        [Column("sku")]
        public string SKU { get; set; }

        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        [Column("whole_sale_price")]
        public int WholeSalePrice { get; set; }

        [Column("retail_price")]
        public int RetailPrice { get; set; }


        [Column("pre_retail_price")]
        public int PreRetailPrice { get; set; }

        [Column("valid_time")]
        public DateTime ValidTime { get; set; }

        /// <summary>
        /// sku产品图片
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

        [Column("map")]
        public int Map { get; set; }

        [Column("msrp")]
        public int Msrp { get; set; }

        /// <summary>
        /// 平台sku链接
        /// </summary>
        [Column("platform_item_url")]
        public string PlatformItemUrl { get; set; }

        [Column("unique_id")]
        public string UniqueId { get; set; }

        /// <summary>
        /// 是否有效 无效不抓取、不调接口
        /// </summary>
        [Column("is_valid")]
        public bool IsValid { get; set; }

        /// <summary>
        /// 是否是特销产品（组合产品或其他）
        /// </summary>
        [Column("is_special")]
        public bool IsSpecial { get; set; }
        
        /// <summary>
        /// 是否有效 无效不抓取、不调接口
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("sync_status")]
        public int SyncStatus { get; set; }

        [Column("request_sync_time")]
        public DateTime RequestSyncTime { get; set; }

        [NotMapped]
        public Dictionary<long, ItemProduct> ProductDict { get; set; }

        [NotMapped]
        public Dictionary<long, Competition> CompetitionDict { get; set; }

        [NotMapped]
        public Dictionary<long, ItemPriceRecord> PriceRecordDict { get; set; }
    }


}
