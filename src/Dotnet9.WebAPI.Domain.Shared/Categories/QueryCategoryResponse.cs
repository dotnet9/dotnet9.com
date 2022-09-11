namespace Dotnet9.WebAPI.Domain.Shared.Categories;

public record QueryCategoryResponse(IEnumerable<CategoryDTO>? Categories, long TotalCount);