namespace Dotnet9.Service.Services;

public class BlogService : ServiceBase
{
    public BlogService() : base("/api/blogs")
    {
    }

    public async Task<List<BlogBrief>> GetRecommendAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new BlogsQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }

    [RoutePattern(pattern: "/api/albums/{slug}/blogs")]
    public async Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(IEventBus eventBus,
        CancellationToken cancellationToken, [FromRoute] string slug, [FromQuery] int pageSize = 20,
        [FromQuery] int page = 1)
    {
        var queryEvent = new SearchBlogsByAlbumQuery()
        {
            AlbumSlug = slug,
            PageSize = pageSize,
            Page = page
        };
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return new GetBlogListByAlbumSlugResponse(true, queryEvent.AlbumName, queryEvent.Result.Result,
            queryEvent.Result.Total, queryEvent.Result.TotalPages);
    }
}