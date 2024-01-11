using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Application.Contracts.Persistence;
using Contracts;
using MassTransit;

namespace Client.Application.Features.ClientFeatures.CheckClient;
public class CheckClientConsumer : IConsumer<CheckClientExistenceRequest>
{
    private readonly IClientRepository _clientRepository;

    public CheckClientConsumer(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    public async Task Consume(ConsumeContext<CheckClientExistenceRequest> context)
    {
        var client = await _clientRepository.GetByIdAsync(context.Message.ClientId);
        CheckClientExistenceResponse response = new()
        {
            Exists = client != null
        };
        await context.RespondAsync(response);
    }
}
