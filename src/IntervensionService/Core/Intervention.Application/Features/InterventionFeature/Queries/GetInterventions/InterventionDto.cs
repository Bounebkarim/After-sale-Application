using Intervention.Domain.Enums;

namespace Intervention.Application.Features.InterventionFeature.Queries.GetInterventions;
public record InterventionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ClientName { get; set; }
    public string ClientLastName { get; set; }
    public InterventionStatus InterventionStatus { get; set; } = default;
    public Severity Severity { get; set; } = default;
    public ProblemType ProblemType { get; set; } = default;
}
