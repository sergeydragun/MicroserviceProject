using AutoMapper;
using PlatformService.BLL.DTO;
using PlatformService.Models;

namespace PlatformService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Platform, PlatformReadDTO>();

            CreateMap<PlatformCreateDTO, Platform>();
        }

    }
}
