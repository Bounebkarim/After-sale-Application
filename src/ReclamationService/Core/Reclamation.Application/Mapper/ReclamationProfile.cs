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
        CreateMap<CreateReclamationCommand, Domain.Reclamation>().ReverseMap();
        CreateMap<UpdateReclamationCommand, Domain.Reclamation>().ReverseMap();
        CreateMap<Domain.Reclamation, ReclamationDto>();
        CreateMap<Domain.Reclamation, ReclamationDetailsDto>();
    }
}