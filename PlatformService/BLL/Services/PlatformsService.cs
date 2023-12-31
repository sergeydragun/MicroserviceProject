using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlatformService.BLL.DTO;
using PlatformService.BLL.Interfaces;
using PlatformService.Data.Intrefaces;
using PlatformService.Entities;

namespace PlatformService.BLL.Services
{
    public class PlatformsService : BaseService, IPlatformService
    {
        public PlatformsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<bool> CreatePlatformAsync(Platform platform)
        {
            bool result;
            try
            {
                await _database.Platforms.CreateAsync(platform);
                result = await _database.SaveAsync();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public async Task<bool> DeletePlatformAsync(int id)
        {
            var platform = await _database.Platforms.FirstOrDefaultAsync(x => x.Id == id);

            if (platform == null)
            {
                return false;
            }

            _database.Platforms.Delete(platform);

            return await _database.SaveAsync();
        }

        public async Task<List<PlatformReadDTO>> GetAllPlatformReadAsync()
        {
            var platforms = await _database.Platforms.GetAll().ToListAsync();
            var platformsDTO = _mapper.Map<List<PlatformReadDTO>>(platforms);
            return platformsDTO;
        }

        public async Task<List<Platform>> GetAllPlatformsAsync()
        {
            return await _database.Platforms.GetAll().ToListAsync();
        }

        public async Task<PlatformReadDTO?> GetPlatformReadByIdAsync(int id)
        {
            var platform = await _database.Platforms.FirstOrDefaultAsync(p => p.Id == id);

            var platformDTO = _mapper.Map<PlatformReadDTO>(platform);

            return platformDTO;
        }


    }
}
