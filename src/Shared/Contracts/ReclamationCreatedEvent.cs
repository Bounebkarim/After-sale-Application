namespace Contracts;

public class ReclamationCreatedEvent
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid ClientName { get; set; }
    public Guid ClientLastName { get; set; }
    public int EtatReclamation { get; set; } = default;
    public int Severity { get; set; } = default;
    public int problemType { get; set; } = default;
}
