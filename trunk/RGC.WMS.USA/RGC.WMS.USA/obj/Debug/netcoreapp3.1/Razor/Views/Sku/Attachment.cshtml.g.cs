#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\Attachment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "35d1aeb0c6b18493bfb70fd4119644a9386fce18"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sku_Attachment), @"mvc.1.0.view", @"/Views/Sku/Attachment.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35d1aeb0c6b18493bfb70fd4119644a9386fce18", @"/Views/Sku/Attachment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Sku_Attachment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 2 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Sku\Attachment.cshtml"
  
    ViewBag.Title = "FileManage";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<link rel=\"stylesheet\" href=\"/lib/elFinder/lib/jquery-ui/themes/base/all.css\" />\r\n<link rel=\"stylesheet\" href=\"/lib/elFinder/lib/elfinder/css/elfinder.full.css\" />\r\n<link rel=\"stylesheet\" href=\"/lib/elFinder/lib/elfinder/css/theme.css\" />\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "35d1aeb0c6b18493bfb70fd4119644a9386fce183910", async() => {
                WriteLiteral("\r\n    <div id=\"wrapper\">\r\n        <!-- 标题和地址 -->\r\n        <el-row>\r\n            <el-col :span=\"24\" class=\"breadcrumb-container\">\r\n");
                WriteLiteral("                <el-breadcrumb separator=\"/\">\r\n");
                WriteLiteral(@"                    <el-breadcrumb-item><strong>Sku管理</strong></el-breadcrumb-item>
                    <el-breadcrumb-item>附件管理</el-breadcrumb-item>
                </el-breadcrumb>
            </el-col>
        </el-row>
        <template>
            <article class=""administration-maintenance-container"">
                <div ref=""elfinder"" id=""elfinder""></div>


            </article>
        </template>
    </div>
    <script src=""/lib/elFinder/lib/jquery-ui/jquery-ui.min.js""></script>
    <script src=""/lib/elFinder/lib/elfinder/js/elfinder.full.js""></script>
    <script src=""/lib/elFinder/lib/elfinder/js/i18n/elfinder.zh_CN.js""></script>
    <script type=""text/javascript"" src=""/lib/ckeditor/ckeditor.js""></script>
    <script>
        var Main = {
            data() {
                return {
                    previewSrc: '',
                    mainSrc: '',
                    selectedFile: '',
                    selectedFilePath: '',
                    selectedFileByte: '',");
                WriteLiteral(@"
                    fileManageVisable: false,
                    active: 0,
                    cascader: [],
                    // 添加自定义配置
                    filters: {
                        searchKey: """",
                        isChanged: 0,
                        type: void 0,
                        status: void 0
                    },
                    total: 30,
                    page: 1,
                    pageSize: 10,
                    listLoading: false,
                }
            },
            created() {
             
                setTimeout(() => {
                    var path = getQueryString(""path"");
                    this.fileManageInitial(path);
                }, 0)
            },
            methods: {
                fileManageInitial(defalutPath) {
                    var path = '';
                    var isReadOnly = false;
                    var isLocked = false;
                    var isShowOnly = false;
                    if (d");
                WriteLiteral(@"efalutPath != undefined && defalutPath != null && defalutPath != """") {
                        path = defalutPath;
                        isReadOnly = true;
                        isLocked = true;
                        //isShowOnly = true;
                            
                    }
                    var _self = this;
                    var myCommands = elFinder.prototype._options.commands;
                    var disabled = ['callback', 'chmod', 'editor', 'netmount', 'ping',  'zipdl', 'help']; // Not yet implemented commands in elFinder.NetCore
                    elFinder.prototype.i18.en.messages.TextArea = ""Edit"";

                    $.each(disabled, function (i, cmd) {
                        (idx = $.inArray(cmd, myCommands)) !== -1 && myCommands.splice(idx, 1);
                    });

                    var options = {
                        url: '/sku/connector', // Default (Local File System)
                        customData: { path:path,isReadOnly:isReadOnly,isL");
                WriteLiteral(@"ocked:isLocked,isShowOnly:isShowOnly }, // customData passed in every request to the connector as query strings. These values are used in FileController's Index method.
                        rememberLastDir: false, // Prevent elFinder saving in the Browser LocalStorage the last visited directory
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
                            ['info'");
                WriteLiteral(@"],
                            ['quicklook'],
                            ['copy', 'cut', 'paste'],
                            ['rm'],
                            ['duplicate', 'rename', 'edit'],
                            ['selectall', 'selectnone', 'selectinvert'],
                            ['view', 'sort'],
                            ['search']
                        ]

                    }, handlers: {
                        select: function (event, elfinderInstance) {
                            if (event.data.selected.length == 1) {
                                var item = $('#' + event.data.selected[0]);
                                if (!item.hasClass('directory')) {
                                    selectedFile = event.data.selected[0];
                                    previewSrc = ""/sku/connector?cmd=file&target="" + selectedFile + ""&_t="" + event.timeStamp;

                                    $.ajax({
                                        type: 'post',
       ");
                WriteLiteral(@"                                 url: '/sku/selectFile',
                                        data: { target: selectedFile },
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
                                     ");
                WriteLiteral(@"               type: 'error',
                                                    message: errorThrown
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
                    window.addEventListener('scroll', this.onScroll)
                })
            }
            }
        var Ctor = Vue.extend(Main);
        new Ctor().$mount('#wrapper');

         function getQueryString(name) {
            var reg = new RegExp(""(^|&)"" + n");
                WriteLiteral("ame + \"=([^&]*)(&|$)\", \"i\");\r\n            var r = window.location.search.substr(1).match(reg);\r\n            if (r != null) return unescape(r[2]); return null;\r\n        }\r\n    </script>\r\n");
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
