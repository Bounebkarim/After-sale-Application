using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Application.Contracts.Exceptions;
using Client.Application.Contracts.Logging;
using Client.Application.Contracts.Persistence;
using MediatR;

namespace Client.Application.Features.ClientFeatures.Commands.DeleteClient;
public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAppLogger<DeleteClientCommandHandler> _logger;

    public DeleteClientCommandHandler(IClientRepository clientRepository, IAppLogger<DeleteClientCommandHandler> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }
    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var clientToDelete = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);
        if (clientToDelete is null)
        {
            _logger.LogWarning("Invalid delete request {0} - {1}", typeof(Domain.Client), request.Id);
            throw new BadRequestException("Invalid request");
        }
        await _clientRepository.DeleteAsync(clientToDelete, cancellationToken);
    }
}
