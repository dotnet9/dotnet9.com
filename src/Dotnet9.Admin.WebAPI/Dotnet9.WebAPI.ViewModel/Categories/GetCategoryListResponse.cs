namespace Dotnet9.WebAPI.ViewModel.Categories;

public record GetCategoryListResponse(IEnumerable<CategoryDto>? Records, long Count);