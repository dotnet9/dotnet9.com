namespace Dotnet9.Web.Service.Categories;

internal class CategoryService : ICategoryService
{
    private readonly Dotnet9DbContext _dbContext;

    public CategoryService(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CategoryBrief>> GetCategoriesAsync()
    {
        var categories = await _dbContext.Categories!.Select(c => new CategoryBrief(c.Slug, c.Name, c.Description,
                _dbContext.Set<BlogPostCategory>().Count(d => d.CategoryId == c.Id)))
            .ToListAsync();
        var distinctCategories = from cat in categories
            where cat.BlogCount > 0
            orderby cat.BlogCount descending
            select cat;
        return distinctCategories.ToList();
    }
}