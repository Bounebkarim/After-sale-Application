using MediatR;
using Reclamation.Domain.Enums;

namespace Reclamation.Application.Features.ReclamationFeature.Commands.CreateReclamation;
public sealed record CreateReclamationCommand : IRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ClientName { get; set; }
    public Guid ClientId { get; set; }
    public string ClientLastName { get; set; }
    public ReclamationStatus EtatReclamation { get; set; }
    public Severity Severity { get; set; }
    public ProblemType problemType { get; set; }
}
