using Intervension.Domain.Common;
using Intervention.Application.Contracts.Persistence.Specifications;
using Intervention.Application.Contracts.Specs;

namespace Intervention.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> FindWithSpecificationPattern(ISpecification<T> specification = null,CancellationToken cancellationToken=default);
    Task<Pagination<T>> GetPaginationAsync(SpecParams specParams, CancellationToken cancellationToken = default);
}