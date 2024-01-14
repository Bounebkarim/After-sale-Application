using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts;
public class ReclamationModifiedEvent
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ClientName { get; set; }
    public string ClientLastName { get; set; }
    public int EtatReclamation { get; set; } = default;
    public int Severity { get; set; } = default;
    public int problemType { get; set; } = default;
    public string Description { get; set; }
    public Guid ClientId { get; set; }
}
