namespace Client.Application.Features.ClientFeatures.Queries.GetClientDetails;

public class ClientDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Cin { get; set; }
    public string Adress { get; set; }
    public string PhoneNumber { get; set; }
}