namespace Dotnet9.WebAPI.ViewModels.ActionLogs;

public class AddActionLogRequest
{
    public string UId { get; set; } = null!;
    public string UA { get; set; } = null!;
    public string OS { get; set; } = null!;
    public string Browser { get; set; } = null!;
    public string IP { get; set; } = null!;
    public string? Referer { get; set; }
    public string? AccessName { get; set; }
    public string? Original { get; set; }
    public string? Url { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Method { get; set; }
    public string? Arguments { get; set; }
    public double Duration { get; set; }
}