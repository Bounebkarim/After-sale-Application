using Contracts;
using Intervention.Domain.Enums;
using MassTransit;
using Intervention.Application.Contracts.Persistence;

namespace Intervention.Infrastructure.InterventionConsumers;
public class UpdateInterventionConsumer : IConsumer<ReclamationCreatedEvent>
{
    private readonly IGenericRepository<Domain.Intervention> _repository;

    public UpdateInterventionConsumer(IGenericRepository<Domain.Intervention> repository)
    {
        _repository = repository;
    }
    public async Task Consume(ConsumeContext<ReclamationCreatedEvent> context)
    {
        var specification = new GetByReclamationSpecification(context.Message.Id);
        var intervention = (await _repository.FindWithSpecificationPattern(specification)).FirstOrDefault();
        if (intervention == null)
        {
            //impliment error and log in here
        }
        intervention.ClientName = context.Message.ClientName;
        intervention.ClientLastName = context.Message.ClientLastName;
        intervention.Title = context.Message.Title;
        intervention.InterventionStatus = (InterventionStatus)context.Message.EtatReclamation;
        intervention.ProblemType = (ProblemType)context.Message.problemType;
        intervention.Severity = (Severity)context.Message.Severity;

        await _repository.UpdateAsync(intervention);
    }
}