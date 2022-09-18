namespace Dotnet9.WebAPI.ViewModel.Categories;

public record GetCategoryListResponse(IEnumerable<CategoryDto>? Categories, long Total);