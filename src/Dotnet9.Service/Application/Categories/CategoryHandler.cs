namespace Dotnet9.Service.Application.Categories;

public class CategoryHandler
{
    private readonly ICategoryRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public CategoryHandler(ICategoryRepository repository,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task GetListAsync(CategoriesQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        var key = $"{nameof(CategoryHandler)}_{nameof(GetListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await _repository.GetAllBriefAsync();

            if (dataFromDb?.Any() == true)
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

        if (data != null)
        {
            query.Result = new PaginatedListBase<CategoryBrief>()
            {
                Result = data
            };
        }
    }
}