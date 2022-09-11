namespace Dotnet9.WebAPI.ViewModels.Categories;

public record QueryCategoryResponse(IEnumerable<CategoryDTO>? Categories, long TotalCount);