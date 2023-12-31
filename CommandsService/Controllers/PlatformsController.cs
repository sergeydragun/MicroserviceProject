using AutoMapper;
using CommandsService.Data.Intrefaces;
using CommandsService.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : BaseController
    {
        private readonly IUnitOfWork _database;

        public PlatformsController(ILogger<BaseController> logger, IMapper mapper, IHttpContextAccessor contextAccessor,
                                   IUnitOfWork database) : base(logger, mapper, contextAccessor)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            var platforms = await _database.Platforms.GetAll().ToListAsync();

            var platformsDTO = _mapper.Map<List<PlatformReadDTO>>(platforms);

            return Ok(platformsDTO);
        }

        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound Post # Command Service");

            _logger.LogInformation("Inbound Post # Command Service");

            return Ok("Inbound test of from Platforms Controller");
        }
    }
}
