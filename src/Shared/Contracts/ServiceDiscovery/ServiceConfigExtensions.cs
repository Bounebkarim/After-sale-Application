using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Contracts.ServiceDiscovery;

public static class ServiceConfigExtensions
{
    public static ServiceConfig GetServiceConfig(this IConfiguration configuration, IWebHostEnvironment environment)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        var serviceConfig = new ServiceConfig
        {
            Id = configuration.GetSection("ServiceConfig:Id").Value!,
            Name = configuration.GetSection("ServiceConfig:Name").Value!,
            Address = configuration.GetSection("ServiceConfig:Address").Value!,
            Port = int.Parse(configuration.GetSection("ServiceConfig:Port").Value!),
            DiscoveryAddress = new Uri(configuration.GetSection("ServiceConfig:DiscoveryAddress").Value!),
            HealthCheckEndPoint = configuration.GetSection("ServiceConfig:HealthCheckEndPoint").Value!
        };

        return serviceConfig;
    }
}