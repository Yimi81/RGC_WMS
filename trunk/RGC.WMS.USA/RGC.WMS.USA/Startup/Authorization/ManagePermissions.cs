using System.Collections.Generic;

namespace RGC.WMS.USA.Authorization
{
    public class ManagePermissions
    {
        public static List<ControllerPermissions> GetPermissions()
        {
            return new List<ControllerPermissions>()
            {
                //用户
                new ControllerPermissions("BmsUser","CreateUser",AuthorizeCode.User_Add),
                new ControllerPermissions("BmsUser","GetUserDetail",AuthorizeCode.User_Modify),
                new ControllerPermissions("BmsUser","GetUserList",AuthorizeCode.User_List),

                //菜单
                new ControllerPermissions("Administration","Menu",AuthorizeCode.Menu),
                new ControllerPermissions("BmsMenu","DetailGet",AuthorizeCode.Menu),
                new ControllerPermissions("BmsMenu","Create",AuthorizeCode.Menu_Add),
                new ControllerPermissions("BmsMenu","Modify",AuthorizeCode.Menu_Modify),
                new ControllerPermissions("BmsMenu","Delete",AuthorizeCode.Menu_Delete),
                new ControllerPermissions("BmsMenu","TreeGet",AuthorizeCode.Menu),
                new ControllerPermissions("BmsMenu","DetailGet",AuthorizeCode.Menu),
                new ControllerPermissions("BmsMenu","ParentGet",AuthorizeCode.Menu),
                new ControllerPermissions("BmsMenu","ChildrenGet",AuthorizeCode.Menu),

                //Sku
                new ControllerPermissions("Sku","Recovery",AuthorizeCode.Sku_Recycle_Recover),
                new ControllerPermissions("Sku","Recycle",AuthorizeCode.Sku_Recycle),
                new ControllerPermissions("Sku","RecycleQuery",AuthorizeCode.Sku_Recycle_List),
                new ControllerPermissions("Sku","SkuList",AuthorizeCode.Sku_List),

                new ControllerPermissions("Sku","SkuCostList",AuthorizeCode.Sku_Cost_List),
                new ControllerPermissions("Sku","SkuCostDetail",AuthorizeCode.Sku_Cost_Detail),
                new ControllerPermissions("Sku","GetSkuCostBatchList",AuthorizeCode.Sku_Cost_List),
                new ControllerPermissions("Sku","SkuCostCreate",AuthorizeCode.Sku_Cost_Add),
                new ControllerPermissions("Sku","SkuCostUpdate",AuthorizeCode.Sku_Cost_Modify),
                new ControllerPermissions("Sku","SkuCostDelete",AuthorizeCode.Sku_Cost_Delete),
                new ControllerPermissions("Sku","SkuCostBatchAdd",AuthorizeCode.Sku_Cost_Add),
                new ControllerPermissions("Sku","SkuCostBatchStatusUpdate",AuthorizeCode.Sku_Cost_Modify),
                new ControllerPermissions("Sku","GetSkuCostBatchPageList",AuthorizeCode.Sku_Cost_List),

                new ControllerPermissions("Sku","SkuStockList",AuthorizeCode.Sku_Stock_List),
                new ControllerPermissions("Sku","GetSkuStock",AuthorizeCode.Sku_Stock_Detail),
                new ControllerPermissions("Sku","CreateStock",AuthorizeCode.Sku_Stock_Add),
                new ControllerPermissions("Sku","DeleteStock",AuthorizeCode.Sku_Stock_Delete),
                new ControllerPermissions("Sku","UpdateStock",AuthorizeCode.Sku_Stock_Modify),

                //产品
                new ControllerPermissions("Product","Create",AuthorizeCode.Product_Add),
                new ControllerPermissions("Product","Delete",AuthorizeCode.Product_Delete),
                new ControllerPermissions("Product","Recovery",AuthorizeCode.Product_Recycle_Recover),
                new ControllerPermissions("Product","Update",AuthorizeCode.Product_Modify),
                new ControllerPermissions("Product","EditGetModel",AuthorizeCode.Product_Detail,AuthorizeCode.Product_Modify),
                new ControllerPermissions("Product","GetProductList",AuthorizeCode.Product_List),
                new ControllerPermissions("Product","UpdateStatus",AuthorizeCode.Product_Status),

                new ControllerPermissions("Product","Recycle",AuthorizeCode.Product_Recycle),
                new ControllerPermissions("Product","RecycleQuery",AuthorizeCode.Product_Recycle_List),

                new ControllerPermissions("Product","AddCategory",AuthorizeCode.Product_Category),
                new ControllerPermissions("Product","GetCategoryProductList",AuthorizeCode.Product_Category),
                new ControllerPermissions("Product","GetOtherCategoryProductList",AuthorizeCode.Product_Category),
                               
                //产品配置
                new ControllerPermissions("Product","GetPackage",AuthorizeCode.Product_Config),
                new ControllerPermissions("Product","GetParts",AuthorizeCode.Product_Config),
                new ControllerPermissions("Product","GetPartsList",AuthorizeCode.Product_Config),
                new ControllerPermissions("Product","GetPartsEdit",AuthorizeCode.Product_Config),
                new ControllerPermissions("Product","GetFittingList",AuthorizeCode.Product_Config),
                new ControllerPermissions("Product","GetFittingDetail",AuthorizeCode.Product_Config),
                 
                //产品分类
                new ControllerPermissions("Product","Category",AuthorizeCode.Product_Category),
                new ControllerPermissions("Category","Create",AuthorizeCode.Product_Category),
                new ControllerPermissions("Category","Create",AuthorizeCode.Product_Category_Add),
                new ControllerPermissions("Category","Delete",AuthorizeCode.Product_Category_Delete),
                new ControllerPermissions("Category","Update",AuthorizeCode.Product_Category_Modify),
                new ControllerPermissions("Category","List",AuthorizeCode.Product_Category_List),
                new ControllerPermissions("Category","ProductCascader",AuthorizeCode.Product_Category,AuthorizeCode.Product_Add,AuthorizeCode.Product_Modify),
                
                //平台产品

                //仓库
                new ControllerPermissions("Warehouse","Create",AuthorizeCode.Warehouse_Add),
                new ControllerPermissions("Warehouse","Delete",AuthorizeCode.Warehouse_Delete),
                new ControllerPermissions("Warehouse","Recovery",AuthorizeCode.Warehouse_Recovery),
                new ControllerPermissions("Warehouse","Update",AuthorizeCode.Warehouse_Modify),
                new ControllerPermissions("Warehouse","Get",AuthorizeCode.Warehouse_Detail,AuthorizeCode.Warehouse_Modify),
                new ControllerPermissions("Warehouse","GetPage",AuthorizeCode.Warehouse_List),
                new ControllerPermissions("Warehouse","UpdateStatus",AuthorizeCode.Warehouse_Status),

                new ControllerPermissions("Warehouse","StockInCreate",AuthorizeCode.Warehouse_StockIn_Add),
                new ControllerPermissions("Warehouse","StockInDelete",AuthorizeCode.Warehouse_StockIn_Delete),
                new ControllerPermissions("Warehouse","StockInRecovery",AuthorizeCode.Warehouse_StockIn_Recovery),
                new ControllerPermissions("Warehouse","StockInUpdate",AuthorizeCode.Warehouse_StockIn_Modify),
                new ControllerPermissions("Warehouse","GetStockIn",AuthorizeCode.Warehouse_StockIn_Detail,AuthorizeCode.Warehouse_StockIn_Modify),
                new ControllerPermissions("Warehouse","GetStockInPage",AuthorizeCode.Warehouse_StockIn_List),
                new ControllerPermissions("Warehouse","UpdateStockInStatus",AuthorizeCode.Warehouse_StockIn_Status),

                new ControllerPermissions("Warehouse","StockOutCreate",AuthorizeCode.Warehouse_StockOut_Add),
                new ControllerPermissions("Warehouse","StockOutDelete",AuthorizeCode.Warehouse_StockOut_Delete),
                new ControllerPermissions("Warehouse","StockOutRecovery",AuthorizeCode.Warehouse_StockOut_Recovery),
                new ControllerPermissions("Warehouse","StockOutUpdate",AuthorizeCode.Warehouse_StockOut_Modify),
                new ControllerPermissions("Warehouse","GetStockOut",AuthorizeCode.Warehouse_StockOut_Detail,AuthorizeCode.Warehouse_StockOut_Modify),
                new ControllerPermissions("Warehouse","GetStockOutPage",AuthorizeCode.Warehouse_StockOut_List),
                new ControllerPermissions("Warehouse","UpdateStockOutStatus",AuthorizeCode.Warehouse_StockOut_Status),
            };
        }
    }

    public class ControllerPermissions
    {
        public ControllerPermissions(string controllerName, string actionName, params string[] authorizeCodes)
        {
            ControllerName = controllerName;
            ActionName = actionName;
            AuthorizeCodes = authorizeCodes;
        }
        public string ControllerName { get; set; }
        public string[] AuthorizeCodes { get; set; }
        public string ActionName { get; set; }
    }
}
