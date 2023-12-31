namespace CommandsService.Data.Intrefaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPlatformRepo Platforms { get; }
        ICommandRepo Commands { get; }
        Task<bool> SaveAsync();
        bool Save();
    }
}
