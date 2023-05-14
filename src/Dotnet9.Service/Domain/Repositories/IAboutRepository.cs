namespace Dotnet9.Service.Domain.Repositories;

public interface IAboutRepository : IRepository<About, Guid>
{
    Task<About?> GetAsync();
}