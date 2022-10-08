namespace Dotnet9.Web.Service.Categories;

internal class CategoryService : ICategoryService
{
    private readonly Dotnet9DbContext _dbContext;

    public CategoryService(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<CategoryBrief>> GetCategoriesAsync()
    {
        return _dbContext.Categories!.Select(c => new CategoryBrief(c.Slug, c.Name, c.Description)).ToListAsync();
    }
}