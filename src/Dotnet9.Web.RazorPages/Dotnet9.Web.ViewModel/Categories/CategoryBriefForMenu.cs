namespace Dotnet9.Web.ViewModel.Categories;

public record CategoryBriefForMenu(string Slug, string Name, string? Description, CategoryBriefForMenu[]? Children,
    int BlogCount = 0);