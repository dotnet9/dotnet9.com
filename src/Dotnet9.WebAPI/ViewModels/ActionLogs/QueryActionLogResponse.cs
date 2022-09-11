namespace Dotnet9.WebAPI.ViewModels.ActionLogs;

public record QueryActionLogResponse(IEnumerable<ActionLogDTO>? ActionLogs, long TotalCount);