using AutoMapper;
using Intervention.Application.Features.InterventionFeature.Commands.CreateIntervention;
using Intervention.Application.Features.InterventionFeature.Commands.UpdateIntervention;
using Intervention.Application.Features.InterventionFeature.Queries.GetInterventionDetails;
using Intervention.Application.Features.InterventionFeature.Queries.GetInterventions;

namespace Intervention.Application.Mapper;

public class InterventionProfile : Profile
{
    public InterventionProfile()
    {
        CreateMap<CreateInterventionCommand, Domain.Intervention>().ReverseMap();
        CreateMap<UpdateInterventionCommand, Domain.Intervention>().ReverseMap();
        CreateMap<Domain.Intervention, InterventionDto>();
        CreateMap<Domain.Intervention, InterventionDetailsDto>();
    }
}