using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Item;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Item.Dto
{
    public class ItemEditDto: FullEntity
    {

        public long Id { get; set; }
        public ItemEditDto()
        {
            ProductList = new List<ItemProduct>();
        }
        /// <summary>
        /// 主产品id
        /// </summary>
        public long ProductId { get; set; }

        public string ItemName { get; set; }

        public string FactoryModel { get; set; }

        public string UPC { get; set; }

        public string SKU { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        public int WholeSalePrice { get; set; }

        public string WholeSalePriceString { get; set; }

        public int RetailPrice { get; set; }

        public string RetailPriceString { get; set; }
        public string MapString { get; set; }
        public int Map { get; set; }
        public string MsrpString { get; set; }
        public int Msrp { get; set; }

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
        public string PlatformItemUrl { get; set; }

        public string UniqueId { get; set; }

        /// <summary>
        /// 是否有效 无效不抓取、不调接口
        /// </summary>
        public bool IsValid { get; set; }
        public string Remarks { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsSync { get; set; }

        public List<ItemProduct> ProductList { get; set; }
    }
}
