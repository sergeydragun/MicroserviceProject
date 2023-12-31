using AutoMapper;
using CommandsService.DTO;
using CommandsService.Entities;
using PlatformService;

namespace CommandsService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<Command, CommandReadDTO>();

            CreateMap<Platform, PlatformReadDTO>();

            CreateMap<PlatformPublishedDTO, Platform>()
                .ForMember(dest => dest.ExternalID, d => d.MapFrom(src => src.Id));

            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalID, d => d.MapFrom(src => src.PlatformId))
                .ForMember(dest => dest.Commands, d => d.Ignore());
        }

    }
}
