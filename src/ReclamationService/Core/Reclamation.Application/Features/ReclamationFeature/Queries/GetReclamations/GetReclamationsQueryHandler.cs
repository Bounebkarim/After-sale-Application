using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reclamation.Domain;
using Reclamation.Application.Contracts.Persistence;
using Reclamation.Application.Mapper;
using Reclamation.Application.Contracts.Specs;
using Reclamation.Application.Contracts.Logging;

namespace Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamations;
public class GetReclamationsQueryHandler : IRequestHandler<GetReclamationsQuery, Pagination<ReclamationDto>>
{
    private readonly IGenericRepository<Domain.Reclamation> _genericRepository;
    private readonly Contracts.Logging.IAppLogger<GetReclamationsQueryHandler> _logger;

    public GetReclamationsQueryHandler(IGenericRepository<Domain.Reclamation> genericRepository,IAppLogger<GetReclamationsQueryHandler> logger)
    {
        this._genericRepository = genericRepository;
        this._logger = logger;
    }

    public async Task<Pagination<ReclamationDto>> Handle(GetReclamationsQuery request, CancellationToken cancellationToken)
    {
        var data = await _genericRepository.GetPaginationAsync(request.SpecParams, cancellationToken);
        var reclamations = staticMapper.Mapper.Map<Pagination<ReclamationDto>>(data);
        return reclamations;
    }
}
