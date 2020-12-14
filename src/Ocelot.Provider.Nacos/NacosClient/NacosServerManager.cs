using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.Extensions.Options;
using Nacos;

namespace Ocelot.Provider.Nacos.NacosClient
{
    public class NacosServerManager : INacosServerManager
    {
        private readonly INacosNamingClient _client;
        IOptions<NacosAspNetCoreOptions> _optionsAccs;

        private readonly IEasyCachingProvider _provider;

        public NacosServerManager(
            INacosNamingClient client,
            IEasyCachingProviderFactory factory,
            IOptions<NacosAspNetCoreOptions> optionsAccs)
        {
            _client = client;
            _optionsAccs = optionsAccs;
            _provider = factory.GetCachingProvider("nacos.aspnetcore");
        }

        public async Task<List<Host>> GetServerAsync(string serviceName)
        {
            var cached = await _provider.GetAsync(serviceName, async () =>
            {
                var serviceInstances = await _client.ListInstancesAsync(new ListInstancesRequest
                {
                    ServiceName = serviceName,
                    GroupName = _optionsAccs.Value.GroupName,
                    NamespaceId = _optionsAccs.Value.Namespace,
                    HealthyOnly = true,
                });

                var baseUrl = string.Empty;

                if (serviceInstances != null && serviceInstances.Hosts != null && serviceInstances.Hosts.Any())
                {
                    return serviceInstances.Hosts.ToList();
                }
                return null;
            }, TimeSpan.FromSeconds(10));

            if (cached.HasValue)
            {
                return cached.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
