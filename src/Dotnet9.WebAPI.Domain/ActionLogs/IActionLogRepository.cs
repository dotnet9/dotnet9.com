namespace Dotnet9.WebAPI.Domain.ActionLogs;

public interface IActionLogRepository
{
    Task<QueryActionLogResponse> List(string? keywords, int pageIndex, int pageSize);
    Task<int> DeleteActionLogsAsync(Guid[] ids);
}