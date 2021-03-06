#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\SkuStock.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "83fd26eff4a23d232eb7a79b2416759eb34ee4b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sku_SkuStock), @"mvc.1.0.view", @"/Views/Sku/SkuStock.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83fd26eff4a23d232eb7a79b2416759eb34ee4b7", @"/Views/Sku/SkuStock.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Sku_SkuStock : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 2 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\SkuStock.cshtml"
  
    /**/

    ViewBag.Title = "List";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "83fd26eff4a23d232eb7a79b2416759eb34ee4b73646", async() => {
                WriteLiteral(@"
    <div id=""skustocklist"">
        <!-- 标题和地址 -->
        <el-row>
            <el-col :span=""24"" class=""breadcrumb-container"">
                <el-breadcrumb separator=""/"">
                    <el-breadcrumb-item><strong>产品管理</strong></el-breadcrumb-item>
                    <el-breadcrumb-item>Sku库存列表</el-breadcrumb-item>
                </el-breadcrumb>
            </el-col>
        </el-row>
        <article class=""administration-tenant-container"">
            <!--搜索-->
            <el-col :span=""24"" class=""toolbar"">
                <el-form :inline=""true"" size=""small"" :model=""filters"" v-on:submit.native.prevent>
                    <el-form-item>
                        <el-input placeholder=""搜索"" v-model=""filters.searchKey""
                                  v-on:keyup.enter.native=""getSkuStockList()"" clearable></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button v-on:click=""search"" icon=""el-icon-search"">搜索</el-button>");
                WriteLiteral(@"
                        <el-button v-on:click=""add"" icon=""el-icon-plus"" type=""primary"">添加Sku库存</el-button>
                    </el-form-item>
                </el-form>
            </el-col>
            <el-table class=""data-table"" v-loading=""listLoading"" :data=""skuStockList"" :fit=""true"" border size=""small"">
                <el-table-column min-width=""40"" prop=""id"" label=""ID"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.id}}</i>
                    </template>
                </el-table-column>

                <el-table-column min-width=""200"" prop=""fullEName"" label=""产品名"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.sku.fullEName}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""150"" prop=""factoryModel"" label=""工厂型号"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.sku.factoryModel}}</i>
       ");
                WriteLiteral(@"             </template>
                </el-table-column>
                <el-table-column width=""120"" label=""图片"" align=""center"">
                    <template slot-scope=""scope"">
                        <img v-if=""scope.row.sku.primaryImageSrcFull"" class=""pimg"" alt=""no image"" :src=""scope.row.sku.primaryImageSrcFull"" height=""50"">
                    </template>
                </el-table-column>
                <el-table-column min-width=""100"" prop=""batchNo"" label=""批次号"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.batchNo}}</i>
                    </template>
                </el-table-column>
                <el-table-column prop=""WarehouseId"" label=""仓库"" width=""90"">
                    <template slot-scope=""scope"">
                        <el-tag size=""small"" v-if=""scope.row.warehouseId > 0"">{{scope.row.warehouse.number}}</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.warehouseId <= 0"">未设置</el-tag>
                    </tem");
                WriteLiteral(@"plate>
                </el-table-column>
                <el-table-column min-width=""80"" prop=""currentStock"" label=""现货库存"">
                </el-table-column>
                <el-table-column min-width=""90"" prop=""lockStock"" label=""不可售库存"">
                </el-table-column>
                <el-table-column min-width=""80"" prop=""onWayStock"" label=""在途库存"">
                </el-table-column>
                <el-table-column min-width=""80"" prop=""orderStock"" label=""预占库存"">
                </el-table-column>
                <el-table-column min-width=""70"" prop=""safeStock"" label=""安全库存"">
                </el-table-column>
                <el-table-column min-width=""50"" prop=""lowStock"" label=""低库存"">
                </el-table-column>
                <el-table-column min-width=""130"" prop=""lastModificationTimeString"" label=""上次修改时间"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.lastModificationTimeString}}</i>
                    </template>
                </el-ta");
                WriteLiteral(@"ble-column>
                <el-table-column min-width=""60"" prop=""creatorUser"" label=""创建人"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.creatorUser}}</i>
                    </template>
                </el-table-column>

                <el-table-column width=""100"" label=""操作"">
                    <template slot-scope=""scope"">
                        <el-dropdown trigger=""click"">
                            <el-button type=""primary"" size=""mini"" class=""mb5"">
                                操作
                                <i class=""el-icon-caret-bottom el-icon--right""></i>
                            </el-button>
                            <el-dropdown-menu slot=""dropdown"">
                                <el-dropdown-item>
                                    <div v-on:click=""edit(scope.row)"">修 改</div>
                                </el-dropdown-item>
");
                WriteLiteral(@"
                                <el-dropdown-item>
                                    <div v-on:click=""del(scope.$index, scope.row.id)"">删 除</div>
                                </el-dropdown-item>
                            </el-dropdown-menu>
                        </el-dropdown>
");
                WriteLiteral(@"
                    </template>
                </el-table-column>
            </el-table>
            <el-col :span=""24"" class=""toolbar"">
                <el-pagination v-on:size-change=""handleSizeChange"" v-on:current-change=""handleCurrentChange"" :current-page=""page"" :page-sizes=""[10,20,30,40,50]"" :page-size=""pageSize"" layout=""total, sizes, prev, pager, next, jumper"" :total=""total"" style=""float:right;"">
                </el-pagination>
            </el-col>
        </article>


        <el-dialog title=""编辑库存"" :visible.sync=""editSkuStockVisible"" size=""small"" :close-on-click-modal=""false"" top=""5%"" width=""50%"">
            <el-form :model=""skuStockForm"" size=""small"" label-width=""150px"" ref=""editForm"" inline>
");
                WriteLiteral(@"                <el-form-item label=""产品型号"">
                    {{skuStockForm.skuFactoryModel}}
                </el-form-item><br />
                <el-form-item label=""批次号"">
                    {{skuStockForm.batchNo}}
                </el-form-item><br />
                <el-form-item label=""所在仓库"" v-if=""skuStockForm.warehouseId <= 0"">
                    未设置
                </el-form-item>
                <el-form-item label=""所在仓库"" v-if=""skuStockForm.warehouseId > 0"">
                    {{skuStockForm.warehouse.number}}
                </el-form-item>
                <br />
                <el-form-item label=""现货库存"" prop=""currentStock"">
                    <el-input-number v-model=""skuStockForm.currentStock"" :controls=""false"" :precision=""0"" class=""w178""></el-input-number>
                </el-form-item>
                <el-form-item label=""锁定库存"" prop=""lockStock"">
                    <el-input-number v-model=""skuStockForm.lockStock"" :controls=""false"" :precision=""0"" class=""w178""></el-inpu");
                WriteLiteral(@"t-number>
                </el-form-item>
                <el-form-item label=""安全库存"" prop=""safeStock"">
                    <el-input-number v-model=""skuStockForm.safeStock"" :controls=""false"" :precision=""0"" class=""w178""></el-input-number>
                </el-form-item>
                <el-form-item label=""低库存"" prop=""lowStock"">
                    <el-input-number v-model=""skuStockForm.lowStock"" :controls=""false"" :precision=""0"" class=""w178""></el-input-number>
                </el-form-item>
                <el-form-item label=""在途库存"" prop=""onWayStock"">
                    <el-input-number v-model=""skuStockForm.onWayStock"" :controls=""false"" :precision=""0"" class=""w178""></el-input-number>
                </el-form-item>
");
                WriteLiteral(@"            </el-form>
            <div slot=""footer"" class=""dialog-footer"">
                <el-button size=""small"" type=""primary"" v-on:click=""saveEdit()"">保 存</el-button>
            </div>
        </el-dialog>

        <el-dialog title=""添加库存"" :visible.sync=""addSkuStockVisible"" :close-on-click-modal=""false"" top=""5%"" width=""50%"">
            <el-form :model=""addSkuStockForm"" size=""small"" label-width=""150px"" ref=""editForm"" inline>
                <el-form-item label=""选择产品"" prop=""skuId"">
                    <el-select v-model=""addSkuStockForm.skuId""
                               class=""w200""
                               clearable
                               filterable
                               remote
                               reserve-keyword
                               placeholder=""请输入名称或型号""
                               :remote-method=""remoteMethod""
                               :loading=""loading"">
                        <el-option v-for=""(item,index) in searchList""
   ");
                WriteLiteral(@"                                :key=""item.index""
                                   :label=""item.name""
                                   :value=""item.id"">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label=""所在仓库"" prop=""warehouseId"">
                    <el-select v-model=""addSkuStockForm.warehouseId"" placeholder=""请选择仓库"" class=""w200"">
                        <el-option v-for=""item in warehouseList""
                                   :key=""item.id""
                                   :label=""item.number""
                                   :value=""item.id"">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label=""现货库存"" prop=""currentStock"">
                    <el-input-number v-model=""addSkuStockForm.currentStock"" :controls=""false"" :precision=""0"" class=""w200""></el-input-number>
                </el-form-item>
                <e");
                WriteLiteral(@"l-form-item label=""锁定库存"" prop=""lockStock"">
                    <el-input-number v-model=""addSkuStockForm.lockStock"" :controls=""false"" :precision=""0"" class=""w200""></el-input-number>
                </el-form-item>
                <el-form-item label=""安全库存"" prop=""safeStock"">
                    <el-input-number v-model=""addSkuStockForm.safeStock"" :controls=""false"" :precision=""0"" class=""w200""></el-input-number>
                </el-form-item>
                <el-form-item label=""低库存"" prop=""lowStock"">
                    <el-input-number v-model=""addSkuStockForm.lowStock"" :controls=""false"" :precision=""0"" class=""w200""></el-input-number>
                </el-form-item>
                <el-form-item label=""在途库存"" prop=""onWayStock"">
                    <el-input-number v-model=""addSkuStockForm.onWayStock"" :controls=""false"" :precision=""0"" class=""w200""></el-input-number>
                </el-form-item>
");
                WriteLiteral(@"

            </el-form>
            <div slot=""footer"" class=""dialog-footer"">
                <el-button type=""primary"" size=""small"" v-on:click=""saveAdd()"">保 存</el-button>
            </div>

        </el-dialog>
    </div>
    <script>
        var Main = {
            data() {
                return {
                    listLoading: false,
                    loading: false,
                    skuStockList: [],
                    filters: {
                        searchKey: """",
                        warehouseId: 0
                    },
                    total: 30,
                    page: 1,
                    pageSize: 10,
                    warehouseList: [],
                    warehouseIdPath: [],
                    editSkuStockVisible: false,
                    addSkuStockVisible: false,
                    skuStockForm: {
                        id: 0,
                        skuCostId: 0,
                        skuId: 0,
                        sku: void ");
                WriteLiteral(@"0,
                        productId: 0,
                        batchId: 0,
                        batchNo: '',
                        skuName: void 0,
                        skuFactoryModel: void 0,
                        warehouseId: void 0,
                        currentStock: void 0,
                        lockStock: void 0,
                        safeStock: void 0,
                        lowStock: void 0,
                        onWayStock: void 0,
                        preStock: void 0,
                        warehouse: {
                            name: ''
                        }
                    },
                    addSkuStockForm: {
                        id: 0,
                        skuCostId: 0,
                        skuId: 0,
                        sku: void 0,
                        productId: 0,
                        batchId: 0,
                        batchNo: '',
                        skuName: void 0,
                        skuFactoryM");
                WriteLiteral(@"odel: void 0,
                        warehouseId: void 0,
                        currentStock: void 0,
                        lockStock: void 0,
                        safeStock: void 0,
                        lowStock: void 0,
                        onWayStock: void 0,
                        preStock: void 0,
                        warehouse: {
                            name: ''
                        }
                    },
                    searchList: [],
                    searchAllList: [],

                }
            },
            created() {
                this.getWarehouseSimpleList(this.getSkuStockList);
            },
            methods: {
                getWarehouseSimpleList: function (action) {
                    var _self = this;
                    $.ajax({
                        type: 'GET',
                        url: '/rest/warehouse/list/simple/get',
                        success: function (data) {
                            if (data.s");
                WriteLiteral(@"uccess == true) {
                                _self.warehouseList = data.data;
                                _self.warehouseList.unshift({
                                    id: 0,
                                    number: '请选择仓库'
                                });
                            } else {
                                _self.$notify.error({
                                    title: '错误',
                                    message: data.msg
                                });
                            }
                        },
                        complete() {
                            if (action)
                                action();
                        }
                    });
                },
                getSearchAll() {
                    var _self = this;
                    _self.listLoading = true;
                    $.ajax({
                        type: 'post',
                        dataType: 'json',
                      ");
                WriteLiteral(@"  url: '/rest/sku/select/list',
                        data: _self.filters,
                        success: function (data) {
                            _self.searchAllList = data.data;
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThrown == ""Unauthorized"") {
                                window.parent.MainIndex.openLoginFrom();
                                _self.$message({
                                    type: 'error',
                                    message: XMLHttpRequest.responseJSON.msg
                                });
                            } else {
                                _self.$message({
                                    type: 'error',
                                    message: errorThrown
                                });
                            }
                        },
                        complete() {
                            _");
                WriteLiteral(@"self.listLoading = false;
                        }
                    });
                },
                remoteMethod(query) {
                    if (query !== '') {
                        this.loading = true;
                        setTimeout(() => {
                            this.loading = false;
                            this.searchList = this.searchAllList.filter(item => {
                                return item.name.toLowerCase().indexOf(query.toLowerCase()) > -1;
                            });
                        }, 200);
                    } else {
                        this.searchList = [];
                    }
                },
                // 改变每页显示几条数据
                handleSizeChange(val) {
                    this.pageSize = val;
                    this.getSkuStockList();
                },
                //【点击第几页】
                handleCurrentChange(val) {
                    this.page = val;
                    this.getSkuStockList();
  ");
                WriteLiteral(@"              },
                getSkuStockList() {
                    this.listLoading = true;
                    var _self = this;
                    var warehouseId = 0;
                    $.ajax({
                        type: 'GET',
                        url: '../../rest/sku/stock/list',
                        data: { key: _self.filters.searchKey, warehouseId: warehouseId, pageSize: _self.pageSize, currentPage: _self.page },
                        success: function (data) {
                            if (data.success == true) {
                                _self.total = data.page.totalCount;
                                _self.skuStockList = data.data;
                            } else {
                                _self.$notify.error({
                                    title: '错误',
                                    message: data.msg
                                });
                            }
                        },
                        error: funct");
                WriteLiteral(@"ion (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThrown == ""Unauthorized"") {
                                window.parent.MainIndex.openLoginFrom();
                                _self.$message({
                                    type: 'error',
                                    message: XMLHttpRequest.responseJSON.msg
                                });
                            } else {
                                _self.$message({
                                    type: 'error',
                                    message: errorThrown
                                });
                            }
                        },
                        complete() {
                            _self.listLoading = false;
                        }
                    });
                },
                search() {
                    this.getSkuStockList();
                },
                add() {
                    //this.getSearchAll(");
                WriteLiteral(@");
                    //this.addSkuStockVisible = true;
                    var _self = this;
                    window.parent.MainIndex.addTabByIframe(""添加RGC在售产品库存_"" + 0, ""/Sku/AddSkuStock?&skuStockId=0"", ""添加RGC在售产品库存"");

                },
                saveAdd() {
                    var _self = this;
                    console.log(_self.addSkuStockForm);
                    $.ajax({
                        type: 'POST',
                        contentType: 'application/json',
                        url: '../../rest/sku/stock/create',
                        data: JSON.stringify(_self.addSkuStockForm),
                        success: function (data) {
                            if (data.success == true) {
                                _self.$message({
                                    type: 'success',
                                    message: '保存成功'
                                });
                                _self.addSkuStockVisible = false;
                     ");
                WriteLiteral(@"           _self.getSkuStockList();
                            } else {
                                _self.$notify.error({
                                    title: '错误',
                                    message: data.msg
                                });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThrown == ""Unauthorized"") {
                                window.parent.MainIndex.openLoginFrom();
                                _self.$message({
                                    type: 'error',
                                    message: XMLHttpRequest.responseJSON.msg
                                });
                            } else {
                                _self.$message({
                                    type: 'error',
                                    message: errorThrown
                                });
                ");
                WriteLiteral(@"            }
                        }
                    });
                },
                edit(row) {
                    var _self = this;
                    var skuStockId = row.id;
                    $.ajax({
                        type: 'GET',
                        url: '/rest/sku/stock/detail',
                        data: { skuStockId: skuStockId },
                        success: function (data) {
                            if (data.success == true) {
                                _self.editSkuStockVisible = true;
                                _self.skuStockForm.id = data.data.id;
                                _self.skuStockForm.sku = data.data.sku;
                                _self.skuStockForm.skuId = data.data.skuId;
                                _self.skuStockForm.skuName = data.data.sku.fullEName;
                                _self.skuStockForm.skuFactoryModel = data.data.sku.factoryModel;
                                _self.skuStockForm.warehou");
                WriteLiteral(@"seId = data.data.warehouseId;
                                _self.skuStockForm.warehouse = data.data.warehouse;
                                _self.skuStockForm.currentStock = data.data.currentStock;
                                _self.skuStockForm.lockStock = data.data.lockStock;
                                _self.skuStockForm.safeStock = data.data.safeStock;
                                _self.skuStockForm.lowStock = data.data.lowStock;
                                _self.skuStockForm.onWayStock = data.data.onWayStock;
                                _self.skuStockForm.preStock = data.data.preStock;
                                _self.skuStockForm.productId = data.data.productId;
                                _self.skuStockForm.skuCostId = data.data.skuCostId;
                                _self.skuStockForm.batchId = data.data.batchId;
                                _self.skuStockForm.batchNo = data.data.batchNo;
                            } else {
                        ");
                WriteLiteral(@"        _self.$notify.error({
                                    title: '错误',
                                    message: data.msg
                                });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThrown == ""Unauthorized"") {
                                window.parent.MainIndex.openLoginFrom();
                                _self.$message({
                                    type: 'error',
                                    message: XMLHttpRequest.responseJSON.msg
                                });
                            } else {
                                _self.$message({
                                    type: 'error',
                                    message: errorThrown
                                });
                            }
                        }
                    });
                },
            ");
                WriteLiteral(@"    saveEdit() {
                    var _self = this;
                    console.log(_self.skuStockForm);
                    $.ajax({
                        type: 'POST',
                        contentType: 'application/json',
                        url: '../../rest/sku/stock/update',
                        data: JSON.stringify(_self.skuStockForm),
                        success: function (data) {
                            if (data.success == true) {
                                _self.$message({
                                    type: 'success',
                                    message: '保存成功'
                                });
                                _self.editSkuStockVisible = false;
                                _self.getSkuStockList();
                            } else {
                                _self.$notify.error({
                                    title: '错误',
                                    message: data.msg
                               ");
                WriteLiteral(@" });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThrown == ""Unauthorized"") {
                                window.parent.MainIndex.openLoginFrom();
                                _self.$message({
                                    type: 'error',
                                    message: XMLHttpRequest.responseJSON.msg
                                });
                            } else {
                                _self.$message({
                                    type: 'error',
                                    message: errorThrown
                                });
                            }
                        }
                    });
                },
                detail(id) {

                },

                del(index, id) {
                    var _self = this;
                    this.$confirm(""确定删除吗"", '提示', {
");
                WriteLiteral(@"                        type: 'warning'
                    }).then(() => {
                        $.ajax({
                            type: 'GET',
                            url: '../../rest/sku/stock/delete',
                            data: { id: id },
                            success: function (data) {
                                if (data.success == true) {
                                    _self.$message({
                                        type: 'success',
                                        message: '删除成功'
                                    });
                                    _self.getSkuStockList();
                                } else {
                                    _self.$notify.error({
                                        title: '错误',
                                        message: data.msg
                                    });
                                }
                            },
                            error: function (XM");
                WriteLiteral(@"LHttpRequest, textStatus, errorThrown) {
                                if (errorThrown == ""Unauthorized"") {
                                    window.parent.MainIndex.openLoginFrom();
                                    _self.$message({
                                        type: 'error',
                                        message: XMLHttpRequest.responseJSON.msg
                                    });
                                } else {
                                    _self.$message({
                                        type: 'error',
                                        message: errorThrown
                                    });
                                }
                            }
                        });
                    }).catch(() => {

                    });
                }
            }
        };
        var Ctor = Vue.extend(Main);
        new Ctor().$mount('#skustocklist');
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
