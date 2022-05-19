namespace Dotnet9.Application.Contracts.Categories;

public interface ICategoryAppService
{
    Task<CategoryViewModel?> GetCategoryAsync(string? slug);

    Task<List<CategoryCountDto>> GetListCountAsync();

    Task<List<CategoryDto>> AdminListAsync();
}