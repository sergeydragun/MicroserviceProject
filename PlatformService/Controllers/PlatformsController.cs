using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.BLL.AsyncDataServices.Interfaces;
using PlatformService.BLL.DTO;
using PlatformService.BLL.Interfaces;
using PlatformService.Entities;
using PlatformService.SyncDataServices.Http.Interfaces;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : BaseController
    {
        protected readonly IPlatformService _platformService;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(ILogger<BaseController> logger, IMapper mapper, IHttpContextAccessor contextAccessor,
                                    IPlatformService platformService, ICommandDataClient commandDataClient, 
                                    IMessageBusClient messageBusClient) : base(logger, mapper, contextAccessor)
        {
            _platformService = platformService;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
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

                try
                {
                    await _commandDataClient.SendPlatformToCommand(platformReadDTO);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Could not send synchronously {ex.Message}");
                }

                try
                {
                    var platformPublishedDTO = _mapper.Map<PlatformPublishedDTO>(platformReadDTO);
                    platformPublishedDTO.Event = "Platform_Published";
                    _messageBusClient.PublishNewPlatform(platformPublishedDTO);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Could not send asynchronously {ex.Message}");
                }

                return route;    
            }

            return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeletePlatform")]
        public async Task<IActionResult> DeletePlatform(int id)
        {
            bool res = await _platformService.DeletePlatformAsync(id);

            if (res)
                return RedirectToAction(nameof(GetPlatform));

            return BadRequest();
        }
    }
}
