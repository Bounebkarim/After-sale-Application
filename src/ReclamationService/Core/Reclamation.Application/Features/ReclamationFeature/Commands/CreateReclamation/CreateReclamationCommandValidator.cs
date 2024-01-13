using Contracts;
using FluentValidation;
using MassTransit;

namespace Reclamation.Application.Features.ReclamationFeature.Commands.CreateReclamation;
public class CreateReclamationCommandValidator : AbstractValidator<CreateReclamationCommand>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRequestClient<CheckClientExistenceRequest> _client;

    public CreateReclamationCommandValidator(IRequestClient<CheckClientExistenceRequest> client)
    {
        _client = client;
        RuleFor(r => r.ClientLastName).NotNull().NotEmpty().WithMessage("ClientLastName is required");
        RuleFor(r => r.ClientName).NotNull().NotEmpty().WithMessage("ClientName is required");
        RuleFor(r => r.EtatReclamation).NotNull().NotEmpty().WithMessage("EtatReclamation is required");
        RuleFor(r => r.Severity).NotNull().NotEmpty().WithMessage("Severity is required");
        RuleFor(r => r.Title).NotNull().NotEmpty().WithMessage("Title is required");
        RuleFor(r => r.problemType).NotNull().NotEmpty().WithMessage("problemType is required");
        RuleFor(r => r.ClientId).MustAsync(ClientExist).WithMessage("Client does not exist");
    }

    private async Task<bool> ClientExist(Guid clientId, CancellationToken cancellationToken)
    {
        var response = await _client.GetResponse<CheckClientExistenceResponse>(new CheckClientExistenceRequest { ClientId = clientId }, cancellationToken);
        return response.Message.Exists;
    }
}
