namespace Dotnet9.WebAPI.ViewModels.ActionLogs;

public record QueryActionLogRequest(string? Keywords, int PageIndex, int PageSize);