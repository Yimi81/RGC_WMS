﻿using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Entities.Product.Do
{
    public class ProductDo : FullEntity
    {
        public ProductDo()
        {
            FuncCategoryIds = new List<long>();
            PartChildren = new List<ProductComponentTreeDo>();
            FittingChildren = new List<ProductComponentTreeDo>();
            SkuList = new List<SkuInfo>();
            product = new ProductInfo();
        }

        public ProductInfo product { get; set; }

        /// <summary>
        /// SKU状态
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// SKU
        /// 12-52-0
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// UPC
        /// </summary>
        public string UPC { get; set; }

        /// <summary>
        /// 认证
        /// </summary>
        public string Certification { get; set; }

        #region 产品中英文名称

        public long Id { get; set; }

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

        #endregion

        /// <summary>
        /// 工厂型号，正常产品唯一
        /// </summary>
        public string FactoryModel { get; set; }

        /// <summary>
        /// 功能分类Id
        /// </summary>
        public long FuncCategoryId { get; set; }

        public List<long> FuncCategoryIds { get; set; }

        /// <summary>
        /// 功能分类对象
        /// </summary>
        public ProductCategory FuncCategory { get; set; }


        /// <summary>
        /// 产品尺寸（inch）
        /// 长（深）  宽  高
        /// </summary>
        public string ProductSize { get; set; }

        /// <summary>
        /// 包装尺寸（inch）
        /// </summary>
        public string PackingSize { get; set; }


        /// <summary>
        /// 净重（LB），目前小数点后1位
        /// </summary>
        public decimal NetWeight { get; set; }

        /// <summary>
        /// 毛重（LB），目前小数点后1位
        /// </summary>
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// 产品主图
        /// </summary>
        public string PrimaryImageSrc { get; set; }

        public string PrimaryImageSrcFull { get; set; }

        /// <summary>
        /// 包装配置
        /// </summary>
        public string PackingConfig { get; set; }


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

        /// <summary>
        /// 后台备注
        /// </summary>
        public string Remarks { get; set; }
        public string MapString { get; set; }
        public int Map { get; set; }
        public string MsrpString { get; set; }
        public int Msrp { get; set; }

        public string ImageMain { get; set; }
        public string CreationTimeString { get; set; }
        public string ModifyTimeString { get; set; }
        public string CreateUser { get; set; }

        public List<ProductComponentTreeDo> PartChildren;
        public List<ProductComponentTreeDo> FittingChildren;
        public List<SkuInfo> SkuList;
        public List<ProductComponentTreeDo> FittingChildrenLeft;
        public List<ProductComponentTreeDo> FittingChildrenRight;
        public List<ProductInfoCustom> CustomList;
    }
}
