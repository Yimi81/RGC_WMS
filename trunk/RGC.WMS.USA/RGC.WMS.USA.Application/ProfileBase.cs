using AutoMapper;
using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Application.Bidding.Dto;
using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Item.Dto;
using RGC.WMS.USA.Application.Order.Dto;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Application.Warehouse.Dto;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Entities.Bidding.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Item.Do;
using RGC.WMS.USA.Domain.Entities.Order;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Entities.Warehouse;

namespace RGC.WMS.USA.Application
{
    public class ProfileBase : Profile
    {
        public ProfileBase()
        {
            #region system
            CreateMap(typeof(BmsMenu), typeof(BmsMenuDto));
            CreateMap(typeof(BmsMenu), typeof(BmsMenuTreeDto));
            CreateMap(typeof(BmsMenuTreeDto), typeof(BmsMenu));
            CreateMap(typeof(BmsMenuDto), typeof(BmsMenu));
            CreateMap(typeof(BmsMenuTreeDo), typeof(BmsMenuTreeDto));
            CreateMap(typeof(BmsMenuTreeDto), typeof(BmsMenuTreeDo));
            CreateMap(typeof(BmsOrganization), typeof(BmsOrganizationDto));
            CreateMap(typeof(BmsOrganization), typeof(BmsOrganizationTreeDto));
            CreateMap(typeof(BmsOrganizationDto), typeof(BmsOrganization));
            CreateMap(typeof(BmsOrganizationTreeDto), typeof(BmsOrganization));
            CreateMap(typeof(BmsOrganizationCascaderDto), typeof(BmsOrganizationCascaderDo));
            CreateMap(typeof(BmsOrganizationCascaderDo), typeof(BmsOrganizationCascaderDto));
            #endregion

            #region Sku
            CreateMap(typeof(SkuInfo), typeof(SkuInfoDto));
            CreateMap(typeof(SkuPartsDetail), typeof(SkuPartsDetailDto));
            CreateMap(typeof(SkuInfo), typeof(SkuOutput));
            CreateMap(typeof(WarehouseInfo), typeof(SkuWarehouseOutput));
            CreateMap(typeof(SkuStock), typeof(SkuStockOutput));
            CreateMap(typeof(SkuCost), typeof(SkuCostOutput));
            #endregion

            #region 产品
            CreateMap(typeof(ProductInfo), typeof(ProductDto));
            CreateMap(typeof(ProductDto), typeof(ProductDo));
            CreateMap(typeof(ProductDo), typeof(ProductDto));
            CreateMap(typeof(ProductDo), typeof(ProductEditDto));
            CreateMap(typeof(ProductEditDto), typeof(ProductDo));
            CreateMap(typeof(ProductEditDto), typeof(ProductEditDo));
            CreateMap(typeof(ProductEditDo), typeof(ProductEditDto));
            CreateMap(typeof(ProductEditDo), typeof(ProductModifyFlow));
            CreateMap(typeof(ProductModifyFlow), typeof(ProductEditDo));
            CreateMap(typeof(ProductModifyFlow), typeof(ProductInfo));
            CreateMap(typeof(ProductInfo), typeof(ProductModifyFlow));
            CreateMap(typeof(ProductEditDto), typeof(ProductInfo));
            CreateMap(typeof(ProductInfo), typeof(ProductEditDto));
            CreateMap(typeof(ProductDto), typeof(ProductInfo));
            CreateMap(typeof(ProductCategory), typeof(ProductCategoryDto));
            CreateMap(typeof(ProductCategoryDto), typeof(ProductCategory));
            CreateMap(typeof(ProductComponentTreeDo), typeof(ProductComponentTreeDto));
            CreateMap(typeof(ProductPartsDetail), typeof(ProductPartsDetailDto));
            CreateMap(typeof(ProductPartsDetailDto), typeof(ProductPartsDetail));
            CreateMap(typeof(ProductComponentTreeDto), typeof(ProductComponentTreeDo));
            CreateMap(typeof(ProductConfig), typeof(ProductConfigDto));
            CreateMap(typeof(ProductConfigDto), typeof(ProductConfig));
            CreateMap(typeof(ProductConfig), typeof(ProductConfigEditDto));
            CreateMap(typeof(ProductConfigEditDto), typeof(ProductConfig));
            #endregion

            #region 平台产品
            CreateMap(typeof(ItemInfo), typeof(ItemDto));
            CreateMap(typeof(ItemInfo), typeof(ItemModifyFlow));
            CreateMap(typeof(ItemModifyFlow), typeof(ItemInfo));
            CreateMap(typeof(ItemInfo), typeof(ItemEditDto));
            CreateMap(typeof(ItemEditDto), typeof(ItemInfo));
            CreateMap(typeof(ItemEditDo), typeof(ItemEditDto));
            CreateMap(typeof(ItemEditDto), typeof(ItemEditDo));
            CreateMap(typeof(ItemDto), typeof(ItemInfo));
            CreateMap(typeof(ItemInfo), typeof(v_item));
            CreateMap(typeof(v_item), typeof(ItemInfo));
            CreateMap(typeof(v_item), typeof(ItemEditDto));
            CreateMap(typeof(ItemEditDto), typeof(v_item));
            CreateMap(typeof(ItemPriceRecord), typeof(ItemPriceRecordDto));
            CreateMap(typeof(ItemPriceRecordDto), typeof(ItemPriceRecord));
            CreateMap(typeof(ItemEditDto), typeof(ItemModifyFlow));
            CreateMap(typeof(ItemModifyFlow), typeof(ItemEditDto));
            CreateMap(typeof(ItemEditDo), typeof(ItemModifyFlow));
            CreateMap(typeof(ItemModifyFlow), typeof(ItemEditDo));
            CreateMap(typeof(CompetitionDto), typeof(Competition));
            CreateMap(typeof(Competition), typeof(CompetitionDto));
            CreateMap(typeof(CompetitionEditDto), typeof(CompetitionEditDo));
            CreateMap(typeof(CompetitionEditDo), typeof(CompetitionEditDto));
            #endregion

            #region 仓库
            //数据绑定 by Meridian 2020/06/02
            CreateMap(typeof(WarehouseInfo), typeof(WarehouseDto));
            CreateMap(typeof(WarehouseEditInput), typeof(WarehouseInfo));
            #endregion

            #region 订单
            CreateMap(typeof(SaleOrderDto), typeof(SaleOrder));
            CreateMap(typeof(SaleOrderItemDto), typeof(SaleOrderItem));
            #endregion

        }
    }
}
