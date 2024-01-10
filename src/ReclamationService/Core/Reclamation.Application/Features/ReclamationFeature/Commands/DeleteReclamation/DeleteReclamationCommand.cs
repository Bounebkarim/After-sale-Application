using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Application.Features.ReclamationFeature.Commands.DeleteReclamation;
public record DeleteReclamationCommand(Guid Id) : IRequest;

