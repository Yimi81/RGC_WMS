#pragma checksum "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Home\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3d92356f40a7d9d62bf2dfd101851a66642cd064"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Login), @"mvc.1.0.view", @"/Views/Home/Login.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3d92356f40a7d9d62bf2dfd101851a66642cd064", @"/Views/Home/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"804fc8bd0ed383ec8f2a14309e921cbb1975ce29", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\SVN\RGC\RGC.WMS\trunk\RGC.WMS.USA\RGC.WMS.USA\Views\Home\Login.cshtml"
  
    ViewBag.Title = "Login";

#line default
#line hidden
#nullable disable
            DefineSection("Styles", async() => {
                WriteLiteral("\r\n\r\n");
            }
            );
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3d92356f40a7d9d62bf2dfd101851a66642cd0643367", async() => {
                WriteLiteral("\r\n    <div class=\"login-top\">\r\n        <p class=\"ptb25 tc bac00\"><img class=\"vm\" src=\"/images/logo02.png\"");
                BeginWriteAttribute("alt", " alt=\"", 173, "\"", 179, 0);
                EndWriteAttribute();
                WriteLiteral(@"></p>
    </div>
    <div class=""f-c"">
        <p class=""tc fs28 mt20 c01"">欢迎使用 后台管理系统</p>
        <div id=""login"">
            <div class=""login"" v-loading=""loading"">
                <el-form class=""login-panel"" :model=""loginForm"" status-icon :rules=""loginRules"" ref=""loginForm"" v-on:keyup.enter.native=""submitForm('loginForm')"">
                    <el-form-item class=""tc"">
                        <label class=""fs22 fwb"">管理员登录</label>
                    </el-form-item>
                    <el-form-item prop=""name"">
                        <el-input placeholder=""请输入账号"" prefix-icon=""el-icon-third-account"" v-model=""loginForm.name""></el-input>
                    </el-form-item>
                    <el-form-item prop=""password"">
                        <el-input placeholder=""请输入密码"" type=""password"" prefix-icon=""el-icon-third-123"" v-model=""loginForm.password""></el-input>
                    </el-form-item>
                    <el-form-item prop=""yzm"" class=""login-panel__yzm"">
                     ");
                WriteLiteral(@"   <el-input placeholder=""请输入验证码"" prefix-icon=""el-icon-third-msnui-auth-code"" v-model=""loginForm.yzm""></el-input>
                        <img src=""validateCode"" title=""单击刷新图片"" alt=""单击刷新图片"" onclick=""this.src='validateCode?'+Math.random();"">
                    </el-form-item>
");
                WriteLiteral(@"                    <el-form-item>
                        <el-button class=""w100b btn-red"" type=""primary"" v-on:click=""submitForm('loginForm')"">登录</el-button>
                    </el-form-item>
                </el-form>
            </div>
            <p class=""login-footer""></p>
        </div>
    </div>
    <!-- 登录表单 -->
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
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"

    <script>
        var Main = {
            data() {
                return {
                    loading: false,
                    // 表单字段
                    loginForm: {
                        name: '',
                        password: '',
                        yzm: ''
                    },
                    // 验证表单
                    loginRules: {
                        name: [
                            { required: true, message: '账号不能为空', trigger: 'blur' }
                        ],
                        password: [
                            { required: true, message: '密码不能为空', trigger: 'blur' }
                        ],
                        yzm: [
                            { required: true, message: '验证码不能为空', trigger: 'blur' }
                        ]
                    },
                }
            },
            methods: {

                // 提交表单
                submitForm(formName) {
                    var _self = this;
              ");
                WriteLiteral(@"      _self.$refs[formName].validate((valid) => {
                        if (valid) {
                            _self.loading = true;
                            //alert('submit!');
                            $.ajax({
                                type: 'POST',
                                url: '/rest/bmsuser/login',
                                 dataType: 'json',
                                contentType: 'application/json',
                                data: JSON.stringify({
                                    UserName: _self.loginForm.name.trim(),
                                    Password: _self.loginForm.password.trim(),
                                    ValidateCode: _self.loginForm.yzm.trim()
                                }),
                                success: function (data) {
                                    if (data.success == true) {
                                        //登录
                                        //_self.$notify.success({
      ");
                WriteLiteral(@"                                  //    title: '成功',
                                        //    message: '登录成功'
                                        //});
                                        location.href = ""/"";
                                    } else {
                                        //alert(data.msg);
                                        _self.$notify.error({
                                            title: '错误',
                                            message: '账号或密码错误'
                                        });
                                    }
                                    _self.loading = false;
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    console.log(errorThrown);
                                    _self.loading = false;
                                }
                            });
                        } else {
         ");
                WriteLiteral(@"                   console.log('error submit!!');
                            _self.loading = false;
                            return false;
                        }
                    });
                }
            }
        }
        var Ctor = Vue.extend(Main);
        new Ctor().$mount('#login');
    </script>
");
            }
            );
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
