using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;
using Nacos.V2;

namespace Ocelot.Provider.Nacos
{
    public class Nacos : IServiceDiscoveryProvider
    {
        private readonly INacosNamingService _client;
        private readonly string _serviceName;

        public Nacos(string serviceName, INacosNamingService client)
        {
            _client = client;
            _serviceName = serviceName;
        }

        public async Task<List<Service>> Get()
        {
            var services = new List<Service>();

            var instances = await _client.GetAllInstances(_serviceName);

            if (instances != null && instances.Any())
            {
                services.AddRange(instances.Select(i => new Service(i.InstanceId, new ServiceHostAndPort(i.Ip, i.Port), "", "", new List<string>())));
            }

            return await Task.FromResult(services);
        }
    }
}
