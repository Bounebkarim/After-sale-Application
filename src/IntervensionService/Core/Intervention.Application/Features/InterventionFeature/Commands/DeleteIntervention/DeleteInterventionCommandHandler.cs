using Intervention.Application.Contracts.Exceptions;
using Intervention.Application.Contracts.Logging;
using Intervention.Application.Contracts.Persistence;
using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Commands.DeleteIntervention;
internal class DeleteInterventionCommandHandler : IRequestHandler<DeleteInterventionCommand>
{
    private readonly IGenericRepository<Domain.Intervention> _genericRepository;
    private readonly IAppLogger<DeleteInterventionCommandHandler> _logger;

    public DeleteInterventionCommandHandler(IGenericRepository<Domain.Intervention> genericRepository, IAppLogger<DeleteInterventionCommandHandler> logger)
    {
        _genericRepository = genericRepository;
        _logger = logger;
    }
    public async Task Handle(DeleteInterventionCommand request, CancellationToken cancellationToken)
    {
        var intervention = await _genericRepository.GetByIdAsync(request.Id, cancellationToken);
        if (intervention == null)
        {
            _logger.LogWarning("object {0} with id {1} not found", nameof(Domain.Intervention), request.Id);
            throw new NotFoundException(nameof(Domain.Intervention), request.Id);
        }

        await _genericRepository.DeleteAsync(intervention, cancellationToken);
    }
}
