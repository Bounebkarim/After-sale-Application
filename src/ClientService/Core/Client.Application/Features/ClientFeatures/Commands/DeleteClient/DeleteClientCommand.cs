using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Client.Application.Features.ClientFeatures.Commands.DeleteClient;

public sealed record DeleteClientCommand(Guid Id) : IRequest;
