using CommandsService.Data;
using CommandsService.Mapping;

namespace CommandsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHttpContextAccessor();
            builder.Services.ConfigureUnitOfWork();
            builder.Services.ConfigureDbContext(builder.Configuration);
            builder.Services.ConfigureSingletonEventProcessor();
            builder.Services.ConfigureHostedMessageBusSubscriber();
            builder.Services.ConfigurePlatformDataClient();

            Console.WriteLine(builder.Configuration["RabbitMQHost"]);
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.PrepPopulation();

            app.Run();
        }
    }
}
