using MediatR;
using Reclamation.Application.Contracts.Logging;
using Reclamation.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reclamation.Application.Contracts.Exceptions;

namespace Reclamation.Application.Features.ReclamationFeature.Commands.DeleteReclamation;
internal class DeleteReclamationCommandHandler : IRequestHandler<DeleteReclamationCommand>
{
    private readonly IGenericRepository<Domain.Reclamation> _genericRepository;
    private readonly IAppLogger<DeleteReclamationCommandHandler> _logger;

    public DeleteReclamationCommandHandler(IGenericRepository<Domain.Reclamation> genericRepository, IAppLogger<DeleteReclamationCommandHandler> logger)
    {
        _genericRepository = genericRepository;
        _logger = logger;
    }
    public async Task Handle(DeleteReclamationCommand request, CancellationToken cancellationToken)
    {
        var reclamation = await _genericRepository.GetByIdAsync(request.Id, cancellationToken);
        if (reclamation == null)
        {
            _logger.LogWarning("object {0} with id {1} not found", nameof(Domain.Reclamation), request.Id);
            throw new NotFoundException(nameof(Domain.Reclamation), request.Id);
        }

        await _genericRepository.DeleteAsync(reclamation, cancellationToken);
    }
}
