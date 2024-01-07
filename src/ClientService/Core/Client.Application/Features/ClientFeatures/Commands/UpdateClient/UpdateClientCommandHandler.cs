using Client.Application.Contracts.Exceptions;
using Client.Application.Contracts.Logging;
using Client.Application.Contracts.Persistence;
using Client.Application.Mapper;
using MediatR;

namespace Client.Application.Features.ClientFeatures.Commands.UpdateClient;
public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAppLogger<UpdateClientCommandHandler> _logger;

    public UpdateClientCommandHandler(IClientRepository clientRepository, IAppLogger<UpdateClientCommandHandler> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }
    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateClientCommandValidator(_clientRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation Error on update request {0}-{1}", typeof(Domain.Client), request.Id);
            throw new BadRequestException("Invalid request", validationResult);
        }

        var clientToUpdate = staticMapper.Mapper.Map<Domain.Client>(request);
        await _clientRepository.UpdateAsync(clientToUpdate, cancellationToken);
        _logger.LogInformation("Client updated - {0}",clientToUpdate.Id);
    }
}
