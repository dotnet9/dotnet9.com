namespace Dotnet9.Application.Contracts.ActionLogs;

public class ActionLogDto : EntityDto
{
    public string? UId { get; set; }
    public string? UA { get; set; }
    public string? OS { get; set; }
    public string? Browser { get; set; }
    public string? Referer { get; set; }
    public string? AccessName { get; set; }
    public string? Original { get; set; }
    public string? IP { get; set; }
    public string? Url { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Method { get; set; }
    public string? Arguments { get; set; }
    public double Duration { get; set; }
    public DateTimeOffset CreateDate { get; set; }
}