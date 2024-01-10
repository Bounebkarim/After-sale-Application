using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamationDetails;
public sealed record GetReclamationDetailsQuery(Guid id):IRequest<ReclamationDetailsDto>
{
}
