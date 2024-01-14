using MediatR;

namespace Client.Application.Features.ClientFeatures.Commands.UpdateClient;
public sealed record UpdateClientCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Cin { get; set; } = string.Empty;
}
