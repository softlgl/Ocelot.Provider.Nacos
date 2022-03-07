using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ocelot.Provider.Nacos.NacosClient;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;
using Nacos;
using Microsoft.Extensions.Options;

namespace Ocelot.Provider.Nacos
{
    public class Nacos : IServiceDiscoveryProvider
    {
        private readonly INacosNamingClient _client;
        private readonly string _serviceName;
        private readonly string _groupName;
        private readonly string _clusters;
        private readonly string _namespaceId;

        public Nacos(string serviceName, INacosNamingClient client, IOptions<NacosAspNetCoreOptions> options)
        {
            _serviceName = serviceName;
            _client = client;
            _groupName = string.IsNullOrWhiteSpace(options.Value.GroupName) ?
                "DEFAULT_GROUP" : options.Value.GroupName;
            _clusters = string.IsNullOrWhiteSpace(options.Value.ClusterName) ? "DEFAULT" : options.Value.ClusterName;
            _namespaceId = string.IsNullOrWhiteSpace(options.Value.Namespace) ? "public" : options.Value.Namespace;
        }

        public async Task<List<Service>> Get()
        {
            var services = new List<Service>();

            var instances = await _client.ListInstancesAsync(new ListInstancesRequest
            {
                Clusters = _clusters,
                ServiceName = _serviceName,
                GroupName = _groupName,
                NamespaceId = _namespaceId,
                HealthyOnly = true,
            });

            if (instances != null && instances.Hosts!=null && instances.Hosts.Any())
            {
                services.AddRange(instances.Hosts.Select(i => new Service(i.InstanceId, new ServiceHostAndPort(i.Ip, i.Port), "", "", new List<string>())));
            }

            return await Task.FromResult(services);
        }
    }
}
