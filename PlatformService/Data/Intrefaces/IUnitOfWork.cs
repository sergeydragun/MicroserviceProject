namespace PlatformService.Data.Intrefaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPlatformRepo Platforms { get; }
        Task<bool> SaveAsync();
    }
}
