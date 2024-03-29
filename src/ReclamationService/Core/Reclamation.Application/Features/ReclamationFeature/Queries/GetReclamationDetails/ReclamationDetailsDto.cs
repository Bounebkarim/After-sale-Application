﻿using Reclamation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamationDetails;
public class ReclamationDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid ClientName { get; set; }
    public Guid ClientLastName { get; set; }
    public ReclamationStatus EtatReclamation { get; set; } = default;
    public Severity Severity { get; set; } = default;
    public ProblemType problemType { get; set; } = default;
}
