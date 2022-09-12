namespace Dotnet9.WebAPI.Domain.Timelines;

public interface ITimelineRepository
{
    Task<(Timeline[]? Timelines, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize);
    Task<int> DeleteAsync(Guid[] ids);
    Task<Timeline?> FindByIdAsync(Guid id);
}