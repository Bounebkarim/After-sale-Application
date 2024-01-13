using Contracts;
using MassTransit;
using MediatR;
using Reclamation.Application.Contracts.Exceptions;
using Reclamation.Application.Contracts.Logging;
using Reclamation.Application.Contracts.Persistence;
using Reclamation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reclamation.Application.Mapper;

namespace Reclamation.Application.Features.ReclamationFeature.Commands.UpdateReclamation;
public class UpdateReclamationCommandHandler : IRequestHandler<UpdateReclamationCommand>
{
    private readonly IGenericRepository<Domain.Reclamation> _genericRepository;
    private readonly IRequestClient<CheckClientExistenceRequest> _client;
    private readonly IAppLogger<UpdateReclamationCommandHandler> _logger;
    private readonly IPublishEndpoint _endpoint;

    public UpdateReclamationCommandHandler(IGenericRepository<Domain.Reclamation> genericRepository,
                                           IRequestClient<CheckClientExistenceRequest> client,
                                           IAppLogger<UpdateReclamationCommandHandler> logger,
                                           IPublishEndpoint endpoint)
    {
        this._genericRepository = genericRepository;
        this._client = client;
        this._logger = logger;
        _endpoint = endpoint;
    }
    public async Task Handle(UpdateReclamationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateReclamationCommandValidator(_client);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation failed on Update reclamation request id-{0}", request.Id);
            throw new BadRequestException("Validation failed on Update reclamation request");
        }

        var reclamation = staticMapper.Mapper.Map<Domain.Reclamation>(request);
        await _genericRepository.UpdateAsync(reclamation, cancellationToken);
        await _endpoint.Publish(new ReclamationModifiedEvent()
        {
            ClientLastName = reclamation.ClientLastName,
            EtatReclamation = (int)reclamation.EtatReclamation,
            ClientName = reclamation.ClientId,
            Severity = (int)reclamation.Severity,
            Title = reclamation.Title,
            problemType = (int)reclamation.problemType,
            Id = reclamation.Id
        }, cancellationToken);
    }
}
