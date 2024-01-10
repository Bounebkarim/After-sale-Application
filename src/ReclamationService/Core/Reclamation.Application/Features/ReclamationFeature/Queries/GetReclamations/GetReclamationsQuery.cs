using MediatR;
using Reclamation.Application.Contracts.Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamations;
public sealed record GetReclamationsQuery(SpecParams SpecParams) : IRequest<Pagination<ReclamationDto>>;
