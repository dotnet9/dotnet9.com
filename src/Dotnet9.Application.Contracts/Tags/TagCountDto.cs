namespace Dotnet9.Application.Contracts.Tags;

public class TagCountDto : EntityDto
{
    public string Name { get; set; } = null!;
    public int BlogPostCount { get; set; }
}