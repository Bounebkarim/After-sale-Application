using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Queries.GetInterventionDetails;
public sealed record GetInterventionDetailsQuery(Guid Id) : IRequest<InterventionDetailsDto>
{
}
