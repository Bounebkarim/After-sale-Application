using Contracts;
using FluentValidation;
using MassTransit;

namespace Intervention.Application.Features.InterventionFeature.Commands.CreateIntervention;
public class CreateInterventionCommandValidator : AbstractValidator<CreateInterventionCommand>
{
    private readonly IRequestClient<CheckClientExistenceRequest> _client;

    public CreateInterventionCommandValidator(IRequestClient<CheckClientExistenceRequest> client)
    {
        _client = client;
        RuleFor(r => r.ClientLastName)
            .NotNull().NotEmpty().WithMessage("ClientLastName is required");
        RuleFor(r => r.ClientName)
            .NotNull().NotEmpty().WithMessage("ClientName is required");
        RuleFor(r => r.EtatReclamation)
            .NotNull().NotEmpty().WithMessage("EtatIntervention is required");
        RuleFor(r => r.Severity)
            .NotNull().NotEmpty().WithMessage("Severity is required");
        RuleFor(r => r.Title)
            .NotNull().NotEmpty().WithMessage("Title is required");
        RuleFor(r => r.ProblemType)
            .NotNull().NotEmpty().WithMessage("problemType is required");
        RuleFor(r => r.ClientId)
            .MustAsync(ClientExist).WithMessage("Client does not exist");
    }

    private async Task<bool> ClientExist(Guid clientId, CancellationToken cancellationToken)
    {
        var response = await _client.GetResponse<CheckClientExistenceResponse>(new CheckClientExistenceRequest { ClientId = clientId }, cancellationToken);
        return response.Message.Exists;
    }
}
