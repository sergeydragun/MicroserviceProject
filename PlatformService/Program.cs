using Microsoft.EntityFrameworkCore;
using PlatformService.BLL.SyncDataServices.Grpc;
using PlatformService.Data;
using PlatformService.Mapping;

namespace PlatformService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureDbContext(builder.Environment, builder.Configuration);
            builder.Services.ConfigureUnitOfWork();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.ConfigurePlatformService();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpCommandDataClient();
            builder.Services.ConfigureMessageBusClient();
            builder.Services.AddGrpc();
            var app = builder.Build();

            Console.WriteLine(app.Configuration["CommandService"]);
            Console.WriteLine("testing");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();
            app.MapGrpcService<GrpcPlatformService>();

            app.MapGet("/protos/platforms.proto", async context =>
            {
                await context.Response.WriteAsync(System.IO.File.ReadAllText("Protos/platforms.proto"));
            });

            app.PrepPopulation(app.Environment);

            app.Run();
        }
    }
}