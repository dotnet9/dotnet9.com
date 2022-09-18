namespace Dotnet9.WebAPI.ViewModel.ActionLogs;

public record ActionLogDto(Guid Id,
    string UId,
    string Ua,
    string Os,
    string Browser,
    string? Referer,
    string? AccessName,
    string? Original,
    string Ip,
    string? Url,
    string? Controller,
    string? Action,
    string? Method,
    string? Arguments,
    double Duration,
    DateTime CreateTime);