namespace Dotnet9.WebAPI.ViewModels.ActionLogs;

// ReSharper disable once InconsistentNaming
public record ActionLogDTO(Guid Id,
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