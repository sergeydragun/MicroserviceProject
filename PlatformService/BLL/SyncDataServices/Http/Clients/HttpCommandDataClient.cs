using PlatformService.BLL.DTO;
using PlatformService.BLL.Services;
using PlatformService.SyncDataServices.Http.Interfaces;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http.Clients
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpCommandDataClient> _logger;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, ILogger<HttpCommandDataClient> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDTO platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json");

            var response =await _httpClient.PostAsync($"{_configuration["CommandService"]}platforms", httpContent);

            if(response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Sync POST to CommandService was ok");
                Console.WriteLine("Sync POST to CommandService was ok");
            }
            else
            {
                _logger.LogInformation("Sync POST to CommandService was not ok");
                Console.WriteLine("Sync POST to CommandService was not ok");
            }
        }
    }
}
