using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Application.Contracts.Exceptions;
using Client.Application.Contracts.Logging;
using Client.Application.Contracts.Persistence;
using Client.Application.Mapper;
using MediatR;

namespace Client.Application.Features.ClientFeatures.Queries.GetClientDetails;
public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, ClientDetailDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAppLogger<GetClientDetailQueryHandler> _logger;

    public GetClientDetailQueryHandler(IClientRepository clientRepository, IAppLogger<GetClientDetailQueryHandler> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }
    public async Task<ClientDetailDto> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);
        if (client == null)
        {
            _logger.LogWarning("value Not found {0} - {1}", typeof(Domain.Client), request.Id);
            throw new BadRequestException("Value not found");
        }

        var clientDto = staticMapper.Mapper.Map<ClientDetailDto>(client);
        return clientDto;
    }
}
