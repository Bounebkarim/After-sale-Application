using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reclamation.Application.Contracts.Persistence;
using Reclamation.Persistence.DatabaseContext;
using Reclamation.Persistence.Repositories;

namespace Reclamation.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ReclamationDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("ReclamationDbConnection"),
                options => options.MigrationsAssembly(typeof(ReclamationDbContext).Assembly.FullName));
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
}