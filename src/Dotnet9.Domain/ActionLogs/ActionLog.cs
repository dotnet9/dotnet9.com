namespace Dotnet9.Domain.ActionLogs;

public class ActionLog : EntityBase
{
    public string? Original { get; set; }
    public string? IP { get; set; }
    public string? Url { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Method { get; set; }
    public string? Arguments { get; set; }
    public double Duration { get; set; }
}