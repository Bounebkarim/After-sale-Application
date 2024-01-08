using Client.Application.Contracts.Logging;
using Client.Application.Contracts.Persistence;
using Client.Application.Contracts.Specs;
using Client.Application.Mapper;
using MediatR;

namespace Client.Application.Features.ClientFeatures.Queries.GetClients;
public class GetClientQueryHandler : IRequestHandler<GetClientQuery, Pagination<ClientDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAppLogger<GetClientQueryHandler> _logger;

    public GetClientQueryHandler(IClientRepository clientRepository, IAppLogger<GetClientQueryHandler> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }
    public async Task<Pagination<ClientDto>> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.GetClientsAsync(request.SpecParams,cancellationToken);
        var clientsRespond = staticMapper.Mapper.Map<Pagination<ClientDto>>(clients);
        return clientsRespond;
    }
}
