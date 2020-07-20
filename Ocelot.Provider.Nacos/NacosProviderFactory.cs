using System;
using Ocelot.Provider.Nacos.NacosClient;
using Ocelot.ServiceDiscovery;
using Microsoft.Extensions.DependencyInjection;

namespace Ocelot.Provider.Nacos
{
    public static class NacosProviderFactory
    {
        public static ServiceDiscoveryFinderDelegate Get = (provider, config, route) =>
        {
            var client = provider.GetService<INacosServerManager>();
            if (config.Type?.ToLower() == "nacos" && client != null)
            {
                return new Nacos(route.ServiceName, client);
            }
            return null;
        };
    }
}
