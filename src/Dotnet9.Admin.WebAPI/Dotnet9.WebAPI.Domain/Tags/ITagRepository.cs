namespace Dotnet9.WebAPI.Domain.Tags;

public interface ITagRepository
{
    Task<(TagDto[]? Tags, long Count)> GetListAsync(GetTagListRequest request);
    Task<int> DeleteAsync(Guid[] ids);
    Task<Tag?> FindByIdAsync(Guid id);
    Task<Tag?> FindByNameAsync(string name);
}