namespace Dotnet9.ASPNETCore.Filters;

public record ActionLogCreatedEvent(string UId, string? Ua, string Os, string Browser, string Ip, string? Referer,
    string? AccessName, string? Original, string? Url, string? Controller, string? Action, string? Method,
    string? Arguments,
    double Duration) : INotification;