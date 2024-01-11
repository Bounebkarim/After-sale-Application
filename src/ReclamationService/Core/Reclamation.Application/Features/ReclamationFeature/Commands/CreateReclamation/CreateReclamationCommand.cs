using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Reclamation.Domain.Enums;

namespace Reclamation.Application.Features.ReclamationFeature.Commands.CreateReclamation;
public sealed record CreateReclamationCommand : IRequest
{
    public string Title { get; set; }
    public Guid ClientName { get; set; }
    public Guid ClientId { get; set; }
    public Guid ClientLastName { get; set; }
    public ReclamationStatus EtatReclamation { get; set; }
    public Severity Severity { get; set; }
    public ProblemType problemType { get; set; }
}
