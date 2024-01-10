using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        CreateMap<CreateClientCommand, Domain.Client>().ReverseMap();
        CreateMap<UpdateClientCommand, Domain.Client>().ReverseMap();
        CreateMap<Pagination<Domain.Client>, Pagination<ClientDto>>().ReverseMap();
        CreateMap<Domain.Client, ClientDto>();
        CreateMap<Domain.Client, ClientDetailDto>();
    }
}
