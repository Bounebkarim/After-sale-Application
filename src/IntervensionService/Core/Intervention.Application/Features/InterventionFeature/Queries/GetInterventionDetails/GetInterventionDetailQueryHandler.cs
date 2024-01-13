using Intervention.Application.Contracts.Exceptions;
using Intervention.Application.Contracts.Logging;
using Intervention.Application.Contracts.Persistence;
using Intervention.Application.Mapper;
using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Queries.GetInterventionDetails;
public class GetInterventionDetailQueryHandler : IRequestHandler<GetInterventionDetailsQuery, InterventionDetailsDto>
{
    private readonly IGenericRepository<Domain.Intervention> _genericRepository;
    private readonly IAppLogger<GetInterventionDetailQueryHandler> _logger;

    public GetInterventionDetailQueryHandler(IGenericRepository<Domain.Intervention> genericRepository,
                                            IAppLogger<GetInterventionDetailQueryHandler> logger)
    {
        _genericRepository = genericRepository;
        _logger = logger;
    }
    public async Task<InterventionDetailsDto> Handle(GetInterventionDetailsQuery request, CancellationToken cancellationToken)
    {
        var intervention = await _genericRepository.GetByIdAsync(request.Id, cancellationToken);
        if (intervention == null)
        {
            _logger.LogWarning("Object {0} with id {1} not found", nameof(Domain.Intervention), request.Id);
            throw new NotFoundException(nameof(Domain.Intervention), request.Id);
        }
        var interventionDto = StaticMapper.Mapper.Map<InterventionDetailsDto>(intervention);
        return interventionDto;
    }
}
