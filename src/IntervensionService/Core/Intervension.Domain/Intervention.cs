using Intervension.Domain.Common;
using Intervention.Domain.Enums;

namespace Intervention.Domain;

public class Intervention : BaseEntity
{
    public Intervention(Guid id) : base(id)
    {
    }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid ReclamationId { get; set; }
    public Guid ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientLastName { get; set; }
    public InterventionStatus InterventionStatus { get; set; } = default;
    public Severity Severity { get; set; } = default;
    public ProblemType ProblemType { get; set; } = default;
}
