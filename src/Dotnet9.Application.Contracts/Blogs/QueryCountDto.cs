namespace Dotnet9.Application.Contracts.Blogs;

public class QueryCountDto
{
    public string Original = null!;
    public string Key { get; set; } = null!;
    public string IP { get; set; } = null!;
    public int Count { get; set; }
}