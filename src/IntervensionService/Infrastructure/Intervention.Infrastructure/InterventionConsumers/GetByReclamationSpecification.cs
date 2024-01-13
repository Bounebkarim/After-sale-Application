using System.Linq.Expressions;
using Intervention.Application.Contracts.Persistence.Specifications;

namespace Intervention.Infrastructure.InterventionConsumers;

public class GetByReclamationSpecification : ISpecification<Domain.Intervention>
{
    public GetByReclamationSpecification(Guid reclamationId)
    {
        Criteria = intervention => intervention.ReclamationId == reclamationId;
    }
    public Expression<Func<Domain.Intervention, bool>> Criteria { get; }
    public List<Expression<Func<Domain.Intervention, object>>> Includes { get; }
    public List<string> IncludeStrings { get; }
    public Expression<Func<Domain.Intervention, object>> OrderBy { get; }
    public Expression<Func<Domain.Intervention, object>> OrderByDescending { get; }
    public Expression<Func<Domain.Intervention, object>> GroupBy { get; }
    public int Take { get; }
    public int Skip { get; }
    public bool IsPagingEnabled { get; }
}