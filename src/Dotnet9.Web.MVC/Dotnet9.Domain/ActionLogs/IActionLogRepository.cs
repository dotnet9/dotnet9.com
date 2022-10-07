using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.ActionLogs;

public interface IActionLogRepository : IRepository<ActionLog>
{
    Task<int> CountIPIn24HoursAsync();

    Task<int> CountNotFoundIn24HoursAsync();

    Task<LatestActionLog> GetLatestActionLogAsync(DateTimeOffset? request);

    Task<Top10AccessPage> CountTop10AccessPagesAsync();

    Task<Top10Search> CountTop10SearchAsync();
}