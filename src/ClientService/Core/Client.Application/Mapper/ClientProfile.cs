using AutoMapper;
using Client.Application.Contracts.Specs;
using Client.Application.Features.ClientFeatures.Commands.CreateClient;
using Client.Application.Features.ClientFeatures.Commands.UpdateClient;
using Client.Application.Features.ClientFeatures.Queries.GetClientDetails;
using Client.Application.Features.ClientFeatures.Queries.GetClients;

namespace Client.Application.Mapper;
public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap(typeof(CreateClientCommand), typeof(Domain.Client)).ConstructUsing((src, context) => new Domain.Client(Guid.NewGuid())).ReverseMap();
        CreateMap<UpdateClientCommand, Domain.Client>().ConstructUsing((src, context) =>
                                                                        new Domain.Client(src.Id)).ReverseMap();
        CreateMap<Pagination<Domain.Client>, Pagination<ClientDto>>().ReverseMap();
        CreateMap<Domain.Client, ClientDto>();
        CreateMap<Domain.Client, ClientDetailDto>();
    }
}
