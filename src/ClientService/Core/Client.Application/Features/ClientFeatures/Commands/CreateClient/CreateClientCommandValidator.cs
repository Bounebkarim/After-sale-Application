using Client.Application.Contracts.Persistence;
using FluentValidation;

namespace Client.Application.Features.ClientFeatures.Commands.CreateClient;
public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public CreateClientCommandValidator(IClientRepository clientRepository)
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
            .Must(BeAValidIdNumber).WithMessage("Cin Not in valid format")
            .MustAsync(BeUnique).WithMessage("Cin Already in use");
        RuleFor(o=>o.PhoneNumber)
            .Matches(@"^\d{8}$").WithMessage("Invalid phone number format. It should be 8 digits.");
    }

    private async Task<bool> BeUnique(string cin, CancellationToken cancellationToken)
    {
        return await _clientRepository.CinExist(cin,cancellationToken);
    }

    private bool BeAValidIdNumber(string cin)
    {
        return !string.IsNullOrEmpty(cin) && cin.Length == 8 && int.TryParse(cin, out _);
    }
}
