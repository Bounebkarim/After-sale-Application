using Reclamation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamations;
public record ReclamationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ClientName { get; set; }
    public string ClientLastName { get; set; }
    public ReclamationStatus EtatReclamation { get; set; } = default;
    public Severity Severity { get; set; } = default;
    public ProblemType problemType { get; set; } = default;
}
