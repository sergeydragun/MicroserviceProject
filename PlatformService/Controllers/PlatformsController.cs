using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.BLL.DTO;
using PlatformService.BLL.Interfaces;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : BaseController
    {
        protected readonly IPlatformService _platformService;
        public PlatformsController(ILogger<BaseController> logger, IMapper mapper, IHttpContextAccessor contextAccessor,
                                    IPlatformService platformService) : base(logger, mapper, contextAccessor)
        {
            _platformService = platformService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatform()
        {
            var platforms = await _platformService.GetAllPlatformReadAsync();

            return Ok(platforms);
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<PlatformReadDTO>> GetPlatformById(int id)
        {
            var platformDTO = await _platformService.GetPlatformReadByIdAsync(id);

            if (platformDTO == null)
                return NotFound();

            return Ok(platformDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatform(PlatformCreateDTO platfromDTO)
        {
            var platform = _mapper.Map<Platform>(platfromDTO);
            var result = await _platformService.CreatePlatformAsync(platform);

            if (result)
            {
                var platformReadDTO = _mapper.Map<PlatformReadDTO>(platform);
                var route = CreatedAtRoute(nameof(GetPlatformById), new { id = platformReadDTO.Id }, platformReadDTO);
                return route;
            }

            return BadRequest();
        }

        [Route("DeletePlatform/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePlatform(int id)
        {
            bool res = await _platformService.DeletePlatformAsync(id);

            if (res)
                return RedirectToAction(nameof(GetPlatform));

            return BadRequest();
        }
    }
}
