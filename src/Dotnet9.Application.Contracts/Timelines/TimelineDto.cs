namespace Dotnet9.Application.Contracts.Timelines;

public class TimelineDto
{
    public DateTime Time { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}