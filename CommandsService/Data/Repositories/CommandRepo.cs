using CommandsService.Data;
using CommandsService.Data.Intrefaces;
using CommandsService.Data.Repositories;
using CommandsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data.Repositories
{
    public class CommandRepo : BaseRepository<Command>, ICommandRepo
    {
        public CommandRepo(MyDbContext context) : base(context)
        {
        }

        public async Task CreateCommand(int platformId, Command command)
        {
            command.PlatformId = platformId;
            await _db.Commands.AddAsync(command);
        }

        public async Task<Command?> GetCommand(int platformId, int commandId)
        {
            return await _db.Commands.FirstOrDefaultAsync(c => c.Id == platformId);
        }

        public async Task<List<Command>> GetCommandsForPlatform(int platformId)
        {
            return await FindByCondition(p => p.PlatformId == platformId).ToListAsync();
        }
    }
}
