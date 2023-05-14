namespace Dotnet9.Service.Domain.Repositories;

public interface ITimelineRepository : IRepository<Timeline, Guid>
{
    Task<List<TimelineDto>?> GetListAsync();
}