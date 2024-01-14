using Intervension.Domain.Common;
using Intervention.Application.Contracts.Logging;
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
    private readonly IAppLogger<GenericRepository<T>> _logger;

    public GenericRepository(InterventionDbContext dbContext, IAppLogger<GenericRepository<T>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            _logger.LogWarning("{0}", ex);
        }
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
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Pagination<T>> GetPaginationAsync(SpecParams specParams,
                                                        CancellationToken cancellationToken = default)
    {
        var paginationSpecification = new PaginationSpecification<T>(specParams);
        var query = _dbContext.Set<T>().AsQueryable();
        var totalRecords = await query.CountAsync(cancellationToken);
        var data = await SpecificationEvaluator<T>.GetQuery(query, paginationSpecification)
                                                         .AsNoTracking()
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