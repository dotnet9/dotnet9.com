namespace Dotnet9.WebAPI.EFCore.Categories;

internal class CategoryRepository : ICategoryRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public CategoryRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var logs = await _dbContext.Categories.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Category?> FindByIdAsync(Guid id)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category?> FindByNameAsync(string name)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Category?> FindBySlugAsync(string slug)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<QueryCategoryResponse> QueryAsync(string? keywords, int pageIndex, int pageSize)
    {
        Expression<Func<Category, bool>> whereLambda;
        if (keywords.IsNullOrWhiteSpace())
        {
            whereLambda = log => true;
        }
        else
        {
            whereLambda = log =>
                EF.Functions.Like(log.Name, $"%{keywords}%")
                || EF.Functions.Like(log.Slug, $"%{keywords}%")
                || EF.Functions.Like(log.Description, $"%{keywords}%");
        }

        var categoryFromDb = _dbContext.Categories.Where(whereLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        var categoryCount = categoryFromDb.LongCount();
        if (categoryCount <= 0)
        {
            return new QueryCategoryResponse(null, 0);
        }

        var categoryDatas = await categoryFromDb.ToListAsync();
        return new QueryCategoryResponse(categoryDatas.Adapt<CategoryDTO[]>(), categoryCount);
    }
}