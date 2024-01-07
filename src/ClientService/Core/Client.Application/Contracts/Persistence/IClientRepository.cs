using Client.Application.Contracts.Specs;

namespace Client.Application.Contracts.Persistence
{
    public interface IClientRepository : IGenericRepository<Domain.Client>
    {
        Task<bool> CinExist(string cin, CancellationToken cancellationToken);
        Task<bool> ClientCinExist(Guid clientId, string cin, CancellationToken cancellationToken);
        Task<Pagination<Domain.Client>> GetClientsAsync(ClientSpecParams specParams);
    }
}