#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Item\CompDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e79734ed2ac8d62e603e438b4f30039924dc5fd0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Item_CompDetail), @"mvc.1.0.view", @"/Views/Item/CompDetail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e79734ed2ac8d62e603e438b4f30039924dc5fd0", @"/Views/Item/CompDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Item_CompDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
#nullable restore
#line 1 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Item\CompDetail.cshtml"
  
    ViewBag.Title = "Update";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<link rel=""stylesheet"" href=""/lib/elFinder/lib/jquery-ui/themes/base/all.css"" />
<link rel=""stylesheet"" href=""/lib/elFinder/lib/elfinder/css/elfinder.full.css"" />
<link rel=""stylesheet"" href=""/lib/elFinder/lib/elfinder/css/theme.css"" />
<style>
    #nav {
        width: 100%;
        /*position: fixed;
        top: 100px;*/
        /*ie6下样式，加下划线表示只针对ie6 的hack */
        _position: absolute; /* 把导航栏位置定义为绝对位置  关键*/
        _top: expression(documentElement.scrollTop + ""px""); /* 把导航栏位置放在浏览器垂直滚动条的顶端  关键 */
        z-index: 100; /* 让导航栏浮在网页的高层位置，遇到flash和图片时候也能始终保持最外层 */
    }

    .pl90 {
        padding-left: 90px;
    }

    .el-step__title {
        font-size: 13px !important;
        line-height: 38px;
        cursor: pointer;
    }

    .w140 {
        width: 140px;
    }

    .w178 {
        width: 178px;
    }

    .w250 {
        width: 250px;
    }

    .w365 {
        width: 365px;
    }

    .w259 {
        width: 259px;
    }

    .avatar {
        width: 200");
            WriteLiteral(@"px;
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
        padding: 0;
    }

    .el-steps--simple {
        padding: 10px 6% !important;
    }

    .item_title {
        height: 35px;
        padding: 0 15px;
        margin-bottom: 20px;
        line-height: 35px;
        background-color: #E5E9F2;
        border-radius: 3px;
    }

        .item_title .el-button {
            margin-top: 4px;
        }

    .skuform .el-tabs__header {
        position: sticky;
        top: 0;
        background: white;
        z-index: 9;
        margin-top: 10px;
    }

    .savebtn {
        float: right;
        position: sticky;
        top: 4px;
        right: 10px;
        z-index: 10;
        margin-right: 10px;
    }

    .btn_baocun {
        float: right;
        position: fixed;
        top: 10px;
      ");
            WriteLiteral(@"  right: 10px;
        z-index: 10;
    }

    .skuform .el-tabs__item {
        height: 44px;
        line-height: 44px;
    }

    .hide-empty .el-table__empty-block {
        display: none;
    }

    .detail-table > .el-table__body-wrapper > table > tbody > tr > td:not(:first-child), .detail-table > .el-table__body-wrapper > table > tbody > tr > td:not(:first-child) > .cell {
        padding: 0;
        border: none;
    }

    .lianjie_textarea textarea {
        height: 76px;
    }
</style>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e79734ed2ac8d62e603e438b4f30039924dc5fd05921", async() => {
                WriteLiteral(@"
    <div id=""wrapper"">
        <!-- 标题和地址 -->
        <el-row>
            <el-col :span=""24"" class=""breadcrumb-container pl20"">
                <el-breadcrumb separator=""/"">
                    <el-breadcrumb-item><strong>电商管理</strong></el-breadcrumb-item>
                    <el-breadcrumb-item>竞品编辑</el-breadcrumb-item>
                </el-breadcrumb>
            </el-col>
        </el-row>
        <template>
            <article class=""administration-maintenance-container"" v-loading=""loading"">
                <div ref=""nav_sku"" id=""nav_sku"">
                    <el-button type=""primary"" size=""medium"" class=""btn_baocun"" v-on:click=""saveComp"" v-loading=""loading"">
                        保 存
                        <i class=""el-icon-check"" style=""margin-left:5px;""></i>
                    </el-button>
                    <el-form :inline=""true"" :model=""compProduct"" ref=""compProduct"" label-width=""300px"" class=""skuform"" label-position=""right"" size=""mini"">
                        <el-card cl");
                WriteLiteral(@"ass=""box-card pl20 mb20"" shadow=""hover"">
                            <div slot=""header"">
                                <span slot=""header"" class=""step-jump pr"" id=""sku_baseInfo"">
                                    <i class=""tip-tit""><i class=""pr z-in2 pl10"">竞品编辑</i></i>
                                </span>
                            </div>
                            <el-row>
                                <el-form-item label=""Product Name(产品名称)"" prop=""itemName"">
                                    <el-input v-model=""compProduct.name"" type=""textarea"" :rows=""1"" :autosize=""{ minRows: 2, maxRows: 4}""
                                              placeholder=""产品名称"" class=""w259"" />
                                </el-form-item>
                                <el-form-item label=""Model(型号)"" prop=""factoryModel"" :rules=""[{ required: true, message: '请输入型号', trigger: 'blur' }]"">
                                    <el-input v-model=""compProduct.factoryModel"" class=""w259""></el-input>
             ");
                WriteLiteral(@"                   </el-form-item>
                                <el-form-item label=""Brand(品牌)"" prop=""factoryModel"" :rules=""[{ required: true, message: '请输入型号', trigger: 'blur' }]"">
                                    <el-input v-model=""compProduct.brandName"" class=""w259""></el-input>
                                </el-form-item>
                            </el-row>

                            <el-row>
                                <div style=""width:574px;display:inline-block;float:left;"">
                                    <el-form-item label=""平台"" prop=""platformId"">
                                        <el-select v-model=""compProduct.platformId"" placeholder=""请选择平台"" size=""mini"" clearable class=""w259"" disabled>
                                            <el-option v-for=""item in platformlist""
                                                       :key=""item.id""
                                                       :label=""item.eName""
                                                  ");
                WriteLiteral(@"     :value=""item.id"">
                                            </el-option>
                                        </el-select>
                                    </el-form-item>
                                    <el-form-item label=""唯一标识"">
                                        <el-input v-model.trim=""compProduct.uniqueId"" class=""w259""></el-input>
                                    </el-form-item>
                                </div>
                                <div style=""width:570px;display:inline-block;"">
                                    <el-form-item label=""平台链接"">
                                        <el-input v-model=""compProduct.platformUrl"" type=""textarea"" :rows=""4""
                                                  placeholder=""产品链接"" class=""lianjie_textarea w259"" />
                                    </el-form-item>
                                </div>
                            </el-row>

                            <el-row>
                                <");
                WriteLiteral(@"el-form-item label=""Remark (产品备注)"">
                                    <el-input v-model=""compProduct.remarks"" type=""textarea"" :rows=""4""
                                              placeholder=""Remark (产品备注)"" class=""w259"" />
                                </el-form-item>
                            </el-row>
                            <el-row>
                                <el-form-item label=""Picture(产品示意图) (320*320)"" prop=""imageMain"">
                                    <div class=""m-t-10 avatar-uploader"" v-on:click=""openFileManage()"">
                                        <div class=""el-upload el-upload--text"">
                                            <img v-if=""compMainSrc"" :src=""compMainSrc"" class=""avatar"">
                                            <i v-else class=""el-icon-plus avatar-uploader-icon""></i>
                                        </div>
                                    </div>
                                </el-form-item>
                            </el-row>");
                WriteLiteral(@"
                        </el-card>
                    </el-form>

                    <!--添加界面-->
                    <el-dialog title=""继承产品"" :visible.sync=""productVisible"" :close-on-click-modal=""false"" width=""70%"">
                        <el-row class=""toolbar"">
                            <article class=""search"">
                                <section>
                                    <el-input placeholder=""关键字搜索"" size=""mini"" v-model=""filters.searchKey""
                                              v-on:keyup.enter.native=""getItemList()"" style=""width:250px"" clearable></el-input>
                                    <el-button v-on:click=""search"" size=""mini"" icon=""el-icon-search"">搜索</el-button>
                                </section>
                            </article>
                        </el-row>

                        <el-table :data=""itemList"" highlight-current-row v-loading=""listLoading"" size=""small"" class=""mt10"">
                            <el-table-column min-width");
                WriteLiteral(@"=""40"" prop=""id"" label=""ID"">
                                <template slot-scope=""scope"">
                                    <i>{{scope.row.id}}</i>
                                </template>
                            </el-table-column>
                            <el-table-column min-width=""200"" prop=""fullEName"" label=""产品名"">
                            </el-table-column>
                            <el-table-column width=""120"" label=""图片"" align=""center"">
                                <template slot-scope=""scope1"">
                                    <img v-if=""scope1.row.primaryImageSrcFull"" class=""pimg"" alt=""no image"" :src=""scope1.row.primaryImageSrcFull"" height=""50"">
                                </template>
                            </el-table-column>
                            <el-table-column prop=""status"" label=""状态"" width=""110"">
                                <template slot-scope=""scope"">
                                    <el-tag size=""small"" v-if=""scope.row.status == 0"">录入中<");
                WriteLiteral(@"/el-tag>
                                    <el-tag size=""small"" v-if=""scope.row.status == 1"" type=""info"">待审核</el-tag>
                                    <el-tag size=""small"" v-if=""scope.row.status == 2"" type=""danger"">审核不通过</el-tag>
                                    <el-tag size=""small"" v-if=""scope.row.status == 3"" type=""success"">正常</el-tag>
                                    <el-tag size=""small"" v-if=""scope.row.status == 7"" type=""info"">冻结</el-tag>
                                    <el-tag size=""small"" v-if=""scope.row.status == 8"" type=""info"">淘汰</el-tag>
                                    <el-tag size=""small"" v-if=""scope.row.status == 9"" type=""info"">异常</el-tag>
                                </template>
                            </el-table-column>
                            <el-table-column min-width=""80"" prop=""funcCategory.eName"" label=""分类"">
                                <template slot-scope=""scope"">
                                    <i v-if=""scope.row.funcCategory!=null"">{{scope.r");
                WriteLiteral(@"ow.funcCategory.eName}}</i>
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
                                </t");
                WriteLiteral(@"emplate>
                            </el-table-column>
                            <el-table-column label=""操作"" min-width=""120"" fixed=""right"">
                                <template slot-scope=""scope"">
                                    <el-button size=""mini"" type=""warning"" plain v-if=""scope.row.id!=compProduct.productId&&!compProduct.isSpecial"" v-on:click=""addItem(scope.row)"">继承产品</el-button>
                                    <p v-if=""scope.row.id==compProduct.productId&&!compProduct.isSpecial"">当前继承产品</p>

                                    <el-button size=""mini"" v-if=""compProduct.isSpecial"" v-on:click=""addItemProduct(scope.row)"">添加关联产品</el-button>
                                    <el-row>
                                        <el-button size=""mini"" v-on:click=""detail(scope.row.id)"">查看详情</el-button>

                                    </el-row>
                                </template>
                            </el-table-column>
                        </el-table>

         ");
                WriteLiteral(@"               <div class=""mt10 tr"">
                            <el-pagination layout=""prev, pager, next"" small :page-size=""10"" :total=""total"" :current-page=""page"" v-on:current-change=""handleCurrentChange""></el-pagination>
                        </div>
                    </el-dialog>
                    <el-dialog title=""附件 Attachment"" :visible.sync=""fileManageVisable"" :close-on-click-modal=""false"" top=""5%"" width=""1000px"">
                        工厂型号 : {{compProduct.factoryModel}}
                        <div ref=""elfinder"" id=""elfinder""></div>
                        <span slot=""footer"" class=""dialog- footer"" style=""display:block;text-align: center;margin-top: 20px"">
                            <el-button type=""primary"" v-on:click=""saveCompMainUrl"">设为竞品预览图</el-button>
                        </span>
                    </el-dialog>
                </div>
            </article>
        </template>
    </div>
    <script src=""/lib/elFinder/lib/jquery-ui/jquery-ui.min.js""></script>
    <scri");
                WriteLiteral(@"pt src=""/lib/elFinder/lib/elfinder/js/elfinder.full.js""></script>
    <script src=""/lib/elFinder/lib/elfinder/js/i18n/elfinder.zh_CN.js""></script>
    <script>
        var Main = {
            data() {
                return {
                    platformlist:[],
                    listLoading: false,
                    data: [],
                    previewSrc: '',
                    mainSrc: '',
                    compMainSrc: '',
                    selectedFile: '',
                    selectedFilePath: '',
                    selectedFileByte: '',
                    fileManageVisable: false,
                    active: 0,
                    dialogAdd: {
                        isShow: false,
                        label: void 0,
                        chineseLabel: void 0,
                        value: void 0,
                    },
                    loading: false,
                    dialogVisible: this.visible,
                    compProduct: {
                    ");
                WriteLiteral(@"    status: 0,
                        name: void 0,
                        factoryModel: void 0,
                        brandName: void 0,
                        remarks: void 0,
                        src: void 0,
                        platformId: void 0,
                        imageMain: void 0,
                        productId: 0,
                        uniqueId: void 0,
                        platformUrl: void 0,
                        itemId: void 0,
                        id: 0,
                    },
                    title: '',
                    compProductList: [],
                    filters: {
                        searchKey: """",
                        isChanged: 0,
                        type: void 0,
                        status: void 0
                    },
                    total: 0,
                    page: 1,
                    pageSize: 10,
                    total1: 30,
                    page1: 1,
                    pageSize1: 10,
");
                WriteLiteral(@"                    no: 0,
                    itemList: [],
                    packageId: 0,
                    visible:false,
                    productVisible:false,
                }
            },
            created() {
                this.GetPlatformList();
                this.GetComp();
            },
            methods: {
                getItemIncludeList() {
                    var _self = this;
                    var id = 0;
                    if (!_self.itemForm.id > 0) {
                        id = 0
                    } else {
                        id = _self.itemForm.id;
                    }
                    _self.listLoading = true;
                    $.ajax({
                        type: 'GET',
                        dataType: 'json',
                        url: '../../rest/item/list',
                        data: { platformId: id, key: _self.itemForm.key, pageSize: _self.pageSize, currentPage: _self.page2, itemId: _self.itemForm.itemId, status: ");
                WriteLiteral(@"_self.itemForm.status },
                        success: function (data) {
                            if (data.success == true) {
                                _self.total = data.data.page.totalCount;
                                _self.itemList = data.data.list;
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
             ");
                WriteLiteral(@"               } else {
                                _self.$message({
                                    type: 'error',
                                    message: errorThrown
                                });
                            }
                        },
                    complete(){
                        _self.listLoading = false;
                    }
                    });
                },

                GetPlatformList() {
                    var _self = this;
                    $.ajax({
                        type: 'GET',
                        dataType: 'json',
                        url: '/rest/item/platform/list',
                        success: function (data) {
                            if (data.success == true) {
                                _self.platformlist = data.data;
                            } else {
                                _self.$notify.error({
                                    title: '错误',
                          ");
                WriteLiteral(@"          message: data.msg
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

                GetComp() {
                    var compId = ");
#nullable restore
#line 438 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Item\CompDetail.cshtml"
                            Write(ViewBag.compId);

#line default
#line hidden
#nullable disable
                WriteLiteral(@";
                    var _self = this;
                    $.ajax({
                        type: 'GET',
                        url: '/rest/item/competition/get',
                        data: { id: compId },
                        success: function (data) {
                            if (data.success == true) {
                                _self.compProduct = data.data;
                                _self.compMainSrc = data.data.srcFull;
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
                       ");
                WriteLiteral(@"         _self.$message({
                                    type: 'error',
                                    message: errorThrown
                                });
                            }
                        }
                    });
                }, 
                saveComp() {
                    var _self = this;
                    console.log(_self.compProduct)
                    var _valid = false;
                    this.$refs['compProduct'].validate((valid) => {
                        if (valid) {
                            _valid = true;
                        }
                    });
                    if (!_valid) {
                        return false;
                    }

                    _self.loading=true
                    $.ajax({
                        type: 'post',
                        dataType: 'json',
                        contentType: 'application/json',
                        url: '/rest/item/competition/update',
        ");
                WriteLiteral(@"                data: JSON.stringify(_self.compProduct),
                        success: function (data) {
                            if (data.success) {
                                _self.$message({
                                    type: 'success',
                                    message: '成功!'
                                });
                            }
                            else {
                                _self.$message({
                                    type: 'error',
                                    message: data.msg
                                });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThrown == ""Unauthorized"") {
                                window.parent.MainIndex.openLoginFrom();
                                _self.$message({
                                    type: 'error',
                         ");
                WriteLiteral(@"           message: XMLHttpRequest.responseJSON.msg
                                });
                            } else {
                                _self.$message({
                                    type: 'error',
                                    message: errorThrown
                                });
                            }
                        },
                        complete(){
                            _self.loading=false;
                        }
                    });
                },
                handleSizeChange(val) {
                    this.pageSize = val;
                    this.getProductList();
                },
                //【点击第几页】
                handleCurrentChange(val) {
                    this.page = val;
                    this.getProductList();
                },
                getProductList() {
                    this.listLoading = true;
                    this.productVisible = true;
                    var _self ");
                WriteLiteral(@"= this;
                    $.ajax({
                        type: 'GET',
                        url: '../../rest/product/list',
                        data: { key: _self.filters.searchKey, categoryId: 0, pageSize: _self.pageSize, currentPage: _self.page },
                        success: function (data) {
                            if (data.success == true) {
                                _self.total = data.data.page.totalCount;
                                _self.productList = data.data.list;
                            } else {
                                _self.$notify.error({
                                    title: '错误',
                                    message: data.msg
                                });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThrown == ""Unauthorized"") {
                                window.parent.MainIndex.openLo");
                WriteLiteral(@"ginFrom();
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
                        complete(){
                            _self.listLoading = false;
                        }
                    });
                },
                search() {
                    this.getProductList();
                },
                openFileManage() {
                    this.fileManageVisable = true;
                    //selectedFilePath = '';
                    setTimeout(() => {
                        this.fileManageInitial();
                   ");
                WriteLiteral(@" }, 0)
                },
                saveCompMainUrl() {
                    //console.log(this.mainSrc);
                    if (selectedFilePath != undefined && selectedFilePath != """") {
                        this.compProduct.imageMain = selectedFilePath;
                        //document.getElementById(""mainSrc"").src = mainSrc;
                        this.compMainSrc = previewSrc;
                        this.fileManageVisable = false;
                    } else {
                        this.$message({
                            type: 'error',
                            message: '请选择图片'
                        });
                    }
                },
                fileManageInitial() {
                    var _self = this;
                    var myCommands = elFinder.prototype._options.commands;
                    var disabled = ['callback', 'chmod', 'editor', 'netmount', 'ping', 'search', 'zipdl', 'help']; // Not yet implemented commands in elFinder.NetCore
        ");
                WriteLiteral(@"            elFinder.prototype.i18.en.messages.TextArea = ""Edit"";

                    $.each(disabled, function (i, cmd) {
                        (idx = $.inArray(cmd, myCommands)) !== -1 && myCommands.splice(idx, 1);
                    });

                    var options = {
                        url: '/product/connector', // Default (Local File System)
");
                WriteLiteral(@"                        rememberLastDir: false, // Prevent elFinder saving in the Browser LocalStorage the last visited directory
                        commands: myCommands,
                        lang: 'zh_CN', // elFinder supports UI and messages localization. Check the folder Content\elfinder\js\i18n for all available languages. Be sure to include the corresponding .js file(s) in the JavaScript bundle.
                        uiOptions: { // UI buttons available to the user
                            toolbar: [
                                ['back', 'forward'],
                                ['reload'],
                                ['home', 'up'],
                                ['mkdir', 'mkfile', 'upload'],
                                ['open', 'download'],
                                ['undo', 'redo'],
                                ['info'],
                                ['quicklook'],
                                ['copy', 'cut', 'paste'],
                           ");
                WriteLiteral(@"     ['rm'],
                                ['duplicate', 'rename', 'edit'],
                                ['selectall', 'selectnone', 'selectinvert'],
                                ['view', 'sort']
                            ]

                        }, handlers: {
                            select: function (event, elfinderInstance) {
                                if (event.data.selected.length == 1) {
                                    var item = $('#' + event.data.selected[0]);
                                    if (!item.hasClass('directory')) {
                                        selectedFile = event.data.selected[0];
                                        previewSrc = ""/product/connector?cmd=file&target="" + selectedFile + ""&_t="" + event.timeStamp;

                                        $.ajax({
                                            type: 'post',
                                            url: '/product/selectFile',
                                            d");
                WriteLiteral(@"ata: { target: selectedFile },
                                            success: function (data) {
                                                selectedFilePath = data;
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
                ");
                WriteLiteral(@"                                        message: errorThrown
                                                    });
                                                }
                                            }
                                        });
                                        return;
                                    }
                                }
                                selectedFile = null;
                            }
                        }

                    };
                    $('#elfinder').elfinder(options).elfinder('instance');
                },
            },
            components: {

            },
            mounted() {
                this.$nextTick(function () {
                    // window.addEventListener('scroll', this.onScroll)
                })
                //this.fetchData();
            }
                }
        var Ctor = Vue.extend(Main);
        new Ctor().$mount('#wrapper');
    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
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
