namespace Dotnet9.WebAPI.Domain.Categories;

public interface ICategoryRepository
{
    Task<(CategoryDto[]? Categories, long Count)> GetListAsync(GetCategoryListRequest request);
    Task<int> DeleteAsync(Guid[] ids);
    Task<Category?> FindByIdAsync(Guid id);
    Task<Category?> FindByNameAsync(string name);
    Task<Category?> FindBySlugAsync(string slug);
}