namespace Dotnet9.Domain.Blogs;

public class QueryCount : EntityBase
{
    public string? Original { get; set; }
    public string? IP { get; set; }
    public string? Key { get; set; }
    public int Count { get; set; }
}