namespace Dotnet9.WebAPI.ViewModels.Categories;

public class AddCategoryRequest
{
    public int SequenceNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public bool Visible { get; set; }
}