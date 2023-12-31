using AutoMapper;
using CommandsService.Entities;
using CommandsService.SyncDataServices.gRPC.Interfaces;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SyncDataServices.gRPC.Clients
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<PlatformDataClient> _logger;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper, ILogger<PlatformDataClient> logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public List<Platform> ReturnAllPlatforms()
        {
            _logger.LogInformation($"Calling Grpc Service {_configuration["GrpcPlatform"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);

            var client = new GrpcPlatform.GrpcPlatformClient(channel);

            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllPlatforms(request);
                return _mapper.Map<List<Platform>>(reply.Platform);
            }
            catch ( Exception ex )
            {
                _logger.LogError($"Could not call Grpc Server: {ex.Message}");
                return [];
            }
        }
    }
}
