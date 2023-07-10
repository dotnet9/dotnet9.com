namespace Dotnet9.Service.Application.Categories;

public class CategoryHandler
{
    private readonly ICategoryRepository _repository;
    private readonly IDistributedCacheHelper _redisClient;

    public CategoryHandler(ICategoryRepository repository,
        IDistributedCacheHelper redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetListAsync(CategoriesQuery query, CancellationToken cancellationToken)
    {
        var key = $"{nameof(CategoryHandler)}_{nameof(GetListAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetAllBriefAsync());

        if (data != null)
        {
            query.Result = new PaginatedListBase<CategoryBrief>()
            {
                Result = data
            };
        }
    }
}