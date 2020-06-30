using HuigeTec.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.Item
{
    [Table("item_product")]
    public class ItemProduct : FullEntity
    {
        /// <summary>
        /// 产品id
        /// </summary>
        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("item_id")]
        public long ItemId { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("factory_model")]
        public string FactoryModel { get; set; }

        [Column("product_status")]
        public int ProductStatus { get; set; }


        /// sku产品图片
        /// </summary>
        [Column("src")]
        public string Src { get; set; }

        [NotMapped]
        public string SrcFull { get; set; }

        /// <summary>
        /// 是否有效 无效不抓取、不调接口
        /// </summary>
        [Column("is_valid")]
        public bool IsValid { get; set; }
    }
}
