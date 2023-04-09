namespace Dotnet9.Service.Infrastructure.Repositories;

public class CategoryRepository : Repository<Dotnet9DbContext, Category, Guid>, ICategoryRepository
{
    public CategoryRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public Task<Category?> FindByIdAsync(Guid id)
    {
        return Context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Category?> FindByNameAsync(string name)
    {
        return Context.Categories.FirstOrDefaultAsync(x => x.Name == name);
    }

    public Task<Category?> FindBySlugAsync(string slug)
    {
        return Context.Categories.FirstOrDefaultAsync(x => x.Slug == slug);
    }
}