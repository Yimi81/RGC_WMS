using HuigeTec.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Order;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Entities.System;
using RGC.WMS.USA.Domain.Entities.Purchase;

namespace RGC.WMS.USA.Data
{
    public class WMSDbContext : DbContext
    {
        public WMSDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BmsMenu> BmsMenus { get; set; }
        public DbSet<BmsUserExtend> BmsUsers { get; set; }
        public DbSet<BmsDataRecycle> BmsDataRecycles { get; set; }
        public DbSet<CompanyStaff> CompanyStaff { get; set; }
        public DbSet<CompanyDepartment> CompanyDepartment { get; set; }
        public DbSet<CompanyDepartmentStaff> CompanyDepartmentStaff { get; set; }
        public DbSet<BmsUserLoginAttempt> BmsUserLoginAttempt { get; set; }
        public DbSet<BmsUserMenuExtend> BmsUserMenu { get; set; }
        public DbSet<BmsOrganization> Organization { get; set; }
        public DbSet<BmsOrganizationMenu> OrganizationMenu { get; set; }
        public DbSet<BmsUserOrganization> UserOrganization { get; set; }
        public DbSet<BmsRole> RoleInfo { get; set; }
        public DbSet<BmsUserRole> UserRole { get; set; }


        public DbSet<SystemInfo> SystemInfo { get; set; }
        public DbSet<BmsUserSystem> UserSystem { get; set; }
        public DbSet<PlatformInfo> PlatformInfo { get; set; }
        public DbSet<BmsUserPlatform> UserPlatform { get; set; }
        public DbSet<VSyncFlow> VSyncFlow { get; set; }
        public DbSet<SyncConfig> SyncConfig { get; set; }
        public DbSet<State> States { get; set; }


        #region 产品
        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<ProductPackageDetail> ProductPackageDetail { get; set; }
        public DbSet<ProductPartsDetail> ProductPartsDetail { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductPartsDetailEx> ProductPartsDetailEx { get; set; }
        public DbSet<ProductInfoCustom> ProductInfoCustom { get; set; }
        public DbSet<ProductConfig> ProductConfig { get; set; }
        public DbSet<ProductConfigDetail> ProductConfigDetail { get; set; }
        public DbSet<ProductModifyFlow> ProductModifyFlow { get; set; }
        #endregion

        #region Sku
        public DbSet<SkuInfo> SkuInfo { get; set; }
        public DbSet<SkuPackageDetail> SkuPackageDetail { get; set; }
        public DbSet<SkuPartsDetail> SkuPartsDetail { get; set; }
        public DbSet<SkuCategory> SkuCategory { get; set; }
        public DbSet<SkuPartsDetailEx> SkuPartsDetailEx { get; set; }
        public DbSet<SkuInfoCustom> SkuInfoCustom { get; set; }
        public DbSet<SkuConfiguration> SkuConfiguration { get; set; }
        public DbSet<SkuConfigurationDetail> SkuConfigurationDetail { get; set; }
        public DbSet<SkuStock> SkuStock { get; set; }
        public DbSet<SkuCost> SkuCost { get; set; }
        public DbSet<SkuCostBatch> SkuCostBatch { get; set; }
        #endregion

        #region 在售平台产品
        public DbSet<ItemInfo> ItemInfo { get; set; }
        public DbSet<Competition> Competition { get; set; }
        public DbSet<CompetitionDaily> CompetitionDaily { get; set; }
        public DbSet<v_item> ItemView { get; set; }
        public DbSet<ItemProduct> ItemProduct { get; set; }

        public DbSet<ItemPriceRecord> ItemPriceRecord { get; set; }

        public DbSet<ItemPriceNotice> ItemPriceNotice { get; set; }

        public DbSet<ItemDailyPrice> ItemDailyPrice { get; set; }
        public DbSet<ItemDailyReview> ItemDailyReview { get; set; }
        public DbSet<ItemModifyFlow> ItemModifyFlow { get; set; }

        #endregion

        //order↓
        public DbSet<SSOrder> Orders { get; set; }
        public DbSet<SSOrderItem> OrderItems { get; set; }

        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderItem> SaleOrderItems { get; set; }

        #region 采购管理        
        public DbSet<PackingListInfo> PackingListInfo { get; set; }
        public DbSet<PackingListDetail> PackingListDetail { get; set; }
        
        #endregion

        #region 仓库
        public DbSet<WarehouseInfo> WarehouseInfo { get; set; }
        public DbSet<StockIn> StockIn { get; set; }
        public DbSet<StockInDetail> StockInDetail { get; set; }
        public DbSet<StockOut> StockOut { get; set; }
        public DbSet<StockOutDetail> StockOutDetail { get; set; }
        #endregion

    }
}
