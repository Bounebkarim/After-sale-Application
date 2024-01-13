using MediatR;
using Reclamation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Application.Features.ReclamationFeature.Commands.UpdateReclamation;
public sealed record UpdateReclamationCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid ClientName { get; set; }
    public Guid ClientId { get; set; }
    public Guid ClientLastName { get; set; }
    public ReclamationStatus EtatReclamation { get; set; }
    public Severity Severity { get; set; }
    public ProblemType problemType { get; set; }
}
