
using Intervension.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Intervention.Persistence.DatabaseContext;

public class InterventionDbContext : DbContext
{
    public InterventionDbContext(DbContextOptions<InterventionDbContext> options) : base(options) { }
    public DbSet<Domain.Intervention> Interventions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InterventionDbContext).Assembly);
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