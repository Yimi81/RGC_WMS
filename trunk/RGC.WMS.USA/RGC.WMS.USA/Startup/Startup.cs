using AutoMapper;
using HuigeTec.Core.Domain.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RGC.WMS.USA.Application;
using RGC.WMS.USA.Authorization;
using RGC.WMS.USA.Data;
using RGC.WMS.USA.Domain;

namespace RGC.WMS.USA
{
    public class Startup
    {
        private const string _apiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddRazorRuntimeCompilation();

            //添加EF及Dapper工作单元
            services.AddUnitOfWork<WMSDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnectionString"))
            );

            //Domain层注入
            services.AutoDi(typeof(IDomainServiceBase));
            //Service层注入
            services.AutoDi(typeof(IAppService));
            //添加对AutoMapper的支持
            services.AddAutoMapper(typeof(ProfileBase));

            //身份认证注入
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            //应用程序配置
            services.Configure<DominBaseConfig>(Configuration.GetSection("System"));
            services.Configure<ApplicationBaseConfig>(Configuration.GetSection("System"));
            //加入session
            services.AddDistributedMemoryCache().AddSession();
            //HttpContextAccessor 默认实现了它简化了访问HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //授权注入
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            // 注册Swagger服务
            services.AddSwaggerGen(options =>
            {
                // 添加文档信息
                options.SwaggerDoc(_apiVersion,
                    new OpenApiInfo
                    {
                        Title = "RGC.WMS.API",
                        Description = "RGC.WMS",
                        Version = _apiVersion
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            //请求错误提示配置
            //app.UseErrorHandling();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // 启用Swagger中间件
            app.UseSwagger();

            // 配置SwaggerUI
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RGC.WMS.USA");
            });
        }
    }
}
