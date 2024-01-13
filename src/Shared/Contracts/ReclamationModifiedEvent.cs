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
    public Guid ClientName { get; set; }
    public Guid ClientLastName { get; set; }
    public int EtatReclamation { get; set; } = default;
    public int Severity { get; set; } = default;
    public int problemType { get; set; } = default;
}
