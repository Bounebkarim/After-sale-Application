using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Application.Contracts.Specs;
using MediatR;

namespace Client.Application.Features.ClientFeatures.Queries.GetClients;
public sealed class GetClientQuery : IRequest<Pagination<ClientDto>>
{
    public SpecParams SpecParams { get; set; }

    public GetClientQuery(SpecParams specParams)
    {
        SpecParams = specParams;
    }
}