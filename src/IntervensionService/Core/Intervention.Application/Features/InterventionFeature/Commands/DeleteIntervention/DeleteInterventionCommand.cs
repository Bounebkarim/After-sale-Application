using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Commands.DeleteIntervention;
public record DeleteInterventionCommand(Guid Id) : IRequest;

