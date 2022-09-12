namespace Dotnet9.WebAPI.ViewModels.Categories;

public record GetCategoryListRequest(string? Keywords, int PageIndex, int PageSize);