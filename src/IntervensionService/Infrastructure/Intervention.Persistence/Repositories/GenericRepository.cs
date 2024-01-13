using Intervension.Domain.Common;
using Intervention.Application.Contracts.Persistence;
using Intervention.Application.Contracts.Persistence.Specifications;
using Intervention.Application.Contracts.Specs;
using Intervention.Persistence.DatabaseContext;
using Intervention.Persistence.Repositories.Evaluators;
using Intervention.Persistence.Specifications.InterventionSpecification;
using Microsoft.EntityFrameworkCore;

namespace Intervention.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly InterventionDbContext _dbContext;

    public GenericRepository(InterventionDbContext dbContext)
    {
        _dbContext = dbContext;
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

    public async Task<IReadOnlyList<T>> FindWithSpecificationPattern(
        ISpecification<T> specification, CancellationToken cancellationToken)
    {
        return await SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), specification)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Pagination<T>> GetPaginationAsync(SpecParams specParams,
        CancellationToken cancellationToken = default)
    {
        var paginationSpecification = new PaginationSpecification<T>(specParams);
        var query = _dbContext.Set<T>().AsQueryable();
        var totalRecords = await query.CountAsync(cancellationToken);
        var data = await SpecificationEvaluator<T>.GetQuery(query, paginationSpecification)
            .ToListAsync(cancellationToken);
        var pagination = new Pagination<T>(specParams.PageIndex, specParams.PageSize, totalRecords, data);
        return pagination;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}