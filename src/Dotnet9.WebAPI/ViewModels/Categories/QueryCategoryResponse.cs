namespace Dotnet9.WebAPI.ViewModels.Categories;

public record QueryCategoryResponse(IEnumerable<CategoryDto>? Categories, long TotalCount);