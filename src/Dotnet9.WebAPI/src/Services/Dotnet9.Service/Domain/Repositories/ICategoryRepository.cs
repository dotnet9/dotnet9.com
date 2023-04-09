namespace Dotnet9.Service.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category, Guid>
{
    Task<Category?> FindByIdAsync(Guid id);
    Task<Category?> FindByNameAsync(string name);
    Task<Category?> FindBySlugAsync(string slug);
}