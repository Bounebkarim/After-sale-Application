using Intervention.Domain.Enums;

namespace Intervention.Application.Features.InterventionFeature.Queries.GetInterventionDetails;
public class InterventionDetailsDto
{
    public Guid Id { get; set; }
    public Guid ReclamationId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string ClientName { get; set; }
    public string ClientLastName { get; set; }
    public InterventionStatus InterventionStatus { get; set; } = default;
    public Severity Severity { get; set; } = default;
    public ProblemType ProblemType { get; set; } = default;
}
