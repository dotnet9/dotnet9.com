namespace Dotnet9.Application.Contracts.Blogs;

public class QueryCountForCreationOrUpdateDto
{
    public QueryCountForCreationOrUpdateDto(string? original, string? ip, string key)
    {
        Original = original;
        IP = ip;
        Key = key;
    }

    public string? Original { get; set; }
    public string? IP { get; set; }
    public string? Key { get; set; }
    public int Count { get; set; }
}