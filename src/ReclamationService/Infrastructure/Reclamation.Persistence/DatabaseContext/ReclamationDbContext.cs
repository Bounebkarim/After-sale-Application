using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Reclamation.Domain.Common;

namespace Reclamation.Persistence.DatabaseContext;

public class ReclamationDbContext : DbContext
{
    public ReclamationDbContext()
    {

    }
    public ReclamationDbContext(DbContextOptions<ReclamationDbContext> options) : base(options) { }
    public DbSet<Domain.Reclamation> Reclamations { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReclamationDbContext).Assembly);
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