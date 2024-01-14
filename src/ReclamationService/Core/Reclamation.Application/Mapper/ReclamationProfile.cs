using AutoMapper;
using Reclamation.Application.Contracts.Specs;
using Reclamation.Application.Features.ReclamationFeature.Commands.CreateReclamation;
using Reclamation.Application.Features.ReclamationFeature.Commands.UpdateReclamation;
using Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamationDetails;
using Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamations;

namespace Reclamation.Application.Mapper;

public class ReclamationProfile : Profile
{
    public ReclamationProfile()
    {
        CreateMap(typeof(CreateReclamationCommand), typeof(Domain.Reclamation))
                                .ConstructUsing((src, context) => new Domain.Reclamation(Guid.NewGuid()));
        CreateMap<UpdateReclamationCommand, Domain.Reclamation>()
                                .ConstructUsing((src, context) => new Domain.Reclamation(src.Id));
        CreateMap<Domain.Reclamation, ReclamationDto>();
        CreateMap<Domain.Reclamation, ReclamationDetailsDto>();
        CreateMap<Pagination<Domain.Reclamation>, Pagination<ReclamationDto>>().ReverseMap();
    }
}