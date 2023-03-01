namespace Dotnet9.WebAPI.ViewModel.Categories;

public record GetCategoryListBriefResponse(IEnumerable<CategoryBriefDto>? Records, long Count);