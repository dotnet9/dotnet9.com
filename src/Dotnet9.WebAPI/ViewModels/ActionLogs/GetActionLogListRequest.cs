namespace Dotnet9.WebAPI.ViewModels.ActionLogs;

public record GetActionLogListRequest(string? Keywords, int PageIndex, int PageSize);