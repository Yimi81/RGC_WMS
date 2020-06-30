using HuigeTec.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Item
{
    /// <summary>
    /// 平台在售产品价格修改记录表
    /// </summary>
    [Table("item_price_record")]
    public class ItemPriceRecord : FullEntity
    {
        public ItemPriceRecord()
        {
            
        }

        [Column("item_id")]
        public long ItemId { get; set; }

        /// <summary>
        /// 默认0已通过审批，1审批未通过，2审批拒绝（这个逻辑暂时不加）
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

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

        [Column("product_id")]
        public long ProductId { get; set; }
        [Column("platform_id")]
        public long PlatformId { get; set; }

        [Column("platform_name")]
        public string PlatformName { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        [Column("whole_sale_price")]
        public int WholeSalePrice { get; set; }

        [NotMapped]
        public string WholeSalePriceString { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        [Column("retail_price")]
        public int RetailPrice { get; set; }

        [Column("valid_time")]
        public DateTime? ValidTime{ get; set; }

        /// <summary>
        /// 下发级别 0普通 1紧急
        /// </summary>
        [Column("level")]
        public int Level { get; set; }

        /// <summary>
        /// 是否同步
        /// </summary>
        [Column("sync_status")]
        public int SyncStatus { get; set; }

        /// <summary>
        /// 请求同步时间
        /// </summary>
        [Column("request_sync_time")]
        public DateTime? RequestSyncTime { get; set; }


        [NotMapped]
        public string RetailPriceString { get; set; }

        [NotMapped]
        public string CreationTimeString { get; set; }

        [NotMapped]
        public string CreationUserName { get; set; }




    }

}
