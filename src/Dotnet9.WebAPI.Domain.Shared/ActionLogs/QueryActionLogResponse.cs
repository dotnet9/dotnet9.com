namespace Dotnet9.WebAPI.Domain.Shared.ActionLogs;

public record QueryActionLogResponse(IEnumerable<ActionLogDto>? ActionLogs, long TotalCount);