using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Provider.Nacos.NacosClient;
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
            services.AddNacosDiscovery(configurationBuilder.Build());

            var provider = services.BuildServiceProvider();
            StatusReportBgTask statusReportBgTask = provider.GetRequiredService<StatusReportBgTask>();
            await statusReportBgTask.StartAsync();
            Console.ReadLine();
        }
    }
}
