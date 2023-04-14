namespace Dotnet9.Service.Services;

public class BlogService : ServiceBase
{
    public BlogService() : base("/api/blog")
    {
    }

    public async Task<List<BlogBrief>> GetRecommendAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new BlogsQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}