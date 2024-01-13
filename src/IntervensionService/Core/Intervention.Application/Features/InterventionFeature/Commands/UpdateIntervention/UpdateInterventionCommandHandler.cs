using Contracts;
using Intervention.Application.Contracts.Exceptions;
using Intervention.Application.Contracts.Logging;
using Intervention.Application.Contracts.Persistence;
using Intervention.Application.Mapper;
using MassTransit;
using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Commands.UpdateIntervention;
public class UpdateInterventionCommandHandler : IRequestHandler<UpdateInterventionCommand>
{
    private readonly IGenericRepository<Domain.Intervention> _genericRepository;
    private readonly IRequestClient<CheckClientExistenceRequest> _client;
    private readonly IAppLogger<UpdateInterventionCommandHandler> _logger;
    private readonly IPublishEndpoint _endpoint;

    public UpdateInterventionCommandHandler(IGenericRepository<Domain.Intervention> genericRepository,
                                           IRequestClient<CheckClientExistenceRequest> client,
                                           IAppLogger<UpdateInterventionCommandHandler> logger,
                                           IPublishEndpoint endpoint)
    {
        this._genericRepository = genericRepository;
        this._client = client;
        this._logger = logger;
        _endpoint = endpoint;
    }
    public async Task Handle(UpdateInterventionCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateInterventionCommandValidator(_client);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation failed on Update Intervention request id-{0}", request.Id);
            throw new BadRequestException("Validation failed on Update Intervention request");
        }

        var reclamation = StaticMapper.Mapper.Map<Domain.Intervention>(request);
        await _genericRepository.UpdateAsync(reclamation, cancellationToken);
    }
}
