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
        List<CategoryBrief> categories = await _dbContext.Categories!.Select(c => new CategoryBrief(c.Slug, c.Name,
                c.Description,
                _dbContext.Set<BlogPostCategory>().Count(d => d.CategoryId == c.Id)))
            .ToListAsync();
        IOrderedEnumerable<CategoryBrief> distinctCategories = from cat in categories
            where cat.BlogCount > 0
            orderby cat.BlogCount descending
            select cat;
        return distinctCategories.ToList();
    }


    public async Task<List<CategoryBriefForMenu>?> GetCategoriesForMenuAsync()
    {
        var allCategories = await _dbContext.Categories!.Select(c => new
            {
                c.Id,
                c.ParentId,
                c.Slug,
                c.Name,
                c.Description,
                BlogCount = _dbContext.Set<BlogPostCategory>().Count(d => d.CategoryId == c.Id)
            })
            .ToListAsync();
        if (!allCategories.Any())
        {
            return null;
        }

        var rootCategories = allCategories.Where(c => c.ParentId == null).ToList();
        List<CategoryBriefForMenu> categories = allCategories.Where(c => c.ParentId == null).Select(x =>
                new CategoryBriefForMenu(x.Slug, x.Name, x.Description,
                    allCategories.Where(c => c.ParentId == x.Id).Select(c =>
                        new CategoryBriefForMenu(c.Slug, c.Name, c.Description, null, c.BlogCount)).ToArray(),
                    x.BlogCount))
            .ToList();
        return categories;
    }
}