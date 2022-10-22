namespace Dotnet9.Web.ViewModel.Categories;

public record CategoryBrief(string Slug, string Name, string? Description, int BlogCount = 0, Guid? Id = null);