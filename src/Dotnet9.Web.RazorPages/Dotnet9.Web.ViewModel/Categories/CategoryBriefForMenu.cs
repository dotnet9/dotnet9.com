namespace Dotnet9.Web.ViewModel.Categories;

public record CategoryBriefForMenu(int SequenceNumber, string Slug, string Name, string? Description,
    CategoryBriefForMenu[]? Children,
    int BlogCount = 0);