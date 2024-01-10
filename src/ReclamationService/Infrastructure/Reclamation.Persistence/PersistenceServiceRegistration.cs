using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reclamation.Persistence.DatabaseContext;

namespace Reclamation.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReclamationDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("ReclamationConnectionString"), 
                                options => options.MigrationsAssembly(typeof(ReclamationDbContext).Assembly.FullName));
        });
        return services;
    }
}
