using Intervention.Application.Contracts.Persistence;
using Intervention.Persistence.DatabaseContext;
using Intervention.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Intervention.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InterventionDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("InterventionDbConnection"),
                options => options.MigrationsAssembly(typeof(InterventionDbContext).Assembly.FullName));
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
}