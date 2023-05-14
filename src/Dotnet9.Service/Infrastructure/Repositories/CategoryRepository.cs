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

    public async Task<List<CategoryBrief>?> GetAllBriefAsync()
    {
        async Task<List<CategoryBrief>> ReadDataFromDb()
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

        TimeSpan? timeSpan = null;
        var key = $"{nameof(CategoryRepository)}_{nameof(GetAllBriefAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<List<CategoryBrief>>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<List<CategoryBrief>>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }
}