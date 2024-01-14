using Reclamation.Domain.Common;
using Reclamation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Domain;
public class Reclamation : BaseEntity
{
    public Reclamation(Guid Id) : base(Id) { }

    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientLastName { get; set; }
    public ReclamationStatus EtatReclamation { get; set; } = default;
    public Severity Severity { get; set; } = default;
    public ProblemType problemType { get; set; } = default;

}
