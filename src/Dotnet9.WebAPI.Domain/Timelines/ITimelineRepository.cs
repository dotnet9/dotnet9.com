namespace Dotnet9.WebAPI.Domain.Timelines;

public interface ITimelineRepository
{
    Task<(Timeline[]? Timelines, long Count)> GetListAsync(GetTimelineListRequest request);
    Task<int> DeleteAsync(Guid[] ids);
    Task<Timeline?> FindByIdAsync(Guid id);
}