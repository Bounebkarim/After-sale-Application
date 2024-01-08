using System.Reflection;
using Client.Application.Contracts.Persistence;
using Client.Persistence.DatabaseContext;
using Client.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ClientDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("ClientDbConnection"),
                assembly => assembly.MigrationsAssembly(typeof(ClientDbContext).Assembly.FullName));
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IClientRepository, ClientRepository>();
        return services;
    }
}
