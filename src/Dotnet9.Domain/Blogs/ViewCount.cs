namespace Dotnet9.Domain.Blogs;

public class ViewCount : EntityBase
{
    public string? Original { get; set; }
    public string? Url { get; set; }
    public string? IP { get; set; }
}