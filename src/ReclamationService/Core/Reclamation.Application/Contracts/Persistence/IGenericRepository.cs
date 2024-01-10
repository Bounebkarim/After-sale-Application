using Reclamation.Application.Contracts.Persistence.Specifications;
using Reclamation.Application.Contracts.Specs;
using Reclamation.Domain.Common;

namespace Reclamation.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> FindWithSpecificationPattern(CancellationToken cancellationToken, ISpecification<T> specification = null);
    Task<Pagination<T>> GetPaginationAsync(SpecParams specParams, CancellationToken cancellationToken = default);
}