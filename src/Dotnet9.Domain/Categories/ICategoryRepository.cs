using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> FindByNameAsync(string name);
    Task<Category?> FindBySlugAsync(string slug);
    Task<List<CategoryCount>> GetListCountAsync();
}