namespace Dotnet9.WebAPI.Domain.Shared.ActionLogs;

public record ActionLogDto(Guid Id,
    string? UId,
    string? UA,
    string? OS,
    string? Browser,
    string? Referer,
    string? AccessName,
    string? Original,
    string? IP,
    string? Url,
    string? Controller,
    string? Action,
    string? Method,
    string? Arguments,
    double Duration);