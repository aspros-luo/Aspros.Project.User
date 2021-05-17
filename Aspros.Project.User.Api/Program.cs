using System;
using System.Net;
using Infrastructure.Consul.ServiceDiscovery;
using Infrastructure.Consul;
using Infrastructure.Consul.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aspros.Project.User.Api
{
    public class Program
    {
        public const string ServiceName = "aspros_user";
        public const string Version = "v1";
        public static ConsulClient ConsulClient = new ConsulClient(c=>c.Address=new Uri("http://127.0.0.1:8500"));
        public static IServiceRegister ServiceRegistry;
        public static IConfigurationRegister ConfigurationRegister;

        public static void Main(string[] args)
        {
            InitConfiguration();
            ServiceRegister(ServiceName);
            var host = new WebHostBuilder()
                .ConfigureServices(cfg => { cfg.AddSingleton(ServiceRegistry); })
                .ConfigureServices(cfg => { cfg.AddSingleton(ConfigurationRegister); })
                .UseUrls($"http://*:{ServiceRegistry.ServicePort}")//set url
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        public static void InitConfiguration()
        {
            var configurationRegister = new ConfigurationRegister(ConsulClient);
            configurationRegister.SetKeyValueAsync($"basic/{ServiceName}", "{\"database\":\"Database=aspros_user\"}").Wait();
            configurationRegister.AddUpdatingPathAsync("basic").Wait();
            ConfigurationRegister = configurationRegister;
        }

        public static void ServiceRegister(string serviceName)
        {
            var ipAddress = ServiceRegistryExtensions.GetLocalIpAddress();
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new Exception("can't get server ip address.");
            }

            ServiceRegistry = new ServiceRegister(IPAddress.Parse(ipAddress),4379);
            ServiceRegistry
                .SetConsul(ConsulClient)
                .AddHttpHealthCheck("health", 30, 10)
                .RegisterServiceAsync(serviceName).Wait();
        }
    }
}