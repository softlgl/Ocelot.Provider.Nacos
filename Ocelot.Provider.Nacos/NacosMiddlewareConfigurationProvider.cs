using System;
using System.Threading.Tasks;
using Ocelot.Configuration;
using Ocelot.Configuration.Repository;
using Ocelot.Middleware;
using Ocelot.Provider.Nacos.NacosClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ocelot.Provider.Nacos
{
    public class NacosMiddlewareConfigurationProvider
    {
        public static OcelotMiddlewareConfigurationDelegate Get = builder =>
        {
            var internalConfigRepo = builder.ApplicationServices.GetService<IInternalConfigurationRepository>();
            var config = internalConfigRepo.Get();

            var hostLifetime = builder.ApplicationServices.GetService<IHostApplicationLifetime>();

            if (UsingNacosServiceDiscoveryProvider(config.Data))
            {
                builder.UseNacosDiscovery(hostLifetime).GetAwaiter().GetResult();
            }

            return Task.CompletedTask;
        };

        private static bool UsingNacosServiceDiscoveryProvider(IInternalConfiguration configuration)
        {
            return configuration?.ServiceProviderConfiguration != null && configuration.ServiceProviderConfiguration.Type?.ToLower() == "nacos";
        }
    }
}
