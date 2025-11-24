using AutoMapper;
using EstateAgency.Application.Contracts.Dto;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Enums;

namespace EstateAgency.Application.Contracts;

/// <summary>
/// AutoMapper profile that defines mappings between domain entities and DTOs.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes the mapping configuration.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Counterparty, CounterpartyGetDto>();
        CreateMap<CounterpartyEditDto, Counterparty>();

        CreateMap<RealEstate, RealEstateGetDto>();
        CreateMap<RealEstateEditDto, RealEstate>()
            .ForMember(dest => dest.Type,
               opt => opt.MapFrom(src => Enum.Parse<RealEstateType>(src.Type, true)))
            .ForMember(dest => dest.Purpose,
               opt => opt.MapFrom(src => Enum.Parse<RealEstatePurpose>(src.Purpose, true)));

        CreateMap<EstateAgency.Domain.Entities.Application, ApplicationGetDto>();
        CreateMap<ApplicationEditDto, EstateAgency.Domain.Entities.Application>()
            .ForMember(dest => dest.Type,
               opt => opt.MapFrom(src => Enum.Parse<ApplicationType>(src.Type, true)));
    }
}
