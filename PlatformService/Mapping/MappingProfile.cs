using AutoMapper;
using PlatformService.BLL.DTO;
using PlatformService.Entities;

namespace PlatformService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();

            CreateMap<PlatformReadDTO, PlatformPublishedDTO>();

            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId, d => d.MapFrom(src => src.Id));
        }

    }
}
