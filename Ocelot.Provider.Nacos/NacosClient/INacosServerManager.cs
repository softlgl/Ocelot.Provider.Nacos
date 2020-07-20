using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nacos;

namespace Ocelot.Provider.Nacos.NacosClient
{
    public interface INacosServerManager
    {
        Task<List<Host>> GetServerAsync(string serviceName);
    }
}
