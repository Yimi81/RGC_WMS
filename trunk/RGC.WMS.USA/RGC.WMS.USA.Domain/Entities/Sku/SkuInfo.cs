using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RGC.WMS.USA.Domain.Entities.Sku
{
    /// <summary>
    /// 产品基础表
    /// </summary>
    [Table("sku_info")]
    public class SkuInfo : FullEntity
    {

        public SkuInfo()
        {
            packageDict = new Dictionary<long, SkuPackageDetail>();
            partsDict = new Dictionary<long, SkuPartsDetail>();
            fittingDict = new Dictionary<long, SkuPartsDetail>();
            customDict = new Dictionary<long, SkuInfoCustom>();
        }        

        #region 产品中英文名称

        /// <summary>
        /// 中文品名
        /// </summary>
        [Column("c_name")]
        public string CName { get; set; }

        /// <summary>
        /// 中文全称
        /// </summary>
        [Column("full_c_name")]
        public string FullCName { get; set; }

        /// <summary>
        /// 英文简称
        /// </summary>
        [Column("e_name")]
        public string EName { get; set; }

        /// <summary>
        /// 英文全称
        /// </summary>
        [Column("full_e_name")]
        public string FullEName { get; set; }

        #endregion

        [Column("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// SKU状态
        /// </summary>
        [Column("status")]
        public ProductStatus Status { get; set; }

        /// <summary>
        /// SKU
        /// 12-52-0
        /// </summary>
        [Column("sku")]
        public string SKU { get; set; }

        /// <summary>
        /// UPC
        /// 853252006081 
        /// </summary>
        [Column("upc")]
        public string UPC { get; set; }

        /// <summary>
        /// 认证
        /// </summary>
        [Column("certification")]
        public string Certification { get; set; }

        /// <summary>
        /// 工厂型号，正常产品唯一
        /// </summary>
        [Column("factory_model")]
        public string FactoryModel { get; set; }

        /// <summary>
        /// 功能分类Id
        /// </summary>
        [Column("func_category_id")]
        public long FuncCategoryId { get; set; }

        /// <summary>
        /// 功能分类对象
        /// </summary>
        [NotMapped]
        public ProductCategory FuncCategory { get; set; }


        /// <summary>
        /// 产品尺寸（inch）
        /// 长（深）  宽  高
        /// </summary>
        [Column("product_size")]
        public string ProductSize { get; set; }

        /// <summary>
        /// 包装尺寸（inch）
        /// </summary>
        [Column("packing_size")]
        public string PackingSize { get; set; }

        /// <summary>
        /// 净重（LB），目前小数点后1位
        /// </summary>
        [Column("net_weight")]
        public decimal NetWeight { get; set; }

        /// <summary>
        /// 毛重（LB），目前小数点后1位
        /// </summary>
        [Column("gross_weight")]
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// 产品主图
        /// </summary>
        [Column("primary_image_src")]
        public string PrimaryImageSrc { get; set; }

        [NotMapped]
        public string PrimaryImageSrcFull { get; set; }

        /// <summary>
        /// 包装配置
        /// </summary>
        [Column("packing_config")]
        public string PackingConfig { get; set; }

        /// <summary>
        /// 1托数量
        /// qit/pallet
        /// </summary>
        [Column("qty_pallet")]
        public int QtyPallet { get; set; }

        /// <summary>
        /// 40HQ装柜数
        /// </summary>
        [Column("loading_qty_40HQ")]
        public string LoadingQty_40HQ { get; set; }

        /// <summary>
        /// 产品卖点
        ///Bullet Point
        /// </summary>
        [Column("bullet_point")]
        public string BulletPoint { get; set; }

        /// <summary>
        /// 英文卖点
        ///Bullet Point
        /// </summary>
        [Column("e_bullet_point")]
        public string EBulletPoint { get; set; }

        /// <summary>
        /// 后台备注
        /// </summary>
        [Column("remarks")]
        public string Remarks { get; set; }

        [JsonIgnoreAttribute]
        [NotMapped]
        public Dictionary<long, SkuPackageDetail> packageDict { get; set; }

        [JsonIgnoreAttribute]
        [NotMapped]
        public Dictionary<long, SkuPartsDetail> partsDict { get; set; }

        [JsonIgnoreAttribute]
        [NotMapped]
        public Dictionary<long, SkuPartsDetail> fittingDict { get; set; }

        [JsonIgnoreAttribute]
        [NotMapped]
        public Dictionary<long, SkuInfoCustom> customDict { get; set; }

    }


}
