namespace Dotnet9.WebAPI.ViewModel.Categories;

public record CategoryDto
{
    public Guid Id { get; set; }
    public int SequenceNumber { get; set; }
    public string Name { get; set; } = null!;
    public int BlogPostCount { get; set; }
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public string? Description { get; set; }
    public bool Visible { get; set; }
    public Guid? ParentId { get; set; }
    public string? ParentName { get; set; }
    public DateTime CreationTime { get; set; }
}