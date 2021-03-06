#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Order\SaleOrderDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "84551dfeed15f0546977b55ad6296656db9eea3a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_SaleOrderDetail), @"mvc.1.0.view", @"/Views/Order/SaleOrderDetail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"84551dfeed15f0546977b55ad6296656db9eea3a", @"/Views/Order/SaleOrderDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_SaleOrderDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Order\SaleOrderDetail.cshtml"
  
    ViewData["Title"] = "SSOrderDetail";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>

    .mg5 {
        margin: 5px;
    }

    .mg15 {
        margin: 15px;
    }

    .mgr10 {
        margin-right: 10px;
    }

    .wd1350 {
        width: 1350px;
    }

    .wd400 {
        width: 400px;
    }

    .wd300 {
        width: 300px;
    }

    .wd200 {
        width: 200px;
    }

    .wd150 {
        width: 150px;
    }

    .wd120 {
        width: 120px;
    }

    .wd100 {
        width: 100px;
    }

    .c01 {
        background-color: #000000;
    }

    .title {
        font-size: 20px;
        font-weight: bold;
        line-height: 20px;
        height: 40px;
        border-bottom: 2px solid #303133;
        margin-bottom: 5px;
    }

    .fl {
        float: left;
    }

    .tar {
        text-align: right;
    }

    .noprint {
        visibility: hidden
    }

    .isBold {
        font-weight: bold
    }

    .underline {
        border-bottom: 1px solid #dbdbdb;
        text-align: center
    }
");
            WriteLiteral(@"
    .el-row {
        margin-bottom: 5px;
    }

    .wrapper-1 {
        float: left;
        width: 250px;
    }

    .listStyle {
        list-style-type: none;
        float: left
    }

        .listStyle li {
            margin-bottom: 3px;
        }

            .listStyle li:last-child {
                margin-bottom: 0px;
            }

    .listStyle2 {
        list-style-type: none;
        font-weight: bold;
    }

        .listStyle2 li {
            margin-bottom: 3px;
        }

            .listStyle2 li:last-child {
                margin-bottom: 0px;
            }

    .left {
        float: left;
        width: 100px;
        text-align: right;
        padding-right: 5px;
    }

    .right {
        float: left
    }

    .listStyle3 {
        list-style-type: none;
        padding-left: 5px;
    }

    .wrapper-2 {
        float: left;
        width: 300px;
        margin-left: 50px;
    }

    .wrapper-3 {
        float: left;
  ");
            WriteLiteral("      width: 200px;\r\n        margin-left: 60px;\r\n    }\r\n\r\n    .wrapper-4 {\r\n        float: right;\r\n        width: 200px;\r\n    }\r\n</style>\r\n\r\n<div id=\"userlist\" class=\"p15 bac01 h100b bsb\">\r\n    <el-card class=\"el-card--plr\">\r\n");
            WriteLiteral(@"
        <template>
            <!--startprint-->
            <div id=""printout"">
                <h1 class=""title"">Order Summary Report </h1>
                <div style=""overflow:hidden"">
                    <div class=""wrapper-1"">
                        <ul class=""listStyle"">
                            <li>{{ orderInfo.shipToName  }}</li>
                            <li>{{ orderInfo.shipToCompany  }}</li>
                            <li>{{ orderInfo.shipToAddress1  }}</li>
                            <li v-if=""orderInfo.shipToAddress2 != ''"">{{ orderInfo.shipToAddress2  }}</li>
                            <li>{{ orderInfo.shipToCity  }}, {{ orderInfo.shipToState  }} {{ orderInfo.shipToZipcode  }} {{ orderInfo.shipToCountry  }}</li>
                        </ul>
                    </div>
                    <div class=""wrapper-2"">
                        <div class=""left"">
                            <ul class=""listStyle2"">
                                <li>Date Paid:</li>
           ");
            WriteLiteral(@"                     <li>Amount Paid:</li>
                                <li>Record #:</li>
                                <li>Store:</li>
                            </ul>
                        </div>
                        <div class=""right"">
                            <ul class=""listStyle3"">
                                <li>{{ orderInfo.paymentDateString}}</li>
                                <li>${{ orderInfo.amountPaidString | capitalize}}</li>
                                <li>{{ orderInfo.orderNumber}}</li>
                                <li></li>
                            </ul>
                        </div>
                    </div>
                    <div class=""wrapper-3"">
                        <div class=""underline"">
                            <span class=""isBold"">Buyer Comments</span>
                        </div>
                    </div>
                    <div class=""wrapper-4"">
                        <div class=""underline"">
                        ");
            WriteLiteral(@"    <span class=""isBold"">Internal Notes</span>
                        </div>
                    </div>

                </div>




                <div>
                    <el-table :data=""orderInfo.items"" style=""width: 1350px;"">
                        <el-table-column prop=""factoryModel"" label=""Item ID"" width=""180""></el-table-column>
                        <el-table-column prop=""name"" label=""Title"" min-width=""200""></el-table-column>
                        <el-table-column prop=""unitPriceString"" label=""Unit Price"" width=""180"">
                            <template slot-scope=""scope"">
                                ${{scope.row.unitPriceString | capitalize}}
                            </template>
                        </el-table-column>
                        <el-table-column prop=""qty"" label=""Qty"" width=""180""></el-table-column>
                        <el-table-column prop=""extPriceString"" label=""Ext Price"" width=""180"">
                            <template slot-scope=""scope"">
");
            WriteLiteral(@"                                ${{scope.row.extPriceString | capitalize}}
                            </template>
                        </el-table-column>
                    </el-table>
                </div>
            </div>
            <!--endprint-->
            <div style=""text-align:left;margin-bottom:20px;margin-top:20px"">
                <el-button size=""mini"" type=""primary"" v-on:click=""f_print""");
            BeginWriteAttribute("class", " class=\"", 6209, "\"", 6217, 0);
            EndWriteAttribute();
            WriteLiteral(">打印</el-button>\r\n            </div>\r\n            <iframe id=\"printf\"");
            BeginWriteAttribute("src", " src=\"", 6286, "\"", 6292, 0);
            EndWriteAttribute();
            WriteLiteral(@" width=""0"" height=""0"" frameborder=""0""></iframe>

        </template>
    </el-card>
</div>
<script src=""/js/canvg.js""></script>
<script src=""/js/html2canvas.js""></script>
<script src=""/js/jspdf.debug.js""></script>
<script language=""javascript"" type=""text/javascript"" src=""/js/LodopFuncs.js""></script>
<object id=""LODOP_OB"" classid=""clsid:2105C259-1E0C-4534-8141-A753534CB4CA"" width=""0"" height=""0"">      
    <embed id=""LODOP_EM"" type=""application/x-print-lodop"" width=""0"" height=""0"" pluginspage=""Lodop/install_lodop32.exe""></embed>   
 </object>

<script>


    var main = {
        data() {
            return {
                activeName: ""first"",
                orderInfo: {},
                orderLoading: false,

                height_table: 650,//表格高度
                // 分页
                total: 0,
                currentPage: 1,
                pageSize: 10,
                itemListLoading: false,//表格loading
                selectList: [],


            }
        },
         f");
            WriteLiteral(@"ilters: {
          capitalize: function (x) {
              var f = parseFloat(x);
             if (isNaN(f)) {
                return false;
             }
             var f = Math.round(x*100)/100;
             var s = f.toString();
             var rs = s.indexOf('.');
             if (rs < 0) {
                 rs = s.length;
                 s += '.';
             }
             while (s.length <= rs + 2) {
                s += '0';
             }
             return s;
          }
        },
        methods: {

            getOrderDetail() {
                var _self = this;
                //_self.itemListLoading = true;
                var filter = {};
                filter.id =");
#nullable restore
#line 289 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Order\SaleOrderDetail.cshtml"
                      Write(ViewBag.orderId);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n\r\n                $.ajax({\r\n                    type: \'GET\',\r\n                    //dataType: \'json\',\r\n                    //contentType: \'application/json\',\r\n                    url: \'/rest/saleorder/detail/get?orderId=");
#nullable restore
#line 295 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Order\SaleOrderDetail.cshtml"
                                                        Write(ViewBag.orderId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                    //data: JSON.stringify(filter),
                    success: function (data) {
                        _self.orderInfo = data.data;
                        //_self.itemListLoading = false;
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        if (XMLHttpRequest.status == 401 || errorThrown == ""Unauthorized"") {
                            window.parent.Main.openLoginFrom();
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
                        //_self.itemListLoading = false;
                    }
                });");
            WriteLiteral(@"
            }
        },
        created() {
            this.getOrderDetail();
        }
    };
    var Ctor = Vue.extend(main)
    new Ctor().$mount('#userlist')

    function printDiv() {
        var divElements = document.getElementById('printout').innerHTML;
        //Get the HTML of whole page
        var oldPage = document.body.innerHTML;
        //Reset the page's HTML with div's HTML only
        document.body.innerHTML =
            ""<html><head><title></title></head><body>"" +
            divElements + ""</body>"";
        //Print Page
        window.print();
        //Restore orignal HTML
        document.body.innerHTML = oldPage;
        location.reload();//重置按钮事件 解决打印按钮 点击后失效问题


    }


    var LODOP;

    function f_print() {

        LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));

        LODOP.SET_PRINT_PAGESIZE(1, 0, 0, ""A4""); //规定纸张大小

        LODOP.SET_PRINT_STYLE(""FontColor"", ""red""); //字体颜色        

        ");
            WriteLiteral(@"LODOP.SET_PRINT_STYLE(""FontSize"", 14); //字体大小

        for (var a = 0; a < 3; a++) {

            var number = a + 1;

            sTmp = ""第"" + number + ""页，共3页"";

            LODOP.ADD_PRINT_TEXT(100, 100, 400, 39, ""页数："" + sTmp);

            LODOP.NEWPAGE();//强制分页

        }

        LODOP.PREVIEW(); //打印预览

    }

</script>
");
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
