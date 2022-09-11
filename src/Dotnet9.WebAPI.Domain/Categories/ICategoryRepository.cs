namespace Dotnet9.WebAPI.Domain.Categories;

public interface ICategoryRepository
{
    Task<QueryCategoryResponse> QueryAsync(string? keywords, int pageIndex, int pageSize);
    Task<int> DeleteAsync(Guid[] ids);
    Task<Category?> FindByIdAsync(Guid id);
    Task<Category?> FindByNameAsync(string name);
    Task<Category?> FindBySlugAsync(string slug);
}