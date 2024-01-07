using System.Security.AccessControl;
using Client.Application.Contracts.Exceptions;
using Client.Application.Contracts.Logging;
using Client.Application.Contracts.Persistence;
using Client.Application.Mapper;
using MediatR;

namespace Client.Application.Features.ClientFeatures.Commands.CreateClient;
public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAppLogger<CreateClientCommandHandler> _logger;

    public CreateClientCommandHandler(IClientRepository clientRepository, IAppLogger<CreateClientCommandHandler> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }
    public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateClientCommandValidator(_clientRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(Client), request);
            throw new BadRequestException("Invalid Client", validationResult);
        }
        var clientToBe = staticMapper.Mapper.Map<Domain.Client>(request);
        await _clientRepository.CreateAsync(clientToBe, cancellationToken);
        _logger.LogInformation("client Created {0}", clientToBe.Id);
    }
}
