using System;
using Ocelot.Provider.Nacos.NacosClient;
using Ocelot.ServiceDiscovery;
using Microsoft.Extensions.DependencyInjection;
using Nacos.V2;
using Microsoft.Extensions.Options;
using Nacos;

namespace Ocelot.Provider.Nacos
{
    public static class NacosProviderFactory
    {
        public static ServiceDiscoveryFinderDelegate Get = (provider, config, route) =>
        {
            var client = provider.GetService<INacosNamingClient>();
            if (config.Type?.ToLower() == "nacos" && client != null)
            {
                var option = provider.GetService<IOptions<NacosAspNetCoreOptions>>();
                return new Nacos(route.ServiceName, client, option);
            }
            return null;
        };
    }
}
