namespace Dotnet9.WebAPI.Infrastructure.Categories;

internal class CategoryRepository : ICategoryRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public CategoryRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var logs = await _dbContext.Categories!.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Category?> FindByIdAsync(Guid id)
    {
        return await _dbContext.Categories!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category?> FindByNameAsync(string name)
    {
        return await _dbContext.Categories!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Category?> FindBySlugAsync(string slug)
    {
        return await _dbContext.Categories!.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<(Category[]? Categories, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize)
    {
        var query = _dbContext.Categories!.AsQueryable();
        if (!keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Name, $"%{keywords}%")
                || EF.Functions.Like(log.Slug, $"%{keywords}%")
                || (log.Description != null && EF.Functions.Like(log.Description!, $"%{keywords}%")));
        }

        var categoriesFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (await categoriesFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}