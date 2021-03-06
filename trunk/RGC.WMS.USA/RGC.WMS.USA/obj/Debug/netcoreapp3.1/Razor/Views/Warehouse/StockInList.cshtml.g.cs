#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Warehouse\StockInList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3009620dea84d8ad52aaaa1220fb24ffc4a92a0c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Warehouse_StockInList), @"mvc.1.0.view", @"/Views/Warehouse/StockInList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3009620dea84d8ad52aaaa1220fb24ffc4a92a0c", @"/Views/Warehouse/StockInList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Warehouse_StockInList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 2 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Warehouse\StockInList.cshtml"
  
    ViewData["Title"] = "入库列表";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3009620dea84d8ad52aaaa1220fb24ffc4a92a0c3692", async() => {
                WriteLiteral(@"
    <div id=""stockinlist"">
        <!-- 标题和地址 -->
        <el-row>
            <el-col :span=""24"" class=""breadcrumb-container"">
                <el-breadcrumb separator=""/"">
                    <el-breadcrumb-item><strong>库存管理</strong></el-breadcrumb-item>
                    <el-breadcrumb-item>入库列表</el-breadcrumb-item>
                </el-breadcrumb>
            </el-col>
        </el-row>
        <article class=""administration-tenant-container"">
            <!--搜索-->
            <el-col :span=""24"" class=""toolbar"">
                <el-form :inline=""true"" size=""small"" :model=""filters"" v-on:submit.native.prevent>
                    <el-form-item>
                        <el-select v-model=""filters.searchType""
                                   style=""width:85px"">
                            <el-option v-for=""item in searchTypeList""
                                       :key=""item.value""
                                       :label=""item.label""
                                       :");
                WriteLiteral(@"value=""item.value"">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item>
                        <el-input placeholder=""搜索"" v-model=""filters.searchKey""
                                  v-on:keyup.enter.native=""getStockInList()"" clearable></el-input>
                    </el-form-item>
                    <el-form-item label=""仓库"">
                        <el-select v-model=""filters.warehouseId""
                                   placeholder=""请选择仓库""
                                   style=""width:100px"">
                            <el-option v-for=""item in warehouseList""
                                       :key=""item.id""
                                       :label=""item.number""
                                       :value=""item.id"">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label=""入库状态""");
                WriteLiteral(@">
                        <el-select v-model=""filters.status""
                                   placeholder=""请选择状态""
                                   style=""width:100px"">
                            <el-option v-for=""item in statusList""
                                       :key=""item.value""
                                       :label=""item.label""
                                       :value=""item.value"">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label=""是否删除"">
                        <el-select v-model=""filters.isDeleted""
                                   placeholder=""请选择状态""
                                   style=""width:100px"">
                            <el-option v-for=""item in deletedList""
                                       :key=""item.value""
                                       :label=""item.label""
                                       :value=""item.value"">
           ");
                WriteLiteral(@"                 </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item>
                        <el-button v-on:click=""search"" icon=""el-icon-search"" size=""small"">搜索</el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-dropdown trigger=""click"" size=""small"">
                            <el-button type=""primary"">
                                <i class=""el-icon-plus""></i>
                                添加入库单
                            </el-button>
                            <el-dropdown-menu slot=""dropdown"">
                                <el-dropdown-item>
                                    <div v-on:click=""addPacking"">发货单入库</div>
                                </el-dropdown-item>
                            </el-dropdown-menu>
                        </el-dropdown>
                    </el-form-item>
                </el-form>
            </el-col>
            ");
                WriteLiteral(@"<el-table class=""data-table""
                      v-loading=""listLoading""
                      :data=""stockInList""
                      :fit=""true""
                      default-expand-all
                      border
                      size=""small"">
                <el-table-column type=""expand"">
                    <template slot-scope=""scope"">
                        <el-table class=""data-table"" :data=""scope.row.detail"" :fit=""true"" border size=""small"">
                            <el-table-column min-width=""100"" prop=""factoryModel"" label=""发货箱号"" align=""center"">
                                <template slot-scope=""scopeDetail"">
                                    <i>{{scopeDetail.row.packingDetail.containerNo}}</i>
                                </template>
                            </el-table-column>
                            <el-table-column min-width=""100"" prop=""factoryModel"" label=""发货型号"" align=""center"">
                                <template slot-scope=""scopeDetail"">
     ");
                WriteLiteral(@"                               <i>{{scopeDetail.row.packingDetail.sku.factoryModel}}</i>
                                </template>
                            </el-table-column>
                            <el-table-column min-width=""100"" prop=""batchNo"" label=""发货批次"" align=""center"">
                                <template slot-scope=""scopeDetail"">
                                    <i>{{scopeDetail.row.packingDetail.skuCostBatch.batchNo}}</i>
                                </template>
                            </el-table-column>
                            <el-table-column min-width=""60"" prop=""planInQty"" label=""发货数量"" align=""center"">
                                <template slot-scope=""scopeDetail"">
                                    <i>{{scopeDetail.row.planInQty}}</i>
                                </template>
                            </el-table-column>
                            <el-table-column min-width=""100"" prop=""factoryModel"" label=""入库型号"" align=""center"">
                    ");
                WriteLiteral(@"            <template slot-scope=""scopeDetail"">
                                    <i>{{scopeDetail.row.actFactoryModel}}</i>
                                </template>
                            </el-table-column>
                            <el-table-column min-width=""100"" prop=""batchNo"" label=""入库批次"" align=""center"">
                                <template slot-scope=""scopeDetail"">
                                    <i>{{scopeDetail.row.skuCostBatch.batchNo}}</i>
                                </template>
                            </el-table-column>
                            <el-table-column min-width=""100"" prop=""actInQty"" label=""入库数量"" align=""center"">
                                <template slot-scope=""scopeDetail"">
                                    <i>{{scopeDetail.row.actInQty}}</i>
                                </template>
                            </el-table-column>
                            <el-table-column width=""100"" label=""操作"" align=""center"">
                      ");
                WriteLiteral(@"          <template slot-scope=""scope"">

                                </template>
                            </el-table-column>
                        </el-table>
                    </template>
                </el-table-column>
                <el-table-column min-width=""40"" prop=""stockInNum"" label=""入库编号"" align=""center"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.stockInNum}}</i>
                    </template>
                </el-table-column>
                <el-table-column prop=""stockInType"" min-width=""60"" align=""center"" label=""状态"">
                    <template slot-scope=""scope"">
                        <el-tag size=""small"" v-if=""scope.row.stockInType == 1"" type=""success"">采购入库</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.stockInType == 2"" type=""info"">调拨入库</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.stockInType == 3"" type=""warning"">退货入库</el-tag>
                    </template>
      ");
                WriteLiteral(@"          </el-table-column>
                <el-table-column min-width=""100"" prop=""number"" label=""入库仓库"" align=""center"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.warehouse.number}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""100"" prop=""contractNo"" label=""发货合同"" align=""center"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.packingList.contractNo}}</i>
                    </template>
                </el-table-column>
                <el-table-column prop=""stockInStatus"" min-width=""60"" align=""center"" label=""状态"">
                    <template slot-scope=""scope"">
                        <el-tag size=""small"" v-if=""scope.row.stockInStatus == 1"" type=""warning"">未入库</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.stockInStatus == 2"" type=""info"">入库中</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.stockI");
                WriteLiteral(@"nStatus == 3"" type=""success"">已入库</el-tag>
                    </template>
                </el-table-column>
                <el-table-column min-width=""60"" align=""center"" label=""删除状态"">
                    <template slot-scope=""scope"">
                        <el-tag type=""primary"" size=""mini"" v-if=""!scope.row.isDeleted"">未删除</el-tag>
                        <el-tag type=""info"" size=""mini"" v-if=""scope.row.isDeleted"">已删除</el-tag>
                    </template>
                </el-table-column>
                <el-table-column width=""150"" prop=""lastModificationTimeString"" label=""上次修改时间"" align=""center"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.lastModificationTimeString}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""60"" prop=""createUser"" label=""创建人"" align=""center"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.createUser}}</i>
         ");
                WriteLiteral(@"           </template>
                </el-table-column>

                <el-table-column width=""180"" label=""操作"" align=""center"">
                    <template slot-scope=""scope"">
                        <el-row>
                            <el-dropdown trigger=""click"">
                                <el-button type=""primary"" size=""mini"" class=""mb5"">
                                    操作
                                    <i class=""el-icon-caret-bottom el-icon--right""></i>
                                </el-button>
                                <el-dropdown-menu slot=""dropdown"">
                                    <el-dropdown-item v-if=""!scope.row.isDeleted"">
                                        <div v-on:click=""editPacking(scope.row)"">修 改</div>
                                    </el-dropdown-item>
                                    <el-dropdown-item v-if=""!scope.row.isDeleted"">
                                        <div v-on:click=""del(scope.$index, scope.row)"">删 除</div>
   ");
                WriteLiteral(@"                                 </el-dropdown-item>
                                    <el-dropdown-item v-if=""scope.row.isDeleted"">
                                        <div v-on:click=""rec(scope.$index, scope.row)"">恢 复</div>
                                    </el-dropdown-item>
                                    <el-dropdown-item>
                                        <div v-on:click=""detail(scope.row)"">查看</div>
                                    </el-dropdown-item>
                                </el-dropdown-menu>
                            </el-dropdown>
                        </el-row>
                        <el-row>
                            <el-dropdown trigger=""click"" size=""small"">
                                <el-button type=""warning"" plain size=""mini"" class=""mb5"">
                                    状态维护
                                    <i class=""el-icon-caret-bottom el-icon--right""></i>
                                </el-button>
                            ");
                WriteLiteral(@"    <el-dropdown-menu slot=""dropdown"">
                                    <el-dropdown-item v-if=""scope.row.stockInStatus != 1"">
                                        <div v-on:click=""update_status(scope.$index, scope.row, 1)"">未入库</div>
                                    </el-dropdown-item>
                                    <el-dropdown-item v-if=""scope.row.stockInStatus != 2"">
                                        <div v-on:click=""update_status(scope.$index, scope.row, 2)"">入库中</div>
                                    </el-dropdown-item>
                                    <el-dropdown-item v-if=""scope.row.stockInStatus != 3"">
                                        <div v-on:click=""update_status(scope.$index, scope.row, 3)"">已入库</div>
                                    </el-dropdown-item>
                                </el-dropdown-menu>
                            </el-dropdown>
                        </el-row>
                    </template>
                </el-table-column>
   ");
                WriteLiteral(@"         </el-table>
            <el-col :span=""24"" class=""toolbar"">
                <el-pagination v-on:size-change=""handleSizeChange"" v-on:current-change=""handleCurrentChange"" :current-page=""page"" :page-sizes=""[10,20,30,40,50]"" :page-size=""pageSize"" layout=""total, sizes, prev, pager, next, jumper"" :total=""total"" style=""float:right;"">
                </el-pagination>
            </el-col>
        </article>
    </div>
    <script>
        var Main = {
            data() {
                return {
                    listLoading: false,
                    loading: false,
                    stockInList: [],
                    warehouseList: [],
                    searchTypeList: [{
                        value: 'contractNo',
                        label: '合同号'
                    }, {
                        value: 'containerNo',
                        label: '箱号'
                    }, {
                        value: 'skuName',
                        label: '产品'
              ");
                WriteLiteral(@"      }, {
                        value: 'batchNo',
                        label: '批次'
                    }, {
                        value: 'factoryModel',
                        label: '型号'
                    }],
                    statusList: [{
                        value: 0,
                        label: '全部显示'
                    }, {
                        value: 1,
                        label: '未入库'
                    }, {
                        value: 2,
                        label: '入库中'
                    }, {
                        value: 3,
                        label: '已入库'
                    }],
                    deletedList: [{
                        value: 0,
                        label: '全部显示'
                    }, {
                        value: 1,
                        label: '未删除'
                    }, {
                        value: 2,
                        label: '已删除'
                    }],
                    filters: {");
                WriteLiteral(@"
                        searchType: 'factoryModel',
                        searchKey: '',
                        warehouseId: 0,
                        status: 0,
                        isDeleted: 0
                    },
                    total: 30,
                    page: 1,
                    pageSize: 10
                }
            },
            created() {
                this.getWarehouseSimpleList(this.getStockInList);
            },
            methods: {
                detail(row) {
                    window.parent.MainIndex.addTabByIframe('查看入库单_' + row.id, '/Warehouse/StockInShow?stockInId=' + row.id, '查看入库单');
                },
                errorException(XMLHttpRequest, textStatus, errorThrown) {
                    if (XMLHttpRequest &&
                        XMLHttpRequest.responseJSON &&
                        errorThrown == ""Unauthorized"") {
                        window.parent.MainIndex.openLoginFrom();
                        this.$message({
  ");
                WriteLiteral(@"                          type: 'error',
                            message: XMLHttpRequest.responseJSON.msg
                        });
                    } else {
                        this.$message({
                            type: 'error',
                            message: errorThrown
                        });
                    }
                },
                getWarehouseSimpleList(action) {
                    var _self = this;
                    $.ajax({
                        type: 'GET',
                        url: '/rest/warehouse/list/simple/get',
                        success: function (data) {
                            if (data.success == true) {
                                _self.warehouseList = data.data;
                                _self.warehouseList.unshift({
                                    id: 0,
                                    number: '全部显示'
                                });

                            } else {
             ");
                WriteLiteral(@"                   _self.$notify.error({
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
                // 改变每页显示几条数据
                handleSizeChange(val) {
                    this.pageSize = val;
                    this.getStockInList();
                },
                //【点击第几页】
                handleCurrentChange(val) {
                    this.page = val;
                    this.getStockInList();
                },
                update_status(index, row, status) {
                    var _self = this;
                    this.$confirm(""确定修改状态吗"", '提示', {
                        type: 'warning'
                    }).then(() => {
            ");
                WriteLiteral(@"            $.ajax({
                            type: 'GET',
                            url: '/rest/warehouse/stock/in/status/update',
                            data: { id: row.id, status: status },
                            success: function (data) {
                                if (data.success == true) {
                                    _self.$notify.success({
                                        title: '成功',
                                        message: ""更新状态成功""
                                    });
                                    _self.getStockInList();
                                } else {
                                    _self.$notify.error({
                                        title: '错误',
                                        message: data.msg
                                    });
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
             ");
                WriteLiteral(@"                   _self.errorException(XMLHttpRequest, textStatus, errorThrown);
                            }
                        });
                    }).catch(() => {

                    });
                },
                getStockInList() {
                    this.listLoading = true;
                    var _self = this;
                    $.ajax({
                        type: 'GET',
                        url: '/rest/warehouse/stock/in/list',
                        data: {
                            searchType: _self.filters.searchType,
                            searchKey: _self.filters.searchKey,
                            warehouseId: _self.filters.warehouseId,
                            status: _self.filters.status,
                            isDeleted: _self.filters.isDeleted,
                            pageSize: _self.pageSize,
                            currentPage: _self.page
                        },
                        success: function (data) {");
                WriteLiteral(@"
                            if (data.success == true) {
                                _self.total = data.page.totalCount;
                                _self.stockInList = data.data;
                            } else {
                                _self.$notify.error({
                                    title: '错误',
                                    message: data.msg
                                });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            _self.errorException(XMLHttpRequest, textStatus, errorThrown);
                        },
                        complete() {
                            _self.listLoading = false;
                        }
                    });
                },
                search() {
                    this.getStockInList();
                },
                addPacking() {
                    window.parent.MainIndex");
                WriteLiteral(@".addTabByIframe('添加发货入库单', '/Warehouse/StockInEdit?stockInId=0', '添加发货入库单');

                },
                editPacking(row) {
                    window.parent.MainIndex.addTabByIframe('修改发货入库单_' + row.id, '/Warehouse/StockInEdit?stockInId=' + row.id, '修改发货入库单');
                },
                del(index, row) {
                    var _self = this;
                    this.$confirm(""确定删除入库单吗"", '提示', {
                        type: 'warning'
                    }).then(() => {
                        $.ajax({
                            type: 'GET',
                            url: '/rest/warehouse/stock/in/delete/' + row.id,
                            success: function (data) {
                                if (data.success == true) {
                                    _self.$message({
                                        type: 'success',
                                        message: '删除成功'
                                    });
                                    _sel");
                WriteLiteral(@"f.getStockInList();
                                } else {
                                    _self.$notify.error({
                                        title: '错误',
                                        message: data.msg
                                    });
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                _self.errorException(XMLHttpRequest, textStatus, errorThrown);
                            }
                        });
                    }).catch(() => {

                    });
                },
                rec(index, row) {
                    var _self = this;
                    this.$confirm(""确定恢复入库单吗"", '提示', {
                        type: 'warning'
                    }).then(() => {
                        $.ajax({
                            type: 'GET',
                            url: '/rest/warehouse/stock/in/recovery/");
                WriteLiteral(@"' + row.id,
                            success: function (data) {
                                if (data.success == true) {
                                    _self.$message({
                                        type: 'success',
                                        message: '恢复成功'
                                    });
                                    _self.getStockInList();
                                } else {
                                    _self.$message({
                                        type: 'error',
                                        message: data.msg
                                    });
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                _self.errorException(XMLHttpRequest, textStatus, errorThrown);
                            }
                        });
                    }).catch(() => {

                   ");
                WriteLiteral(" });\r\n                }\r\n            },\r\n            mounted() {\r\n                this.listLoading = true;\r\n            }\r\n        }\r\n        var Ctor = Vue.extend(Main)\r\n        new Ctor().$mount(\'#stockinlist\')\r\n    </script>\r\n\r\n");
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
            WriteLiteral("\r\n\r\n");
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
