namespace Dotnet9.Application.Contracts.Categories;

public class CategoryDto
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public string? Description { get; set; }
}