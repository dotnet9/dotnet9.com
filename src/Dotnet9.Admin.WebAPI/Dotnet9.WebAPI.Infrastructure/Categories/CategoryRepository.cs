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
        List<Category> logs = await _dbContext.Categories!.Where(cat => ids.Contains(cat.Id)).ToListAsync();
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

    public async Task<(CategoryDto[]? Categories, long Count)> GetListAsync(GetCategoryListRequest request)
    {
        IQueryable<Category> query = _dbContext.Categories!.AsQueryable();
        if (!request.Keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Name, $"%{request.Keywords}%")
                || EF.Functions.Like(log.Slug, $"%{request.Keywords}%")
                || (log.Description != null && EF.Functions.Like(log.Description!, $"%{request.Keywords}%")));
        }

        CategoryDto[] categoriesFromDb = await query.OrderByDescending(x => x.CreationTime)
            .Skip((request.Current - 1) * request.PageSize).Take(request.PageSize).Select(category => new CategoryDto
            {
                Id = category.Id,
                SequenceNumber = category.SequenceNumber,
                Name = category.Name,
                Slug = category.Slug,
                Cover = category.Cover,
                Description = category.Description,
                Visible = category.Visible,
                ParentId = category.ParentId,
                CreationTime = category.CreationTime,
                BlogPostCount = _dbContext.Set<BlogPostCategory>()
                    .Count(blogPostCategory => blogPostCategory.CategoryId == category.Id)
            }).ToArrayAsync();
        return (categoriesFromDb, await query.LongCountAsync());
    }
}