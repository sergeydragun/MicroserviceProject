using AutoMapper;
using CommandsService.Data.Intrefaces;
using CommandsService.DTO;
using CommandsService.Entities;
using System.Text.Json;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper, ILogger<EventProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifyMessage)
        {
            _logger.LogInformation("Determining Event");

            var eventType = JsonSerializer.Deserialize<genericEventDTO>(notifyMessage);

            switch(eventType.Event)
            {
                case "Platform_Published":
                    _logger.LogInformation("Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    _logger.LogError("Could not determine Event Type");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var platformPublishedDTO = JsonSerializer.Deserialize<PlatformPublishedDTO>(platformPublishedMessage);

                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishedDTO);
                    if(!repo.Platforms.ExternalPlatformExists(platform.ExternalID))
                    {
                        repo.Platforms.Create(platform);
                        repo.Save();
                        _logger.LogInformation("Platform added");
                    }
                    else
                    {
                        _logger.LogWarning("Platform already exists...");
                    }
                }
                catch(Exception ex) 
                {
                    _logger.LogError($"Could not add Platform to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
