using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Tags;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> FindByNameAsync(string name);
    Task<List<TagCount>> GetListCountAsync();
}