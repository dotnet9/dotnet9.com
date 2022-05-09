namespace Dotnet9.Application.Contracts.ActionLogs;

public class LatestActionLogItemDto
{
    public string? OS { get; set; }
    public string? Browser { get; set; }
    public string? IP { get; set; }
    public string? Url { get; set; }
    public string? CreateDate { get; set; }
}