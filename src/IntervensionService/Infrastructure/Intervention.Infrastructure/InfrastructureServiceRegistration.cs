using Client.Infrastructure.Logging;
using Intervention.Application.Contracts.Logging;
using Intervention.Infrastructure.InterventionConsumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Intervention.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddHealthChecks();
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.AddConsumer<UpdateInterventionConsumer>();
            busConfigurator.AddConsumer<CreateInterventionConsumer>();
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
