using CommandsService.Entities;

namespace CommandsService.Data.Intrefaces
{
    public interface ICommandRepo : IBaseRepository<Command>
    {

        Task<List<Command>> GetCommandsForPlatform(int platformId);
        Task<Command> GetCommand(int platformId, int commandId);
        Task CreateCommand(int platformId, Command command);
    }
}
