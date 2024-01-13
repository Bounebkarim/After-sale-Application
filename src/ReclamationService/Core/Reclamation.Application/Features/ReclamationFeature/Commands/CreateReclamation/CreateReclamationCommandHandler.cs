using Contracts;
using MassTransit;
using MediatR;
using Reclamation.Application.Contracts.Exceptions;
using Reclamation.Application.Contracts.Logging;
using Reclamation.Application.Contracts.Persistence;
using Reclamation.Application.Mapper;

namespace Reclamation.Application.Features.ReclamationFeature.Commands.CreateReclamation;
public class CreateReclamationCommandHandler : IRequestHandler<CreateReclamationCommand>
{
    private readonly IAppLogger<CreateReclamationCommandHandler> _logger;
    private readonly IGenericRepository<Domain.Reclamation> _genericRepository;
    private readonly IRequestClient<CheckClientExistenceRequest> _client;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateReclamationCommandHandler(IAppLogger<CreateReclamationCommandHandler> logger,
                                            IGenericRepository<Domain.Reclamation> genericRepository,
                                            IRequestClient<CheckClientExistenceRequest> client,
                                            IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _genericRepository = genericRepository;
        _client = client;
        _publishEndpoint = publishEndpoint;
    }
    public async Task Handle(CreateReclamationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateReclamationCommandValidator(_client);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation failed on creating Intervention", validationResult);
            throw new BadRequestException("Validation failed on creating Intervention");
        }

        var intervention = staticMapper.Mapper.Map<Domain.Reclamation>(request);
        await _genericRepository.CreateAsync(intervention, cancellationToken);
    }
}
