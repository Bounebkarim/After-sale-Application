using Client.Application.Contracts.Logging;
using Client.Infrastructure.CheckClient;
using Client.Infrastructure.Logging;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddHealthChecks();
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.AddConsumer<CheckClientConsumer>();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                {
                    h.Username(configuration["MessageBroker:UserName"]);
                    h.Password(configuration["MessageBroker:Password"]);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
        return services;
    }

}
