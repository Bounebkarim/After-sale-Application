using Intervention.Application.Contracts.Logging;
using Intervention.Application.Contracts.Persistence;
using Intervention.Application.Contracts.Specs;
using Intervention.Application.Mapper;
using MediatR;

namespace Intervention.Application.Features.InterventionFeature.Queries.GetInterventions;
public class GetInterventionsQueryHandler : IRequestHandler<GetInterventionsQuery, Pagination<InterventionDto>>
{
    private readonly IGenericRepository<Domain.Intervention> _genericRepository;
    private readonly IAppLogger<GetInterventionsQueryHandler> _logger;

    public GetInterventionsQueryHandler(IGenericRepository<Domain.Intervention> genericRepository, IAppLogger<GetInterventionsQueryHandler> logger)
    {
        _genericRepository = genericRepository;
        _logger = logger;
    }

    public async Task<Pagination<InterventionDto>> Handle(GetInterventionsQuery request, CancellationToken cancellationToken)
    {
        var data = await _genericRepository.GetPaginationAsync(request.SpecParams, cancellationToken);
        var reclamations = StaticMapper.Mapper.Map<Pagination<InterventionDto>>(data);
        return reclamations;
    }
}
