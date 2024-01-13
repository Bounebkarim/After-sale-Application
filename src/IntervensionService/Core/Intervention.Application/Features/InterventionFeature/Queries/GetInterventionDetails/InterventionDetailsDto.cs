using Intervention.Domain.Enums;

namespace Intervention.Application.Features.InterventionFeature.Queries.GetInterventionDetails;
public class InterventionDetailsDto
{
    public Guid Id { get; set; }
    public Guid ReclamationId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid ClientName { get; set; }
    public Guid ClientLastName { get; set; }
    public InterventionStatus EtatReclamation { get; set; } = default;
    public Severity Severity { get; set; } = default;
    public ProblemType ProblemType { get; set; } = default;
}
