using Intervention.Application.Contracts.Specs;
using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Queries.GetInterventions;
public sealed record GetInterventionsQuery(SpecParams SpecParams) : IRequest<Pagination<InterventionDto>>;
