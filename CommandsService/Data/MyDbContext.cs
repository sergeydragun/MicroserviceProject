using Microsoft.EntityFrameworkCore;
using CommandsService.Entities;

namespace CommandsService.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
