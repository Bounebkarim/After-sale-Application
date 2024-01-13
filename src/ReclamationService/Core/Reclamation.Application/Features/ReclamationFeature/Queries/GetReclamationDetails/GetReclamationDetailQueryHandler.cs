using MediatR;
using Reclamation.Application.Contracts.Exceptions;
using Reclamation.Application.Contracts.Logging;
using Reclamation.Application.Contracts.Persistence;
using Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamations;
using Reclamation.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamationDetails;
public class GetReclamationDetailQueryHandler : IRequestHandler<GetReclamationDetailsQuery, ReclamationDetailsDto>
{
    private readonly IGenericRepository<Domain.Reclamation> _genericRepository;
    private readonly IAppLogger<GetReclamationDetailQueryHandler> _logger;

    public GetReclamationDetailQueryHandler(IGenericRepository<Domain.Reclamation> genericRepository, 
                                            IAppLogger<GetReclamationDetailQueryHandler> logger)
    {
        this._genericRepository = genericRepository;
        this._logger = logger;
    }
    public async Task<ReclamationDetailsDto> Handle(GetReclamationDetailsQuery request, CancellationToken cancellationToken)
    {
        var reclamation = await _genericRepository.GetByIdAsync(request.id, cancellationToken);
        if (reclamation == null)
        {
            _logger.LogWarning("Object {0} with id {1} not found", nameof(Domain.Reclamation), request.id);
            throw new NotFoundException(nameof(Domain.Reclamation), request.id);
        }
        var reclamationDto = staticMapper.Mapper.Map<ReclamationDetailsDto>(reclamation);
        return reclamationDto;
    }
}
