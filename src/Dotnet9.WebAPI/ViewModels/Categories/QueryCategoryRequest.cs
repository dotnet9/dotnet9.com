namespace Dotnet9.WebAPI.ViewModels.Categories;

public record QueryCategoryRequest(string? Keywords, int PageIndex, int PageSize);