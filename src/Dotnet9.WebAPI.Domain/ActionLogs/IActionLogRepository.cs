namespace Dotnet9.WebAPI.Domain.ActionLogs;

public interface IActionLogRepository
{
    Task<(ActionLog[]? Logs, long Count)> GetListAsync(GetActionLogListRequest request);
    Task<int> DeleteAsync(Guid[] ids);
}