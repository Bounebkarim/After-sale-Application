using Client.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.Persistence.DatabaseContext;
public class ClientDbContext : DbContext
{
    public ClientDbContext()
    {
        
    }
    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
    {
    }
    public DbSet<Domain.Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entity in base.ChangeTracker.Entries<BaseEntity>().Where(o => o.State == EntityState.Modified || o.State == EntityState.Added).ToList())
        {
            entity.Entity.DateModified = DateTime.UtcNow;
            entity.Entity.ModifiedBy = "emna";
            if (entity.State == EntityState.Added)
            {
                entity.Entity.DateCreated = DateTime.UtcNow;
                entity.Entity.CreatedBy = "karim";
            }
            else if (entity.State == EntityState.Modified && entity.Entity.CreatedBy == null)
            {
                entity.Entity.DateCreated = DateTime.UtcNow;
                entity.Entity.CreatedBy = "karim";
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
