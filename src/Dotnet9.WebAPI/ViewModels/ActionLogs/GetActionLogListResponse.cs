namespace Dotnet9.WebAPI.ViewModels.ActionLogs;

public record GetActionLogListResponse(IEnumerable<ActionLogDto>? ActionLogs, long TotalCount);