namespace Dotnet9.WebAPI.ViewModel.ActionLogs;

public record GetActionLogListRequest(string? Ua, string? Os, string? Browser, string? Referer, string? Original,
    string? Ip, string? Url, string? Controller, string? Action, string? Method, string? Arguments,
    DateTime? StartCreationTime, DateTime? EndCreationTime, int Current, int PageSize);