using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddDbContext<MyDbContext>(options => options.UseInMemoryDatabase("InMem"));
            builder.Services.ConfigureUnitOfWork();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.ConfigurePlatformService();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.PrepPopulation();

            app.Run();
        }
    }
}