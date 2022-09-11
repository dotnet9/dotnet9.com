namespace Dotnet9.WebAPI.Domain.Shared.ActionLogs;

public record QueryActionLogResponse(IEnumerable<ActionLogDTO>? ActionLogs, long TotalCount);