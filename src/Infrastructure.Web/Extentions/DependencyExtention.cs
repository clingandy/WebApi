using CrmScrew.Core;
using CrmScrew.Domain;
using Infrastructure.Core;
using Infrastructure.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Web.Extentions
{
    public static class DependencyExtention
    {
        /// <summary>
        /// 注入 Core自带
        /// </summary>
        /// <param name="services"></param>
        public static void RegisDependency(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ISystemRepository, SystemRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDictRepository, DictRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}
