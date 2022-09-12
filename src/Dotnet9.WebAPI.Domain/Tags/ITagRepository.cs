namespace Dotnet9.WebAPI.Domain.Tags;

public interface ITagRepository
{
    Task<(Tag[]? Tags, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize);
    Task<int> DeleteAsync(Guid[] ids);
    Task<Tag?> FindByIdAsync(Guid id);
    Task<Tag?> FindByNameAsync(string name);
}