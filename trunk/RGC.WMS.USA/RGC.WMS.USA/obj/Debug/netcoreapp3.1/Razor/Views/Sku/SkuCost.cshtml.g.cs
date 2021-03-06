#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\SkuCost.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02a26064f70c2ba114e33ba4556eac4839a72a06"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sku_SkuCost), @"mvc.1.0.view", @"/Views/Sku/SkuCost.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02a26064f70c2ba114e33ba4556eac4839a72a06", @"/Views/Sku/SkuCost.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Sku_SkuCost : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 2 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\SkuCost.cshtml"
  
    ViewBag.Title = "List";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "02a26064f70c2ba114e33ba4556eac4839a72a063628", async() => {
                WriteLiteral(@"
    <div id=""skustocklist"">
        <!-- 标题和地址 -->
        <el-row>
            <el-col :span=""24"" class=""breadcrumb-container"">
                <el-breadcrumb separator=""/"">
                    <el-breadcrumb-item><strong>产品管理</strong></el-breadcrumb-item>
                    <el-breadcrumb-item>RGC在售产品列表</el-breadcrumb-item>
                </el-breadcrumb>
            </el-col>
        </el-row>
        <article class=""administration-tenant-container"">
            <!--搜索-->
            <el-col :span=""24"" class=""toolbar"">
                <el-form :inline=""true"" size=""small"" :model=""filters"" v-on:submit.native.prevent>
                    <el-form-item>
                        <el-input placeholder=""搜索"" v-model=""filters.searchKey""
                                  v-on:keyup.enter.native=""getSkuCostList()"" clearable></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button v-on:click=""search"" icon=""el-icon-search"">搜索</el-button>");
                WriteLiteral(@"
                        <el-button v-on:click=""add(0)"" icon=""el-icon-plus"" type=""primary"">添加在售产品</el-button>
                    </el-form-item>
                </el-form>

            </el-col>
            <el-table class=""data-table"" v-loading=""listLoading"" :data=""skuCostList"" :fit=""true"" border size=""small"">
                <el-table-column type=""expand"">
                    <template slot-scope=""scope"">
                        <el-table class=""data-table"" :data=""scope.row.skuCostBatchList"" :fit=""true"" border size=""small"">
                            <el-table-column prop=""batchNo"" min-width=""100"" align=""center"" label=""批次号/订单号""></el-table-column>
                            <el-table-column prop=""status"" min-width=""100"" align=""center"" label=""状态"">
                                <template slot-scope=""scope1"">
                                    <el-tag size=""small"" v-if=""scope1.row.status == 0"" type=""info"">在制</el-tag>
                                    <el-tag size=""small"" v-if=""scope1.row.s");
                WriteLiteral(@"tatus == 1"" type=""warning"">在途</el-tag>
                                    <el-tag size=""small"" v-if=""scope1.row.status == 2"" type=""success"">已到货</el-tag>
                                    <el-tag size=""small"" v-if=""scope1.row.status == 3"">在售</el-tag>
                                    <el-tag size=""small"" v-if=""scope1.row.status == 4"" type=""danger"">售罄</el-tag>
                                </template>
                            </el-table-column>
                            <el-table-column prop=""remark"" min-width=""200"" align=""center"" label=""备注""></el-table-column>
                            <el-table-column min-width=""150"" label=""操作"">
                                <template slot-scope=""scope"">
                                    <el-dropdown trigger=""click"" size=""small"">
                                        <el-button type=""warning"" plain size=""mini"" class=""mb5 ml10"">
                                            状态维护
                                            <i class=""el-icon-caret-bo");
                WriteLiteral(@"ttom el-icon--right""></i>
                                        </el-button>
                                        <el-dropdown-menu slot=""dropdown"">
                                            <el-dropdown-item>
                                                <div v-on:click=""update_status(scope.$index, scope.row.skuCostId, scope.row.id, 0)"">在制</div>
                                            </el-dropdown-item>
                                            <el-dropdown-item>
                                                <div v-on:click=""update_status(scope.$index, scope.row.skuCostId, scope.row.id, 1)"">在途</div>
                                            </el-dropdown-item>
                                            <el-dropdown-item>
                                                <div v-on:click=""update_status(scope.$index, scope.row.skuCostId, scope.row.id, 2)"">已到货</div>
                                            </el-dropdown-item>
                                            <el-dropd");
                WriteLiteral(@"own-item>
                                                <div v-on:click=""update_status(scope.$index, scope.row.skuCostId, scope.row.id, 3)"">在售</div>
                                            </el-dropdown-item>
                                            <el-dropdown-item>
                                                <div v-on:click=""update_status(scope.$index, scope.row.skuCostId, scope.row.id, 4)"">售罄</div>
                                            </el-dropdown-item>
                                        </el-dropdown-menu>
                                    </el-dropdown>
                                </template>
                            </el-table-column>
                        </el-table>
                    </template>
                </el-table-column>
                <el-table-column min-width=""40"" prop=""id"" label=""ID"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.id}}</i>
                    </template>
                </e");
                WriteLiteral(@"l-table-column>

                <el-table-column min-width=""200"" prop=""fullEName"" label=""产品名"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.sku.fullEName}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""100"" prop=""factoryModel"" label=""型号"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.sku.factoryModel}}</i>
                    </template>
                </el-table-column>

                <el-table-column width=""120"" label=""图片"" align=""center"">
                    <template slot-scope=""scope"">
                        <img v-if=""scope.row.sku.primaryImageSrcFull"" class=""pimg"" alt=""no image"" :src=""scope.row.sku.primaryImageSrcFull"" height=""50"">
                    </template>
                </el-table-column>

                <el-table-column min-width=""80"" prop=""sku"" label=""Sku"">
                    <template slot-scope=""scope"">
      ");
                WriteLiteral(@"                  <i>{{scope.row.sku.sku}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""60"" prop=""grossWeight"" label=""毛重(LB)"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.sku.grossWeight}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""60"" prop=""qtyPallet"" label=""pcs/pallet"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.sku.qtyPallet}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""60"" prop=""loadingQty_40HQ"" label=""pcs/40HQ"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.sku.loadingQty_40HQ}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""60"" prop=""fobString"" label=""FOB $"">
                </");
                WriteLiteral(@"el-table-column>
                <el-table-column min-width=""60"" prop=""ddpString"" label=""DDP $"">
                </el-table-column>
                <el-table-column min-width=""60"" prop=""seaFreightString"" label=""海运费 $"">
                </el-table-column>
                <el-table-column min-width=""60"" prop=""unloadingChargeString"" label=""卸柜费 $"">
                </el-table-column>
                <el-table-column min-width=""60"" prop=""elcString"" label=""ELC $"">
                </el-table-column>
                <el-table-column min-width=""60"" prop=""z3FreightString"" label=""内陆运费Z3 $"">
                </el-table-column>
                <el-table-column min-width=""60"" prop=""z5FreightString"" label=""内陆运费Z5 $"">
                </el-table-column>
                <el-table-column min-width=""60"" prop=""msrpString"" label=""MSRP $"">
                </el-table-column>
                <el-table-column min-width=""60"" prop=""mapString"" label=""MAP $"">
                </el-table-column>

                <el-table-col");
                WriteLiteral(@"umn min-width=""125"" prop=""modifyTimeString"" label=""上次修改时间"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.modifyTimeString}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""60"" prop=""creatorUser"" label=""创建人"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.creatorUser}}</i>
                    </template>
                </el-table-column>

                <el-table-column width=""100"" label=""操作"" fixed=""right"">
                    <template slot-scope=""scope"">
                        <el-dropdown trigger=""click"">
                            <el-button type=""primary"" size=""mini"" class=""mb5"">
                                操作
                                <i class=""el-icon-caret-bottom el-icon--right""></i>
                            </el-button>
                            <el-dropdown-menu slot=""dropdown"">
                                <e");
                WriteLiteral(@"l-dropdown-item>
                                    <div v-on:click=""edit(scope.row.id)"">修 改</div>
                                </el-dropdown-item>
                                <el-dropdown-item>
                                    <div v-on:click=""del(scope.$index, scope.row.id)"">删 除</div>
                                </el-dropdown-item>
                                <el-dropdown-item>
                                    <div v-on:click=""addBatch(scope.row)"">添加批次</div>
                                </el-dropdown-item>
                                <el-dropdown-item>
                                    <div v-on:click=""detail(scope.row.skuId, scope.row.id)"">查看sku详情</div>
                                </el-dropdown-item>
                            </el-dropdown-menu>
                        </el-dropdown>
                    </template>
                </el-table-column>
            </el-table>
            <el-col :span=""24"" class=""toolbar"">
                <el-pagination v");
                WriteLiteral(@"-on:size-change=""handleSizeChange"" v-on:current-change=""handleCurrentChange"" :current-page=""page"" :page-sizes=""[10,20,30,40,50]"" :page-size=""pageSize"" layout=""total, sizes, prev, pager, next, jumper"" :total=""total"" style=""float:right;"">
                </el-pagination>
            </el-col>
        </article>

        <el-dialog title=""添加批次"" :visible.sync=""addSkuBatchVisible"" :close-on-click-modal=""false"" top=""5%"" width=""450px"">
            <el-form :model=""addSkuBatchForm"" size=""small"" label-width=""60px"" ref=""addSkuBatchForm"" inline label-position=""right"">
                <el-form-item label=""批次号"" prop=""batchNo"" label-width=""80px"">
                    <el-input v-model=""addSkuBatchForm.batchNo"" style=""width:280px;"">
                    </el-input>
                </el-form-item>
                <el-form-item label=""状态"" prop=""status"" label-width=""80px"">
                    <el-select v-model=""addSkuBatchForm.status"" style=""width:280px;"">
                        <el-option v-for=""item in statusLis");
                WriteLiteral(@"t""
                                   :key=""item.value""
                                   :label=""item.label""
                                   :value=""item.value"">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label=""备注"" prop=""remark"" label-width=""80px"">
                    <el-input v-model=""addSkuBatchForm.remark"" type=""textarea"" rows=""4"" style=""width:280px;""></el-input>
                </el-form-item>
            </el-form>
            <div slot=""footer"" class=""dialog-footer"">
                <el-button type=""primary"" size=""small"" v-on:click=""saveAddBatch()"">保 存</el-button>
            </div>

        </el-dialog>
    </div>
    <script>
        var Main = {
            data() {
                return {
                    listLoading: false,
                    loading: false,
                    skuCostList: [],
                    filters: {
                        searchKey: """",
          ");
                WriteLiteral(@"          },
                    total: 30,
                    page: 1,
                    pageSize: 10,
                    addSkuBatchVisible: false,
                    addSkuBatchForm: {
                        id: 0,
                        batchNo: void 0,
                        status: void 0,
                        remark: void 0,
                        skuCostId: 0,
                        skuId: 0,
                        sku: void 0,
                        productId: 0,

                    },

                    searchList: [],
                    searchAllList: [],
                    statusList: [
                        { label: '在制', value: 0 },
                        { label: '在途', value: 1 },
                        { label: '已到货', value: 2 },
                        { label: '在售', value: 3 },
                        { label: '售罄', value: 4 },

                    ],

                }
            },
            created() {
                this.getSkuCo");
                WriteLiteral(@"stList();
            },
            methods: {
                detail(skuId, skuCostId) {

                    window.open(""/Sku/SkuDetail?skuId="" + skuId + ""&skuCostId="" + skuCostId);
                },
                getSearchAll() {
                    var _self = this;
                    _self.listLoading = true;
                    $.ajax({
                        type: 'post',
                        dataType: 'json',
                        url: '/rest/sku/select/list',
                        data: _self.filters,
                        success: function (data) {
                            _self.searchAllList = data.data;
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThrown == ""Unauthorized"") {
                                window.parent.MainIndex.openLoginFrom();
                                _self.$message({
                                    type: 'error',
          ");
                WriteLiteral(@"                          message: XMLHttpRequest.responseJSON.msg
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
                remoteMethod(query) {
                    if (query !== '') {
                        this.loading = true;
                        setTimeout(() => {
                            this.loading = false;
                            this.searchList = this.searchAllList.filter(item => {
                                return item.name.toLowerCase().indexOf(query.toLowerCase()) > -1;
                            });
                       ");
                WriteLiteral(@" }, 200);
                    } else {
                        this.searchList = [];
                    }
                },
                // 改变每页显示几条数据
                handleSizeChange(val) {
                    this.pageSize = val;
                    this.getSkuCostList();
                },
                //【点击第几页】
                handleCurrentChange(val) {
                    this.page = val;
                    this.getSkuCostList();
                },
                update_status(index, id1, id2, status) {
                    var _self = this;
                    this.$confirm(""确定修改状态吗"", '提示', {
                        type: 'warning'
                    }).then(() => {
                        $.ajax({
                            type: 'GET',
                            url: '../../rest/sku/cost/batch/status/update',
                            data: { costId: id1, batchId: id2, status: status },
                            success: function (data) {
                      ");
                WriteLiteral(@"          if (data.success == true) {
                                    //_self.productList[index].status = status;
                                    _self.$notify.success({
                                        title: '成功',
                                        message: ""更新状态成功""
                                    });
                                    _self.getSkuCostList();
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
               ");
                WriteLiteral(@"                         type: 'error',
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
                },
                getSkuCostList() {
                    this.listLoading = true;
                    var _self = this;
                    $.ajax({
                        type: 'GET',
                        url: '../../rest/sku/cost/list',
                        data: { key: _self.filters.searchKey, pageSize: _self.pageSize, currentPage: _self.page },
                        success: function (data) {
          ");
                WriteLiteral(@"                  if (data.success == true) {
                                _self.total = data.page.totalCount;
                                _self.skuCostList = data.data;
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
                    ");
                WriteLiteral(@"                type: 'error',
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
                    this.getSkuCostList();
                },
                add(skuCostId) {
                    var _self = this;
                    window.parent.MainIndex.addTabByIframe(""添加RGC在售产品_"" + skuCostId, ""/Sku/SkuCostDetail?&skuCostId="" + skuCostId, ""添加RGC在售产品"");

                },
                edit(skuCostId) {
                    var _self = this;
                    window.parent.MainIndex.addTabByIframe(""修改RGC在售产品_"" + skuCostId, ""/Sku/SkuCostUpdate?&skuCostId="" + skuCostId, ""修改RGC在售产品"");
                },
                addBatch(row) {
                    var _self = this;
                    _self.a");
                WriteLiteral(@"ddSkuBatchVisible = true;
                    _self.addSkuBatchForm.skuCostId = row.id;
                    _self.addSkuBatchForm.skuId = row.skuId;
                    _self.addSkuBatchForm.productId = row.productId;
                },
                saveAddBatch() {
                    var _self = this;
                    console.log(_self.addSkuBatchForm);
                    $.ajax({
                        type: 'POST',
                        contentType: 'application/json',
                        url: '../../rest/sku/cost/batch/add',
                        data: JSON.stringify(_self.addSkuBatchForm),
                        success: function (data) {
                            if (data.success == true) {
                                _self.$message({
                                    type: 'success',
                                    message: '保存成功'
                                });
                                _self.addSkuBatchVisible = false;
                      ");
                WriteLiteral(@"          _self.clear();
                                _self.getSkuCostList();
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
       ");
                WriteLiteral(@"                         });
                            }
                        }
                    });
                },
                del(index, id) {
                    var _self = this;
                    this.$confirm(""确定删除吗"", '提示', {
                        type: 'warning'
                    }).then(() => {
                        $.ajax({
                            type: 'GET',
                            url: '../../rest/sku/cost/delete',
                            data: { skuCostId: id },
                            success: function (data) {
                                if (data.success == true) {
                                    _self.$message({
                                        type: 'success',
                                        message: '删除成功'
                                    });
                                    _self.getSkuCostList();
                                } else {
                                    _self.$notify.error({
     ");
                WriteLiteral(@"                                   title: '错误',
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
                 ");
                WriteLiteral(@"       });
                    }).catch(() => {

                    });
                },
                clear() {
                    var _self = this;
                    //清空缓存
                    _self.addSkuBatchForm = {
                        id: 0,
                        batchNo: void 0,
                        status: void 0,
                        remark: void 0,
                        skuCostId: 0,
                        skuId: 0,
                    }

                }
            },
            mounted() {
                this.listLoading = true;
            }
        }
        var Ctor = Vue.extend(Main)
        new Ctor().$mount('#skustocklist')
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
