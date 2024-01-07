using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Client.Application.Features.ClientFeatures.Queries.GetClientDetails;

public sealed record GetClientDetailQuery(Guid Id) : IRequest<ClientDetailDto>;