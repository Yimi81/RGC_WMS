#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Warehouse\StockInShow.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bfd1e222e81a602b7bac42619e03e9a49d208392"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Warehouse_StockInShow), @"mvc.1.0.view", @"/Views/Warehouse/StockInShow.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\_ViewImports.cshtml"
using RGC.WMS.USA;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\_ViewImports.cshtml"
using RGC.WMS.USA.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bfd1e222e81a602b7bac42619e03e9a49d208392", @"/Views/Warehouse/StockInShow.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Warehouse_StockInShow : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("plr20"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Warehouse\StockInShow.cshtml"
  
    ViewData["Title"] = "StockInShow";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<link rel=""stylesheet"" href=""/lib/elFinder/lib/jquery-ui/themes/base/all.css"" />
<link rel=""stylesheet"" href=""/lib/elFinder/lib/elfinder/css/elfinder.full.css"" />
<link rel=""stylesheet"" href=""/lib/elFinder/lib/elfinder/css/theme.css"" />
<style>
    .avatar {
        width: 200px;
        height: 200px;
        display: block;
    }

    .el-card {
        border: none;
    }

    .el-card__header {
        border-bottom: none;
        padding-left: 0;
    }

    .el-card__body {
        padding-left: 0;
    }

    .hide-empty .el-table__empty-block {
        display: none;
    }

    .search {
        margin-bottom: 5px;
    }
</style>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bfd1e222e81a602b7bac42619e03e9a49d2083924385", async() => {
                WriteLiteral(@"
    <div id=""wrapper"">
        <!-- 标题和地址 -->
        <el-row>
            <el-col :span=""24"" class=""breadcrumb-container"">
                <el-breadcrumb separator=""/"">
                    <el-breadcrumb-item><strong>库存管理</strong></el-breadcrumb-item>
                    <el-breadcrumb-item>查看入库单</el-breadcrumb-item>
                </el-breadcrumb>
            </el-col>
        </el-row>
        <template>
            <article class=""administration-maintenance-container"" v-loading=""listLoading"">
                <div ref=""nav_sku"" id=""nav_sku"">
                    <el-form :inline=""true"" :model=""tableEditStockIn.editData"" ref=""stockInFrom"" label-width=""300px"" class=""stockInFrom"" label-position=""right"" size=""mini"">
                        <el-card class=""box-card"" shadow=""none"">
                            <span slot=""header"" class=""step-jump pr"" id=""sku_config"">
                                <i class=""tip-tit""><i class=""pr z-in2 pl10"">入库详情</i></i>
                            </span>
   ");
                WriteLiteral(@"                         <el-table :data=""tableEditStockIn.editData.stockInList""
                                      :span-method=""objectSpanMethod""
                                      min-height=""250""
                                      style=""width: 100%;margin-bottom:0px;""
                                      border
                                      class=""gvq hide-empty mt20""
                                      size=""mini"">
                                <el-table-column label=""序号"" prop=""detailId"" width=""45"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.$index + 1}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""仓库"" prop=""warehouseNumber"" width=""45"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.ro");
                WriteLiteral(@"w.warehouseNumber}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""合同号"" prop=""contractNo"" width=""120"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.row.contractNo}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""箱号"" prop=""containerNo"" width=""120"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.row.containerNo}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""产品型号"" prop=""factoryModel"" width=""150"" align=""center"">
                                    <template slot-scope=""scope"">
                     ");
                WriteLiteral(@"                   <i>{{scope.row.factoryModel}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""数量"" prop=""qty"" width=""60"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.row.qty}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""ETD"" prop=""etd"" width=""90"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.row.etd}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""ETA"" prop=""eta"" width=""90"" align=""center"">
                                    <template slot-scope=""scope"">
                                   ");
                WriteLiteral(@"     <i>{{scope.row.eta}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""实际到港日期"" prop=""ataPort"" width=""155"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.row.ataPort}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""实际到仓日期"" prop=""ataWarehouse"" width=""155"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.row.ataWarehouse}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""实际型号"" prop=""actFactoryModel"" width=""120"" align=""center"">
                                    <template slot-scope=""scope"">
         ");
                WriteLiteral(@"                               <i>{{scope.row.actFactoryModel}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""实际数量"" prop=""actInQty"" width=""80"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.row.actInQty}}</i>
                                    </template>
                                </el-table-column>
                                <el-table-column label=""延迟原因"" prop=""reason"" align=""center"">
                                    <template slot-scope=""scope"">
                                        <i>{{scope.row.reason}}</i>
                                    </template>
                                </el-table-column>
                            </el-table>
                        </el-card>

                        <!-- 其它信息 -->
                        <el-card class=""box-card"" shadow=""non");
                WriteLiteral(@"e"">
                            <span slot=""header"" class=""step-jump pr"" id=""sku_config"">
                                <i class=""tip-tit""><i class=""pr z-in2 pl10"">其它信息</i></i>
                            </span>
                            <el-row>
                                <el-form-item label=""备注"" prop=""remarks"">
                                    <i>{{tableEditStockIn.editData.remark}}</i>
                                </el-form-item>
                            </el-row>
                        </el-card>
                    </el-form>
                </div>
            </article>
        </template>
    </div>
    <script src=""/lib/elFinder/lib/jquery-ui/jquery-ui.min.js""></script>
    <script src=""/lib/elFinder/lib/elfinder/js/elfinder.full.js""></script>
    <script src=""/lib/elFinder/lib/elfinder/js/i18n/elfinder.zh_CN.js""></script>
    <script>
        var Main = {
            data() {
                return {
                    mergeArr: {},
                    merg");
                WriteLiteral(@"eRowFields: [""warehouseNumber"", ""contractNo"", ""containerNo"", ""ataPort"", ""ataWarehouse""],

                    tableEditStockIn: {
                        editData: {
                            id: 0,
                            stockInNum: '',
                            remark: '',
                            stockInList: []
                        }
                    },
                    listLoading: false
                }
            },
            created() {
                var stockInId = getQueryString(""stockInId"");
                if (stockInId && parseInt(stockInId) > 0) {
                    this.tableEditStockIn.editData.id = parseInt(stockInId);
                    this.getStockIn();
                }
            },
            methods: {
                errorException(XMLHttpRequest, textStatus, errorThrown) {
                    if (XMLHttpRequest &&
                        XMLHttpRequest.responseJSON &&
                        errorThrown == ""Unauthorized"") {
    ");
                WriteLiteral(@"                    window.parent.MainIndex.openLoginFrom();
                        this.$message({
                            type: 'error',
                            message: XMLHttpRequest.responseJSON.msg
                        });
                    } else {
                        this.$message({
                            type: 'error',
                            message: errorThrown
                        });
                    }
                },

                getStockIn() {
                    var $this = this;
                    $.ajax({
                        type: 'GET',
                        url: '/rest/warehouse/stock/in/get/' + $this.tableEditStockIn.editData.id,
                        success: function (data) {
                            if (data.success == true) {
                                var row = data.data;
                                $this.tableEditStockIn.editData.stockInNum = row.stockInNum;
                                $this.table");
                WriteLiteral(@"EditStockIn.editData.warehouseId = row.warehouseId;
                                $this.tableEditStockIn.editData.remark = row.remark;

                                $this.tableEditStockIn.editData.stockInList = [];
                                row.primaryId = row.id;
                                var stockInDetail = row.detail;
                                delete row.id;
                                delete row.detail;

                                if (stockInDetail) {
                                    for (let i = 0; i < stockInDetail.length; i++) {
                                        let newPush = $.extend(row, stockInDetail[i]);
                                        $this.tableEditStockIn.editData.stockInList.push({
                                            id: newPush.id,
                                            primaryId: newPush.packingId,
                                            detailId: newPush.packingDetailId,

                                      ");
                WriteLiteral(@"      sourceWarehouseId: 0,
                                            sourceProductId: 0,
                                            sourceSkuId: 0,
                                            sourceSkuCostId: 0,
                                            sourceSkuCostBatchId: 0,
                                            sourceSkuStockId: 0,

                                            warehouseNumber: newPush.warehouse.number,
                                            contractNo: newPush.packingList.contractNo,
                                            containerNo: newPush.packingDetail.containerNo,
                                            factoryModel: newPush.sku.factoryModel,
                                            qty: newPush.planInQty,
                                            etd: newPush.etd,
                                            eta: newPush.eta,

                                            targetProductId: newPush.productId,
                                 ");
                WriteLiteral(@"           targetSkuId: newPush.skuId,
                                            targetSkuCostId: newPush.skuCostId,
                                            targetSkuCostBatchId: newPush.skuCostBatchId,
                                            targetSkuStockId: 0,

                                            ataPort: newPush.ataPort,
                                            ataWarehouse: newPush.ataWarehouse,
                                            actFactoryModel: newPush.actFactoryModel,
                                            actInQty: parseInt(newPush.actInQty),
                                            reason: newPush.reason
                                        });
                                    }
                                }
                                $this.merge();
                            } else {
                                $this.$notify.error({
                                    title: '错误',
                                    message:");
                WriteLiteral(@" data.msg
                                });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            $this.errorException(XMLHttpRequest, textStatus, errorThrown);
                        }
                    });
                },
                //合并的方法
                objectSpanMethod({ row, column, rowIndex, columnIndex }) {
                    if (this.mergeRowFields.indexOf(column.property) >= 0) {
                        const _row = this.mergeArr[column.property].array[rowIndex];
                        const _col = _row > 0 ? 1 : 0;
                        return {
                            rowspan: _row,
                            colspan: _col
                        };
                    }
                },
                merge() {
                    this.mergeArr = {};
                    let tableData = this.tableEditStockIn.editData.stockInList;
      ");
                WriteLiteral(@"              if (tableData && tableData.length > 0) {
                        for (let i = 0; i < tableData.length; i++) {
                            if (i === 0) {
                                for (let i = 0; i < this.mergeRowFields.length; i++) {
                                    this.mergeArr[this.mergeRowFields[i]] = {
                                        array: [1],
                                        position: 0
                                    };
                                }
                            }
                            else {
                                for (let sPropertyName in tableData[i]) {
                                    if (this.mergeRowFields.indexOf(sPropertyName) >= 0) {
                                        // 判断当前元素与上一个元素是否相同(第1和第2列)
                                        if (tableData[i][sPropertyName] === tableData[i - 1][sPropertyName]) {
                                            this.mergeArr[sPropertyName].array[this.mergeAr");
                WriteLiteral(@"r[sPropertyName].position] += 1;
                                            this.mergeArr[sPropertyName].array.push(0);
                                        } else {
                                            this.mergeArr[sPropertyName].array.push(1);
                                            this.mergeArr[sPropertyName].position = i;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
        var Ctor = Vue.extend(Main);
        new Ctor().$mount('#wrapper');

        function getQueryString(name) {
            var reg = new RegExp(""(^|&)"" + name + ""=([^&]*)(&|$)"", ""i"");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
