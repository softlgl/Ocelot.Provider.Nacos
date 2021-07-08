using System;
using Ocelot.ServiceDiscovery;
using Microsoft.Extensions.DependencyInjection;
using Nacos.V2;
using Ocelot.Provider.Nacos.NacosClient.V2;
using Microsoft.Extensions.Options;

namespace Ocelot.Provider.Nacos
{
    public static class NacosProviderFactory
    {
        public static ServiceDiscoveryFinderDelegate Get = (provider, config, route) =>
        {
            var client = provider.GetService<INacosNamingService>();
           var option= provider.GetService<IOptions<NacosAspNetOptions>>();
            if (config.Type?.ToLower() == "nacos" && client != null)
            {
                return new Nacos(route.ServiceName, client, option.Value.GroupName);
            }
            return null;
        };
    }
}
