namespace Client.Application.Features.ClientFeatures.Queries.GetClients;

public sealed class ClientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}