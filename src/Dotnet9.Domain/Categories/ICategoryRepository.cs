using System.Linq.Expressions;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> FindByNameAsync(string name);
    Task<Category?> FindBySlugAsync(string slug);
    Task<List<CategoryCount>> GetListCountAsync(Expression<Func<Category, bool>> whereLambda);
}