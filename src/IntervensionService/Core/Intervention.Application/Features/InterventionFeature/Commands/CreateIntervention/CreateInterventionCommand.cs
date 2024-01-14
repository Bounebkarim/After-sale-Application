using Intervention.Domain.Enums;
using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Commands.CreateIntervention;
public sealed record CreateInterventionCommand : IRequest
{
    public string Title { get; set; }
    public string ClientName { get; set; }
    public Guid ClientId { get; set; }
    public Guid ReclamationId { get; set; }
    public string Description { get; set; } 
    public string ClientLastName { get; set; }
    public InterventionStatus EtatReclamation { get; set; }
    public Severity Severity { get; set; }
    public ProblemType ProblemType { get; set; }
}
