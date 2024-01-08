using Client.Application.Contracts.Logging;
using Client.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        return services;
    }

}
