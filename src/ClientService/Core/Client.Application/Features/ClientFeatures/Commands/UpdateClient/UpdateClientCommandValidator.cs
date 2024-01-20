using Client.Application.Contracts.Persistence;
using FluentValidation;

namespace Client.Application.Features.ClientFeatures.Commands.UpdateClient;
public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandValidator(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        RuleFor(o => o.Name)
            .NotEmpty()
            .NotNull().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must be shorter than 50 character");
        RuleFor(o => o.LastName)
            .NotEmpty()
            .NotNull().WithMessage("LastName is required")
            .MaximumLength(50).WithMessage("LastName must be shorter than 50 character");
        RuleFor(o => o.Address)
            .NotEmpty()
            .NotNull().WithMessage("Adress is required")
            .MaximumLength(100).WithMessage("Adress must be shorter than 100 character");
        RuleFor(o => o.Cin)
            .NotEmpty()
            .NotNull().WithMessage("Cin number is required")
            .Must(BeAValidIdNumber).WithMessage("Cin Not in valid format");
        RuleFor(o => o.Id)
            .NotEmpty()
            .NotNull().WithMessage("Id is required")
            .MustAsync(ClientExist).WithMessage("Client does Not exist");
        RuleFor(o => o.PhoneNumber)
            .Matches(@"^\d{8}$").WithMessage("Invalid phone number format. It should be 8 digits.");
        RuleFor(o => o)
            .MustAsync(BeUnique).WithMessage("Cin number already in use");
    }

    private async Task<bool> ClientExist(Guid id, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(id, cancellationToken);
        return client != null;
    }

    private async Task<bool> BeUnique(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var a = !await _clientRepository.ClientCinExist(command.Id, command.Cin, cancellationToken);
        return a;
    }

    private bool BeAValidIdNumber(string cin)
    {
        return !string.IsNullOrEmpty(cin) && cin.Length == 8 && int.TryParse(cin, out _);
    }
}
