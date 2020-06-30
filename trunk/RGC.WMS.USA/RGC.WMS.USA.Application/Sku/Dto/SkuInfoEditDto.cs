using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Sku.Dto
{
    public class SkuInfoEditDto
    {
        public SkuInfoEditDto()
        {
            PartChildren = new List<SkuComponentTreeDto>();
            FittingChildren = new List<SkuComponentTreeDto>();
            CustomList = new List<SkuInfoCustom>();
            CategoryIds = new List<int>();
            IsEditable = true;
        }
        /// <summary>
        /// SKU状态
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// 是否可编辑
        /// 存在批次的sku不可编辑
        /// </summary>
        public bool IsEditable { get; set; }

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

        /// <summary>
        /// 工厂型号，正常产品唯一
        /// </summary>
        public string FactoryModel { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public long CategoryId { get; set; }

        public long ProductId { get; set; }

        /// <summary>
        /// 分类对象
        /// </summary>
        public SkuCategory Category { get; set; }

        /// <summary>
        /// 完整的多级分类路径 
        /// 2-4
        /// </summary>
        public string CategoryIdPath { get; set; }

        /// <summary>
        /// 分类路径
        /// 前端用[2,4]
        /// </summary>
        public List<int> CategoryIds { get; set; }

        /// <summary>
        /// 功能分类Id
        /// </summary>
        public long FuncCategoryId { get; set; }

        /// <summary>
        /// 功能分类对象
        /// </summary>
        public ProductCategory FuncCategory { get; set; }


        /// <summary>
        /// 产品主图
        /// </summary>
        public string PrimaryImageSrc { get; set; }
        public string ImageMain { get; set; }

        /// <summary>
        /// SKU
        /// 12-52-0
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// UPC
        /// 853252006081 
        /// </summary>
        public string UPC { get; set; }

        /// <summary>
        /// 产品尺寸（inch）
        /// 长（深）  宽  高
        /// </summary>
        public string ProductSize { get; set; }

        /// <summary>
        /// 包装配置
        /// </summary>
        public string PackingConfig { get; set; }

        /// <summary>
        /// 包装尺寸（inch）
        /// </summary>
        public string PackingSize { get; set; }

        /// <summary>
        /// 净重（LB），目前小数点后1位
        /// </summary>
        public string NetWeight { get; set; }

        /// <summary>
        /// 毛重（LB），目前小数点后1位
        /// </summary>
        public string GrossWeight { get; set; }

        /// <summary>
        /// 认证
        /// </summary>
        public string Certification { get; set; }

        /// <summary>
        /// 1托数量
        /// qit/pallet
        /// </summary>
        public int QtyPallet { get; set; }

        /// <summary>
        /// 40HQ装柜数
        /// </summary>
        public string LoadingQty_40HQ { get; set; }

        /// <summary>
        /// 产品卖点
        ///Bullet Point
        /// </summary>
        public string BulletPoint { get; set; }

        public string EBulletPoint { get; set; }

        public string Remarks { get; set; }

        //public List<SkuPackageDetail> PackageList;
        public List<SkuComponentTreeDto> PartChildren;
        public List<SkuComponentTreeDto> FittingChildren;
        public List<SkuInfoCustom> CustomList;
        public List<long> FuncCategoryIds { get; set; }

    }
}
