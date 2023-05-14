namespace Dotnet9.Service.Services;

public class CategoryService : ServiceBase
{
    public CategoryService() : base("/api/categories")
    {
    }

    public async Task<List<CategoryBrief>> GetBriefAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new CategoriesQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}