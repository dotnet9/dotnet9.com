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

    public async Task<List<CategoryBrief>?> GetAllBriefAsync()
    {
        var categories = await Context.Set<Category>()
            .Select(cat => new CategoryBrief(cat.Name, cat.Slug, cat.Cover,
                cat.Description,
                Context.Set<BlogCategory>().Count(d => d.CategoryId == cat.Id), cat.Id)).ToListAsync();
        var distinctCategories = from cat in categories
            where cat.BlogCount > 0
            orderby cat.BlogCount descending
            select cat;
        return distinctCategories.ToList();
    }
}