namespace Dotnet9.Service.Application.Categories;

public class CategoryHandler
{
    private readonly ICategoryRepository _repository;
    private readonly RedisClient _redisClient;

    public CategoryHandler(ICategoryRepository repository,
        RedisClient redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetListAsync(CategoriesQuery query, CancellationToken cancellationToken)
    {
        var key = $"{nameof(CategoryHandler)}_{nameof(GetListAsync)}";

        var data = await _redisClient.GetAsync<List<CategoryBrief>>(key);
        if (data == null)
        {
            data = await _repository.GetAllBriefAsync();
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }

        if (data != null)
        {
            query.Result = new PaginatedListBase<CategoryBrief>()
            {
                Result = data
            };
        }
    }
}