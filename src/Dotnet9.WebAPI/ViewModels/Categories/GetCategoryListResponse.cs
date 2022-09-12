namespace Dotnet9.WebAPI.ViewModels.Categories;

public record GetCategoryListResponse(IEnumerable<CategoryDto>? Categories, long TotalCount);