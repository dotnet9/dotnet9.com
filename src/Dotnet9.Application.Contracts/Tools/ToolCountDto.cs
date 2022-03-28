namespace Dotnet9.Application.Contracts.Tools;

public class ToolCountDto
{
    public string? Name { get; set; }
    public string? RelativeUrl { get; set; }
    public List<ToolCountDto>? Children { get; set; }
}