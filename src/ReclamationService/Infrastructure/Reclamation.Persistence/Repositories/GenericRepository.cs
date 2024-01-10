using Client.Application.Contracts.Persistence;
using Client.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Reclamation.Application.Contracts.Persistence.Specifications;
using Reclamation.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Persistence.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ReclamationDbContext _dbContext;

    public GenericRepository(ReclamationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> query, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().Where(query).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> FindWithSpecificationPattern(CancellationToken cancellationToken, ISpecification<T> specification = null)
    {
        return await SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), specification).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
