#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\SkuCostUpdate.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f69c1de36c6fad6cd73f833d68f96bd699a7cac3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sku_SkuCostUpdate), @"mvc.1.0.view", @"/Views/Sku/SkuCostUpdate.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f69c1de36c6fad6cd73f833d68f96bd699a7cac3", @"/Views/Sku/SkuCostUpdate.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Sku_SkuCostUpdate : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 1 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\SkuCostUpdate.cshtml"
  
    ViewBag.Title = "Add";

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

    .w183 {
        width: 183px;
    }

    .w250 {
        width: 250px;
    }

    .w365 {
        width: 365px;
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

    .skuform .el-tabs__item {
        height: 44px;
        line-height: 44px;
    }

   ");
            WriteLiteral(@" .hide-empty .el-table__empty-block {
        display: none;
    }

    .detail-table > .el-table__body-wrapper > table > tbody > tr > td:not(:first-child), .detail-table > .el-table__body-wrapper > table > tbody > tr > td:not(:first-child) > .cell {
        padding: 0;
        border: none;
    }
    /*.el-input__inner{
        width:183px;
    }*/
    .maidian_scroll::-webkit-scrollbar { /*滚动条整体样式*/
        width: 6px !important;
        ; /*高宽分别对应横竖滚动条的尺寸*/
        height: 6px !important;
        ;
        background: #ffffff !important;
        ;
        cursor: pointer !important;
    }

    .maidian_scroll::-webkit-scrollbar-thumb { /*滚动条里面小方块*/
        border-radius: 5px !important;
        -webkit-box-shadow: inset 0 0 5px rgba(240, 240, 240, .5) !important;
        ;
        background: rgba(0, 0, 0, 0.8) !important;
        ;
        cursor: pointer !important;
    }

    .maidian_scroll::-webkit-scrollbar-track { /*滚动条里面轨道*/
        -webkit-box-shadow: inset 0 0 5px r");
            WriteLiteral("gba(240, 240, 240, .5) !important;\r\n        ;\r\n        border-radius: 0 !important;\r\n        ;\r\n        background: rgba(240, 240, 240, 0.5) !important;\r\n        ;\r\n        cursor: pointer !important;\r\n    }\r\n</style>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f69c1de36c6fad6cd73f833d68f96bd699a7cac36698", async() => {
                WriteLiteral(@"
    <div id=""wrapper"">
        <!-- 标题和地址 -->
        <el-row>
            <el-col :span=""24"" class=""breadcrumb-container"" style=""padding-left:20px;"">
                <el-breadcrumb separator=""/"">
                    <el-breadcrumb-item><strong>产品管理</strong></el-breadcrumb-item>
                    <el-breadcrumb-item>修改RGC在售产品</el-breadcrumb-item>
                </el-breadcrumb>
            </el-col>
        </el-row>
        <template>
            <article class=""administration-maintenance-container"" v-loading=""loading"">
                <div ref=""nav_sku"" id=""nav_sku"">
                    <el-form :inline=""true"" :model=""currSkuCost"" ref=""currSkuCost"" ");
                WriteLiteral(@" label-width=""300px"" class=""skuform"" label-position=""right"" size=""mini"">

                        <!-- 选择SKU -->

                        <el-card class=""box-card"" shadow=""hover"" :body-style=""{ padding: '0px' }"">
                            <div slot=""header"">
                                <span class=""step-jump pr ml20"">
                                    <i class=""tip-tit"">
                                        <i class=""pr z-in2 pl10"">产品信息</i>
                                    </i>
                                </span>
                            </div>
                            <el-row>
                                <el-form-item label=""Sku"" prop=""skuFactoryModel"" label-width=""315px"">
                                    <el-input v-model.trim=""currSkuCost.skuFactoryModel"" :disabled=""true"" style=""width:183px;"">
                                    </el-input>
                                </el-form-item>
                            </el-row>
                            <el-r");
                WriteLiteral("ow v-if=\"bulletPoint != undefined && bulletPoint.length >0\">\r\n                                <el-form-item label=\"卖点\" :disabled=\"true\" prop=\"bulletPoint\" label-width=\"315px\">\r\n");
                WriteLiteral(@"                                    <el-card style=""padding: 5px 0 5px 30px;"">
                                        <p style=""overflow-y:auto;height:150px;width:480px;line-height:24px;"" class=""maidian_scroll"" v-html=""currSkuCost.sku.bulletPoint""></p>
                                    </el-card>
                                </el-form-item>
                            </el-row>
                            <el-row>
                                <el-form-item label=""Remark (产品备注)"" label-width=""312px"">
                                    <el-input v-model=""currSkuCost.remark"" type=""textarea"" :rows=""4""
                                              placeholder=""Remark (产品备注)"" class=""w365"" />
                                </el-form-item>
                            </el-row>
                        </el-card>

                        <!-- 成本信息 -->

                        <el-card class=""box-card"" shadow=""hover"" style=""margin-top:20px;padding-right:20px;"" :body-style=""{ padding: '0px' }"">
");
                WriteLiteral(@"                            <div slot=""header"">
                                <span class=""step-jump pr ml20"">
                                    <i class=""tip-tit"">
                                        <i class=""pr z-in2 pl10"">成本信息</i>
                                    </i>
                                </span>
                            </div>
                            <el-form-item label=""FOB"">
                                $ <el-input-number class=""minw250"" placeholder=""0.00"" :controls=""false"" :precision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.fobString""></el-input-number>
                            </el-form-item>
                            <el-form-item label=""DDP"">
                                $ <el-input-number class=""minw250"" placeholder=""0.00"" :controls=""false"" :precision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.ddpString""></el-input-number>
                            </el-form-item>
                            <el-form-item label=""海运费"">
                        ");
                WriteLiteral(@"        $ <el-input-number class=""minw250"" placeholder=""0.00"" :controls=""false"" :precision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.seaFreightString""></el-input-number>
                            </el-form-item>
                            <el-form-item label=""卸柜费"">
                                $ <el-input-number class=""minw250"" placeholder=""0.00"" :controls=""false"" :precision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.unloadingChargeString""></el-input-number>
                            </el-form-item>
                            <el-form-item label=""ELC"" prop=""elcString"">
                                $ <el-input-number class=""minw250"" placeholder=""0.00"" :controls=""false"" :precision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.elcString""></el-input-number>
                            </el-form-item>
                            <el-form-item label=""Z3 Freight(Z3运费)"" prop=""z3FreightString"">
                                $ <el-input-number class=""minw250"" placeholder=""0.00"" :controls=""false"" :prec");
                WriteLiteral(@"ision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.z3FreightString""></el-input-number>
                            </el-form-item>
                            <el-form-item label=""Z5 Freight(Z5运费)"" prop=""z5FreightString"">
                                $ <el-input-number class=""minw250"" placeholder=""0.00"" :controls=""false"" :precision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.z5FreightString""></el-input-number>
                            </el-form-item>
                            <el-form-item label=""MSRP"" prop=""msrpString"">
                                $ <el-input-number class=""minw250"" :controls=""false"" placeholder=""0.00"" :precision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.msrpString""></el-input-number>
                            </el-form-item>
                            <el-form-item label=""MAP"" prop=""mapString"">
                                $ <el-input-number class=""minw250"" :controls=""false"" placeholder=""0.00"" :precision=""2"" :min=""0.00"" v-model.trim=""currSkuCost.mapString""></el-input-nu");
                WriteLiteral(@"mber>
                            </el-form-item>

                        </el-card>

                        <!-- 添加批次 -->

                        <el-card class=""box-card"" shadow=""hover"" style=""margin-top:20px;margin-bottom:20px;padding-bottom:10px;"" :body-style=""{ padding: '10px' }"">
                            <div slot=""header"">
                                <span class=""step-jump pr ml20"">
                                    <i class=""tip-tit"">
                                        <i class=""pr z-in2 pl10"">批次列表</i>
                                    </i>
                                </span>
                                <el-button size=""small"" v-on:click=""addBatch()"" type=""primary"" style=""float:right;margin-right:7px;"">添加批次<i class=""el-icon-circle-plus-outline"" style=""margin-left:5px;""></i></el-button>
                            </div>
                            <div v-if=""currSkuCost.isAddBatch == true"">
");
                WriteLiteral("\r\n                                <el-table class=\"data-table\" v-loading=\"batchListLoading\" :data=\"batchList\" :fit=\"true\" border>\r\n");
                WriteLiteral(@"                                    <el-table-column min-width=""200"" prop=""batchNo"" label=""批次号"">
                                        <template slot-scope=""scope"">
                                            <el-input placeholder=""批次号"" width=""100px"" v-model=""scope.row.batchNo"" auto-complete=""off"" icon=""edit"">
                                            </el-input>
                                        </template>
                                    </el-table-column>
                                    <el-table-column prop=""status"" min-width=""110"" label=""状态"">
                                        <template slot-scope=""scope"">
                                            <el-select v-model=""scope.row.status"" style=""width:100%"" placeholder=""请选择状态"">
                                                <el-option v-for=""item in statusList""
                                                           :key=""item.value""
                                                           :label=""item.label""
      ");
                WriteLiteral("                                                     :value=\"item.value\">\r\n                                                </el-option>\r\n");
                WriteLiteral(@"                                            </el-select>
                                        </template>
                                    </el-table-column>
                                    <el-table-column min-width=""200"" prop=""remark"" label=""备注"">
                                        <template slot-scope=""scope"">
                                            <el-input placeholder=""备注"" v-model=""scope.row.remark"" auto-complete=""off"" icon=""edit"">
                                            </el-input>
                                        </template>
                                    </el-table-column>
                                    <el-table-column width=""100"" label=""操作"">
                                        <template slot-scope=""scope"">
                                            <el-button type=""danger"" plain size=""small"" class=""waves-effect mb5"" v-on:click=""deleteSkuBatch(scope.$index)"">
                                                删除<i class=""el-icon-delete"" style=""mar");
                WriteLiteral(@"gin-left:5px;""></i>
                                            </el-button>
                                        </template>
                                    </el-table-column>
                                </el-table>
                            </div>
                        </el-card>
                        <div style=""text-align:center;margin-bottom:20px;"">
                            <el-button type=""primary"" size=""small"" v-on:click=""save()"" v-loading=""loading"">
                                保 存
                                <i class=""el-icon-check"" style=""margin-left:5px;""></i>
                            </el-button>
                        </div>
                </div>
            </article>
        </template>
    </div>

    <script type=""text/javascript"" src=""/lib/ckeditor/ckeditor.js""></script>
    <script>
        var Main = {
            data() {
                return {
                    loading: false,
                    listLoading: false,
           ");
                WriteLiteral(@"         skuCostId: 0,
                    statusList: [
                        { label: '在制', value: 0 },
                        { label: '在途', value: 1 },
                        { label: '已到货', value: 2 },
                        { label: '在售', value: 3 },
                        { label: '售罄', value: 4 },
                    ],
                    currSkuCost: {
                        id: 0,
                        skuCostId: 0,
                        skuId: 0,
                        productId: 0,
                        sku: void 0,
                        ddpString: '',
                        fobString: '',
                        seaFreightString: '',
                        unloadingChargeString: '',
                        elcString: '',
                        z3FreightString: '',
                        z5FreightString: '',
                        msrpString: '',
                        mapString: '',
                        remark: void 0,
                        skuF");
                WriteLiteral(@"actoryModel: void 0,
                        skuCostBatchList: [],
                        isAddBatch: false,

                    },
                    batchListLoading: false,
                    //rules: {
                    //    elcString: [
                    //        { required: true, message: '请输入ELC', trigger: 'change' },
                    //        { type: 'number', message: 'ELC必须为数字值'}
                    //    ],
                    //    z3FreightString: [
                    //        { required: true, message: '请输入Z3运费', trigger: 'change' },
                    //         { type: 'number', message: 'Z3运费必须为数字值'}
                    //    ],
                    //    z5FreightString: [
                    //        { required: true, message: '请输入Z5运费', trigger: 'change' },
                    //         { type: 'number', message: 'Z5运费必须为数字值'}
                    //    ],
                    //    msrpString: [
                    //        { required: true, message: '");
                WriteLiteral(@"请输入MSRP', trigger: 'change' },
                    //         { type: 'number', message: 'MSRP必须为数字值'}
                    //    ],
                    //    mapString: [
                    //        { required: true, message: '请输入MAP', trigger: 'change' },
                    //         { type: 'number', message: 'MAP必须为数字值'}
                    //    ],

                    //},
                    skuList: [],
                    skuListVisable: false,
                    skuListLoading: false,
                    filters: {
                        searchKey: """",
                        isChanged: 0,
                        type: void 0,
                        status: void 0
                    },
                    total: 0,
                    page: 1,
                    pageSize: 10,
                    //BatchList: [],
                    batchList: [],
                    addBatchList: [],
                    bulletPoint: void 0,

                }
            },
      ");
                WriteLiteral(@"      created() {

            },
            methods: {
                addBatch() {
                    this.batchList.push(
                        {
                            id: 0,
                            batchNo: '',
                            status: void 0,
                            remark: void 0,
                        }
                    )
                },
                deleteSkuBatch(index) {
                    this.$confirm('确定要删除该批次吗?', '提示', {
                        type: 'warning',
                    }).then(() => {
                        this.batchList.splice(index, 1);
                    })

                },

                getSkuCostDetail(skuCostId) {
                    var _self = this;
                    if (skuCostId <= 0) {
                        return;
                    }
                    $.ajax({
                        type: 'GET',
                        url: '../../rest/sku/cost/detail',
                        data: ");
                WriteLiteral(@"{ skuCostId: skuCostId },
                        success: function (data) {
                            if (data.success == true) {
                                _self.currSkuCost = data.data
                                _self.currSkuCost.skuFactoryModel = data.data.sku.factoryModel;
                                _self.batchList = data.data.skuCostBatchList;
                                console.log(_self.batchList);
                                _self.currSkuCost.productId = data.data.productId;
                                _self.bulletPoint = data.data.sku.bulletPoint;

                            } else {
                                _self.$notify.error({
                                    title: '错误',
                                    message: data.msg
                                });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (errorThro");
                WriteLiteral(@"wn == ""Unauthorized"") {
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
                save() {
                    var _valid = false;
                    var _self = this;
                    _self.currSkuCost.skuCostBatchList = [];
                    //if (_self.currSkuCost.isAddBatch) {
                    for (var i = 0; i < _self.batchList.length; i++) {
                        if (_self.batchList[i].batchNo == """" || _self.b");
                WriteLiteral(@"atchList[i].batchNo == undefined || _self.batchList[i].batchNo == null) {
                            //_self.batchList.splice(i, 1);
                            //_self.$message({
                            //    type: 'error',
                            //    message: '批次号不能为空'
                            //});
                            //return false;
                        }
                        else {
                            _self.currSkuCost.skuCostBatchList.push(_self.batchList[i]);
                        }
                    }
                    //}
                    //console.log(_self.currSkuCost)
                    //return;
                    this.$refs['currSkuCost'].validate((valid) => {
                        if (valid) {
                            _valid = true;

                        }
                    });
                    console.log(_valid)
                    if (_valid) {
                        //return false;

                       ");
                WriteLiteral(@" _self.loading = true

                        try {

                            _self.currSkuCost.ddpString = _self.currSkuCost.ddpString.toString();
                            _self.currSkuCost.fobString = _self.currSkuCost.fobString.toString();
                            _self.currSkuCost.seaFreightString = _self.currSkuCost.seaFreightString.toString();
                            _self.currSkuCost.unloadingChargeString = _self.currSkuCost.unloadingChargeString.toString();
                            _self.currSkuCost.elcString = _self.currSkuCost.elcString.toString();
                            _self.currSkuCost.z3FreightString = _self.currSkuCost.z3FreightString.toString();
                            _self.currSkuCost.z5FreightString = _self.currSkuCost.z5FreightString.toString();
                            _self.currSkuCost.msrpString = _self.currSkuCost.msrpString.toString();
                            _self.currSkuCost.mapString = _self.currSkuCost.mapString.toString();

         ");
                WriteLiteral(@"                   $.ajax({
                                type: 'POST',
                                contentType: 'application/json',
                                url: '../../rest/sku/cost/update',
                                data: JSON.stringify(_self.currSkuCost),
                                success: function (data) {
                                    if (data.success == true) {
                                        _self.$notify({
                                            title: '成功',
                                            message: '修改成功！',
                                            type: 'success'
                                        });
                                        setTimeout(function () {
                                            window.parent.MainIndex.removeTab(""修改RGC在售产品_"" + _self.skuCostId);

                                        }, 1500)
                                        //_self.clear();
                                        //_");
                WriteLiteral(@"self.fetchData();
                                    }
                                    else {
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
                WriteLiteral(@"                            type: 'error',
                                            message: errorThrown
                                        });
                                    }
                                },
                                complete(){
                                    _self.loading = false;
                                }
                            });
                        }
                        catch (e) {
                            _self.loading = false;
                            _self.$message({
                                type: 'error',
                                message: e
                            });
                        }

                    }
                },
                clear() {
                    var _self = this;
                    //清空缓存
                    _self.currSkuCost = {
                        id: 0,
                        skuCostId: 0,
                        skuId: 0,
                   ");
                WriteLiteral(@"     productId: 0,
                        sku: void 0,
                        ddpString: 0,
                        fobString: 0,
                        seaFreightString: 0,
                        unloadingChargeString: 0,
                        elcString: 0,
                        z3FreightString: 0,
                        z5FreightString: 0,
                        msrpString: 0,
                        mapString: 0,
                        remark: void 0,
                    }

                },

            },
            components: {

            },
            mounted() {
                this.skuCostId = getQueryString(""skuCostId"");
                //编辑 有Id
                if (this.skuCostId != 0) {
                    this.getSkuCostDetail(this.skuCostId);
                }
            }
        }
        var Ctor = Vue.extend(Main);
        new Ctor().$mount('#wrapper');

        function getQueryString(name) {
            var reg = new RegExp(""(^|&)"" + name + ");
                WriteLiteral("\"=([^&]*)(&|$)\", \"i\");\r\n            var r = window.location.search.substr(1).match(reg);\r\n            if (r != null) return unescape(r[2]); return null;\r\n        }\r\n    </script>\r\n");
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