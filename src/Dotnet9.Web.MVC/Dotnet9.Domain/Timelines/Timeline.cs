namespace Dotnet9.Domain.Timelines;

public class Timeline : EntityBase
{
    public DateTimeOffset Time { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}