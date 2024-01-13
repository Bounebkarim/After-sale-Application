using Contracts;
using Intervention.Application.Contracts.Exceptions;
using Intervention.Application.Contracts.Logging;
using Intervention.Application.Contracts.Persistence;
using Intervention.Application.Mapper;
using MassTransit;
using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Commands.CreateIntervention;
public class CreateInterventionCommandHandler : IRequestHandler<CreateInterventionCommand>
{
    private readonly IAppLogger<CreateInterventionCommandHandler> _logger;
    private readonly IGenericRepository<Domain.Intervention> _genericRepository;
    private readonly IRequestClient<CheckClientExistenceRequest> _client;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateInterventionCommandHandler(IAppLogger<CreateInterventionCommandHandler> logger,
                                            IGenericRepository<Domain.Intervention> genericRepository,
                                            IRequestClient<CheckClientExistenceRequest> client,
                                            IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _genericRepository = genericRepository;
        _client = client;
        _publishEndpoint = publishEndpoint;
    }
    public async Task Handle(CreateInterventionCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateInterventionCommandValidator(_client);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation failed on creating Intervention", validationResult);
            throw new BadRequestException("Validation failed on creating Intervention");
        }

        var reclamation = StaticMapper.Mapper.Map<Domain.Intervention>(request);
        await _genericRepository.CreateAsync(reclamation, cancellationToken);
    }
}
