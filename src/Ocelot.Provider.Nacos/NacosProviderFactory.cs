using System;
using Ocelot.ServiceDiscovery;
using Microsoft.Extensions.DependencyInjection;
using Nacos.V2;

namespace Ocelot.Provider.Nacos
{
    public static class NacosProviderFactory
    {
        public static ServiceDiscoveryFinderDelegate Get = (provider, config, route) =>
        {
            var client = provider.GetService<INacosNamingService>();
            if (config.Type?.ToLower() == "nacos" && client != null)
            {
                return new Nacos(route.ServiceName, client);
            }
            return null;
        };
    }
}
