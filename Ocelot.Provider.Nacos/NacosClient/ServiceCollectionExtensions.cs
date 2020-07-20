using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Nacos;
using Nacos.Config.Http;

namespace Ocelot.Provider.Nacos.NacosClient
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Nacos AspNetCore.
        /// </summary>
        /// <param name="services">services.</param>
        /// <param name="configuration">configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddNacosDiscovery(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var nacosSection = configuration.GetSection("nacos");
            services.Configure<NacosAspNetCoreOptions>(nacosSection);
            services.AddNacosNaming(configuration);

            services.AddEasyCaching(options =>
            {
                options.UseInMemory("nacos.aspnetcore");
            });

            services.AddSingleton<INacosServerManager, NacosServerManager>();
            services.AddSingleton<StatusReportBgTask>();
            return services;
        }

        public static async Task<IApplicationBuilder> UseNacosDiscovery(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            StatusReportBgTask statusReportBgTask = app.ApplicationServices.GetRequiredService<StatusReportBgTask>();
            await statusReportBgTask.StartAsync();
            lifetime.ApplicationStopping.Register(async()=> {
                await statusReportBgTask.StopAsync();
            });
            return app;
        }
    }
}
