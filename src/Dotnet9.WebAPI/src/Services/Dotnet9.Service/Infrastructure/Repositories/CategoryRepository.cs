namespace Dotnet9.Service.Infrastructure.Repositories;

public class CategoryRepository : Repository<Dotnet9DbContext, Category, Guid>, ICategoryRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public CategoryRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
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

    public async Task<List<CategoryBrief>> GetAllBriefAsync()
    {
        //TimeSpan? timeSpan = null;
        //var key = $"{nameof(CategoryRepository)}_{nameof(GetAllBriefAsync)}";
        //var cats = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        //{
        var categories = await Context.Set<Category>()
            .Select(cat => new CategoryBrief(cat.Name, cat.Slug, cat.Cover,
                cat.Description,
                Context.Set<BlogCategory>().Count(d => d.CategoryId == cat.Id))).ToListAsync();
        var distinctCategories = from cat in categories
            where cat.BlogCount > 0
            orderby cat.BlogCount descending
            select cat;
        var distinctCategoryList = distinctCategories.ToList();
        return distinctCategoryList;
        //    if (datas != null)
        //    {
        //        return new CacheEntry<List<CategoryBrief>>(datas, TimeSpan.FromDays(3))
        //        {
        //            SlidingExpiration = TimeSpan.FromMinutes(5)
        //        };
        //    }

        //    timeSpan = TimeSpan.FromSeconds(5);
        //    return new CacheEntry<List<CategoryBrief>>(datas);
        //}, options =>
        //    options.AbsoluteExpirationRelativeToNow = timeSpan);

        //return cats;
    }
}