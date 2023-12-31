using AutoMapper;
using CommandsService.Data.Intrefaces;
using CommandsService.DTO;
using CommandsService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : BaseController
    {
        private readonly IUnitOfWork _database;

        public CommandsController(ILogger<BaseController> logger, IMapper mapper, IHttpContextAccessor contextAccessor,
                                  IUnitOfWork database) : base(logger, mapper, contextAccessor)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommandsForPlatform(int platformId)
        {
            var commands = await _database.Commands.GetCommandsForPlatform(platformId);

            if(commands.Count == 0)
                return NotFound();

            var commandsReadDTO = _mapper.Map<List<CommandReadDTO>>(commands);

            return Ok(commandsReadDTO);
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public async Task<IActionResult> GetCommandForPlatform(int platformId, int commandId)
        {
            if(!await _database.Platforms.PlatformExits(platformId))
            {
                _logger.LogWarning($"{platformId} is not exist");
                return NotFound();
            }

            var command = _database.Commands.GetCommand(platformId, commandId);

            if(command == null)
                return NotFound();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(command);

            return Ok(commandReadDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommandForPlatform(int platformId, CommandCreateDTO commandCreate)
        {
            if (!await _database.Platforms.PlatformExits(platformId))
            {
                _logger.LogWarning($"{platformId} is not exist");
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandCreate);

            await _database.Commands.CreateCommand(platformId, command);
            var result = await _database.SaveAsync();

            if(result)
            {
                var commandReadDTO = _mapper.Map<CommandReadDTO>(command);
                return CreatedAtRoute(nameof(GetCommandForPlatform),
                        new { platformId = platformId, commandId = commandReadDTO.Id }, commandReadDTO);
            }

            return BadRequest();
        }

    }
}
