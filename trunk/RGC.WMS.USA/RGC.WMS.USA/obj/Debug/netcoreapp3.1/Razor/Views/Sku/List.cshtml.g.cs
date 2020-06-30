#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "47f363d28f2046c1ff7b8115201b425ed28c0f72"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sku_List), @"mvc.1.0.view", @"/Views/Sku/List.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"47f363d28f2046c1ff7b8115201b425ed28c0f72", @"/Views/Sku/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Sku_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 2 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\List.cshtml"
  
    /**/

    ViewBag.Title = "List";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "47f363d28f2046c1ff7b8115201b425ed28c0f723622", async() => {
                WriteLiteral("\r\n    <div id=\"skulist\">\r\n        <!-- 标题和地址 -->\r\n        <el-row>\r\n            <el-col :span=\"24\" class=\"breadcrumb-container\">\r\n");
                WriteLiteral(@"                <el-breadcrumb separator=""/"">
                    <el-breadcrumb-item><strong>产品管理</strong></el-breadcrumb-item>
                    <el-breadcrumb-item>Sku列表</el-breadcrumb-item>
                </el-breadcrumb>
            </el-col>
        </el-row>
        <article class=""administration-tenant-container"">
            <!--搜索-->
            <el-col :span=""24"" class=""toolbar"">
                <el-form :inline=""true"" size=""small"" :model=""filters"" v-on:submit.native.prevent>
                    <el-form-item>
                        <el-input placeholder=""搜索"" v-model=""filters.searchKey""
                                  v-on:keyup.enter.native=""getSkuList()"" clearable></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button v-on:click=""search"" icon=""el-icon-search"">搜索</el-button>
                    </el-form-item>
                </el-form>
            </el-col>
            <el-table class=""data-table"" size=""small""");
                WriteLiteral(@" v-loading=""listLoading"" :data=""skuList"" :fit=""true"" border>
                <el-table-column min-width=""40"" prop=""id"" label=""ID"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.id}}</i>
                    </template>
                </el-table-column>
");
                WriteLiteral(@"                <el-table-column min-width=""200"" prop=""fullEName"" label=""产品名"">
                </el-table-column>
                <el-table-column width=""120"" label=""图片"" align=""center"">
                    <template slot-scope=""scope1"">
                        <img v-if=""scope1.row.primaryImageSrcFull"" class=""pimg"" alt=""no image"" :src=""scope1.row.primaryImageSrcFull"" height=""50"">
                    </template>
                </el-table-column>
                <el-table-column prop=""status"" label=""状态"" min-width=""80"">
                    <template slot-scope=""scope"">
                        <el-tag size=""small"" v-if=""scope.row.status == 0"">暂存</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.status == 1"" type=""info"">待审核</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.status == 2"" type=""danger"">审核不通过</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.status == 3"" type=""success"">正常</el-tag>
                        <el-tag size=""small"" v-i");
                WriteLiteral(@"f=""scope.row.status == 7"" type=""info"">冻结</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.status == 8"" type=""info"">淘汰</el-tag>
                        <el-tag size=""small"" v-if=""scope.row.status == 9"" type=""info"">异常</el-tag>
                    </template>
                </el-table-column>
");
                WriteLiteral(@"                <el-table-column min-width=""90"" prop=""funcCategory.name"" label=""分类"">
                    <template slot-scope=""scope"">
                        <i v-if=""scope.row.funcCategory!=null"">{{scope.row.funcCategory.eName}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""80"" prop=""factoryModel"" label=""工厂型号"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.factoryModel}}</i>
                    </template>
                </el-table-column>
                <el-table-column width=""150"" prop=""editTime"" label=""上次修改时间"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.modifyTimeString}}</i>
                    </template>
                </el-table-column>
                <el-table-column min-width=""60"" prop=""createUser"" label=""创建人"">
                    <template slot-scope=""scope"">
                        <i>{{scope.row.createUser}}</i>
      ");
                WriteLiteral(@"              </template>
                </el-table-column>
                <el-table-column width=""220"" label=""操作"">
                    <template slot-scope=""scope"">
                        <el-dropdown trigger=""click"" size=""small"">
                            <el-button type=""primary"" size=""mini"" class=""mb5"">
                                操作
                                <i class=""el-icon-caret-bottom el-icon--right""></i>
                            </el-button>
                            <el-dropdown-menu slot=""dropdown"">
                                <el-dropdown-item>
                                    <div v-on:click=""detail(scope.row.id)"">查看详情</div>
                                </el-dropdown-item>
                            </el-dropdown-menu>
                        </el-dropdown>
                    </template>
                </el-table-column>
            </el-table>
            <el-col :span=""24"" class=""toolbar"">
                <el-pagination v-on:size-change=""hand");
                WriteLiteral(@"leSizeChange"" v-on:current-change=""handleCurrentChange"" :current-page=""page"" :page-sizes=""[10,20,30,40,50]"" :page-size=""pageSize"" layout=""total, sizes, prev, pager, next, jumper"" :total=""total"" style=""float:right;"">
                </el-pagination>
            </el-col>
        </article>
    </div>
    <script>
        var Main = {
            data() {
                return {
                    listLoading: false,
                    skuList: [],
                    filters: {
                        searchKey: """",
                        isChanged: 0
                    },
                    categoryList: [],
                    categoryIdPath: [],
                    total: 30,
                    page: 1,
                    pageSize: 10,
                }
            },
            created() {
                this.getSkuList();
            },
            methods: {
                // 改变每页显示几条数据
                handleSizeChange(val) {
                    this.pageSize = val;");
                WriteLiteral(@"
                    this.getSkuList();
                },
                //【点击第几页】
                handleCurrentChange(val) {
                    this.page = val;
                    this.getSkuList();
                },
                inherite(id) {
                    window.parent.MainIndex.addTabByIframe(""sku创建产品_"" + id, ""/Manage/Product/Inherite?skuId="" + id, ""创建产品"");
                },
                createSkuCost(skuCostId, skuId) {
                    window.parent.MainIndex.addTabByIframe(""添加RGC在售产品_"" + skuCostId, ""/Sku/SkuCostDetail?&skuCostId="" + skuCostId + ""&skuId="" + skuId, ""添加RGC在售产品"");
                },
                getSkuList() {
                    this.listLoading = true;
                    var _self = this;
                    var categoryID = 0;
                    if (_self.categoryIdPath.length > 0) {
                        categoryID = _self.categoryIdPath[_self.categoryIdPath.length - 1];
                    }
                    $.ajax({
              ");
                WriteLiteral(@"          type: 'GET',
                        url: '../../rest/sku/list',
                        data: { key: _self.filters.searchKey, categoryId: categoryID, pageSize: _self.pageSize, currentPage: _self.page },
                        success: function (data) {
                            if (data.success == true) {
                                _self.total = data.page.totalCount;
                                _self.skuList = data.data;
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
                WriteLiteral(@"                                    type: 'error',
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
                    this.getSkuList();
                },
                edit(id) {
                    window.parent.MainIndex.addTabByIframe(""Sku更新_"" + id, ""/Sku/Update?id="" + id, ""Sku更新"");
                },
                detail(id) {
                    window.parent.MainIndex.addTabByIframe(""Sku详情_"" + id, ""/Sku/Detail?id="" + id, ""Sku详情"");
             ");
                WriteLiteral(@"       //window.open(""/Sku/SkuDetail?id="" + id);
                },
                update_status(index, id, status) {
                    var _self = this;
                    this.$confirm(""确定修改状态吗"", '提示', {
                        type: 'warning'
                    }).then(() => {
                        $.ajax({
                            type: 'GET',
                            url: '../../rest/sku/status/update',
                            data: { id: id, status: status },
                            success: function (data) {
                                if (data.success == true) {
                                    _self.skuList[index].status = status;
                                    _self.$notify.success({
                                        title: '成功',
                                        message: ""更新状态成功""
                                    });
                                } else {
                                    _self.$notify.error({
                  ");
                WriteLiteral(@"                      title: '错误',
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
 ");
                WriteLiteral(@"                   }).catch(() => {

                    });
                },
                del(index, id) {
                    var _self = this;
                    this.$confirm(""确定删除吗"", '提示', {
                        type: 'warning'
                    }).then(() => {
                        $.ajax({
                            type: 'GET',
                            url: '../../rest/sku/delete',
                            data: { id: id },
                            success: function (data) {
                                if (data.success == true) {
                                    _self.skuList.splice(index, 1);
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
        }
        var Ctor = Vue.extend(Main)
        new Ctor().$mount('#skulist')
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