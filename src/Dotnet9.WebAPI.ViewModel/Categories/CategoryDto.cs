namespace Dotnet9.WebAPI.ViewModel.Categories;

public class CategoryDto
{
    public Guid Id { get; set; }
    public int SequenceNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Visible { get; set; }
    public string? ParentName { get; set; }
}