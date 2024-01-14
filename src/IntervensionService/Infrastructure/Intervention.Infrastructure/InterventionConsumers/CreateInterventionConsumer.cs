using Contracts;
using Intervention.Application.Contracts.Persistence;
using Intervention.Domain.Enums;
using MassTransit;

namespace Intervention.Infrastructure.InterventionConsumers;
public class CreateInterventionConsumer : IConsumer<ReclamationCreatedEvent>
{
    private readonly IGenericRepository<Domain.Intervention> _repository;

    public CreateInterventionConsumer(IGenericRepository<Domain.Intervention> repository)
    {
        _repository = repository;
    }
    public async Task Consume(ConsumeContext<ReclamationCreatedEvent> context)
    {
        var intervention = new Domain.Intervention(Guid.NewGuid())
        {
            ClientName = context.Message.ClientName,
            ClientLastName = context.Message.ClientLastName,
            Title = context.Message.Title,
            InterventionStatus = (InterventionStatus)context.Message.EtatReclamation,
            ProblemType = (ProblemType)context.Message.problemType,
            ReclamationId = Guid.Parse(context.Message.Id.ToString()),
            Severity = (Severity)context.Message.Severity,
            Description = context.Message.Description,
            ClientId = Guid.Parse(context.Message.ClientId.ToString())
        };
        await _repository.CreateAsync(intervention);
    }
}
