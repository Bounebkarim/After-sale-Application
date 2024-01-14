using Microsoft.EntityFrameworkCore;
using Reclamation.Application.Contracts.Persistence.Specifications;
using Reclamation.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Reclamation.Application.Contracts.Persistence;
using Reclamation.Domain.Common;
using Reclamation.Persistence.Repositories.Evaluators;
using Reclamation.Application.Contracts.Specs;
using Reclamation.Persistence.Specifications.ReclamationSpecification;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Reclamation.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ReclamationDbContext _dbContext;

    public GenericRepository(ReclamationDbContext dbContext)
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

    public async Task<IReadOnlyList<T>> FindWithSpecificationPattern(CancellationToken cancellationToken,
        ISpecification<T> specification = null)
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
        return await _dbContext.Set<T>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
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