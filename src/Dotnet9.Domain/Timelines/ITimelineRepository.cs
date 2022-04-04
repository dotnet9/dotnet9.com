using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Timelines;

public interface ITimelineRepository : IRepository<Timeline>
{
    Task<List<Timeline>> GetListAsync();
}