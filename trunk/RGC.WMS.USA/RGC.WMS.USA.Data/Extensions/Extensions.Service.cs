using System;
using System.Linq;
using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RGC.WMS.USA.Data.Repositories;
using RGC.WMS.USA.Data.Uow;
using RGC.WMS.USA.Domain.Uow;

namespace RGC.WMS.USA.Data
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options) where T : DbContext
        {
            services.AddDbContext<T>(options);
            services.AddScoped<DbContext, T>();
            AddDefault(services);
            return services;
        }

        #region private function
        /// <summary>
        /// 注册作用域依赖
        /// </summary>
        private static void AddDefault(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AutoDi(typeof(IRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

        }

        /// <summary>
        /// 自动DI依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static IServiceCollection AutoDi(this IServiceCollection services, Type baseType)
        {
            var allAssemblies = AppDomain.CurrentDomain.GetCurrentPathAssembly();
            foreach (var assembly in allAssemblies)
            {
                var types = assembly.GetTypes()
                    .Where(type => type.IsClass
                                   && type.BaseType != null
                                   && type.HasImplementedRawGeneric(baseType));
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();

                    var interfaceType = interfaces.FirstOrDefault(x => x.Name == $"I{type.Name}");
                    if (interfaceType == null)
                    {
                        interfaceType = type;
                    }
                    ServiceDescriptor serviceDescriptor =
                        new ServiceDescriptor(interfaceType, type, ServiceLifetime.Scoped);
                    if (!services.Contains(serviceDescriptor))
                    {
                        services.Add(serviceDescriptor);
                    }
                }
            }

            return services;
        }
        #endregion
    }
}
