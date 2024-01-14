using AutoMapper;
using Intervention.Application.Contracts.Specs;
using Intervention.Application.Features.InterventionFeature.Commands.CreateIntervention;
using Intervention.Application.Features.InterventionFeature.Commands.UpdateIntervention;
using Intervention.Application.Features.InterventionFeature.Queries.GetInterventionDetails;
using Intervention.Application.Features.InterventionFeature.Queries.GetInterventions;

namespace Intervention.Application.Mapper;

public class InterventionProfile : Profile
{
    public InterventionProfile()
    {
        CreateMap(typeof(CreateInterventionCommand), typeof(Domain.Intervention))
                            .ConstructUsing((src,context)=>new Domain.Intervention(Guid.NewGuid())).ReverseMap();
        CreateMap<UpdateInterventionCommand, Domain.Intervention>()
                            .ConstructUsing((src, context) => new Domain.Intervention(src.Id)).ReverseMap();
        CreateMap<Domain.Intervention, InterventionDto>();
        CreateMap<Domain.Intervention, InterventionDetailsDto>();
        CreateMap<Pagination<Domain.Intervention>, Pagination<InterventionDto>>().ReverseMap();
    }
}