namespace Dotnet9.Application.Contracts.Blogs;

public class ViewCountForCreationOrUpdateDto
{
    public ViewCountForCreationOrUpdateDto(string? original, string? ip, string? url)
    {
        Original = original;
        Url = url;
        IP = ip;
    }

    public string? Original { get; set; }
    public string? Url { get; set; }
    public string? IP { get; set; }
    public int Count { get; set; }
}