namespace Dotnet9.Application.Contracts.Blogs;

public class ViewCountDto
{
    public string Original = null!;
    public string Url { get; set; } = null!;
    public string IP { get; set; } = null!;
    public int Count { get; set; }
}