﻿using Intervention.Domain.Enums;
using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Commands.UpdateIntervention;
public sealed record UpdateInterventionCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid ClientName { get; set; }
    public Guid ClientId { get; set; }
    public Guid ClientLastName { get; set; }
    public InterventionStatus EtatReclamation { get; set; }
    public Severity Severity { get; set; }
    public ProblemType ProblemType { get; set; }
}
