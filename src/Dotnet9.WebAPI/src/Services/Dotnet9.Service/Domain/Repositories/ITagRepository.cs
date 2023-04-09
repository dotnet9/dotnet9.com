namespace Dotnet9.Service.Domain.Repositories;

public interface ITagRepository : IRepository<Tag, Guid>
{
    Task<Tag?> FindByIdAsync(Guid id);
    Task<Tag?> FindByNameAsync(string name);
}
