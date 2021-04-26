using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Provider.Nacos.NacosClient;
using Ocelot.Provider.Nacos.NacosClient.V2;
using Xunit;

namespace Ocelot.Provider.Nacos.Test
{
    public class NacosClientTest
    {
        [Fact]
        public async void TestClient()
        {
            IServiceCollection services = new ServiceCollection();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            services.AddNacosAspNet(configurationBuilder.Build());

            var provider = services.BuildServiceProvider();
            RegSvcBgTask regSvcBgTask = provider.GetRequiredService<RegSvcBgTask>();
            await regSvcBgTask.StartAsync();
            Console.ReadLine();
        }
    }
}
