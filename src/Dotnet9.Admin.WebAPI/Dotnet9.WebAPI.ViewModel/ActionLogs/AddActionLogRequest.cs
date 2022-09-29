namespace Dotnet9.WebAPI.ViewModel.ActionLogs;

public record AddActionLogRequest(string UId, string Ua, string Os, string Browser, string Ip, string? Referer,
    string? AccessName, string? Original, string? Url, string? Controller, string? Action, string? Method,
    string? Arguments,
    double Duration);