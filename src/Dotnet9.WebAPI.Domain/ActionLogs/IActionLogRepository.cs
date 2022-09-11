namespace Dotnet9.WebAPI.Domain.ActionLogs;

public interface IActionLogRepository
{
    Task<(ActionLog[]? Logs, long Count)> QueryAsync(string? keywords, int pageIndex, int pageSize);
    Task<int> DeleteAsync(Guid[] ids);
}