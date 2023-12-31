using PlatformService.BLL.AsyncDataServices.Interfaces;
using PlatformService.BLL.DTO;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.BLL.AsyncDataServices.Clients
{
    public class MessageBusClient : IMessageBusClient, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MessageBusClient> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration, ILogger<MessageBusClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("Connected to message bus");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("RabbitMQ Connection Shutdown");
        }

        public void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO)
        {
            var message = JsonSerializer.Serialize(platformPublishedDTO);

            if(!_connection.IsOpen)
            {
                _logger.LogError("RabbitMQ connection is close not sending");
                return;
            }

            _logger.LogInformation("RabbitMQ connection is open to sending message");
            SendMessage(message);
            
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);

            _logger.LogInformation("We've send message");
        }

        public void Dispose()
        {
            _logger.LogInformation("Message bus disposed");
            if(_channel.IsOpen)
            {
                _channel.Close();
                _channel.Close();
            }

            GC.SuppressFinalize(this);
        }
    }
}
