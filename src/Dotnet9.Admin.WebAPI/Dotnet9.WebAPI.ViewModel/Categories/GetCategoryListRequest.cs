namespace Dotnet9.WebAPI.ViewModel.Categories;

public record GetCategoryListRequest(string? Keywords, int Current, int PageSize);