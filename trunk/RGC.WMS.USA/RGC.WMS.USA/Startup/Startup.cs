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

            //���EF��Dapper������Ԫ
            services.AddUnitOfWork<WMSDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnectionString"))
            );

            //Domain��ע��
            services.AutoDi(typeof(IDomainServiceBase));
            //Service��ע��
            services.AutoDi(typeof(IAppService));
            //��Ӷ�AutoMapper��֧��
            services.AddAutoMapper(typeof(ProfileBase));

            //�����֤ע��
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            //Ӧ�ó�������
            services.Configure<DominBaseConfig>(Configuration.GetSection("System"));
            services.Configure<ApplicationBaseConfig>(Configuration.GetSection("System"));
            //����session
            services.AddDistributedMemoryCache().AddSession();
            //HttpContextAccessor Ĭ��ʵ���������˷���HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //��Ȩע��
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            // ע��Swagger����
            services.AddSwaggerGen(options =>
            {
                // ����ĵ���Ϣ
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

            //���������ʾ����
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

            // ����Swagger�м��
            app.UseSwagger();

            // ����SwaggerUI
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RGC.WMS.USA");
            });
        }
    }
}
