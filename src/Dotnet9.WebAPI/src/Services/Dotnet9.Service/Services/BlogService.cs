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

    [RoutePattern(pattern: "/api/blogs/{slug}")]
    public async Task<BlogDetails> GetBlogDetailsBySlugAsync(IEventBus eventBus,
        CancellationToken cancellationToken, [FromRoute] string slug)
    {
        var queryEvent = new SearchBlogDetailsBySlugQuery()
        {
            Slug = slug
        };
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result;
    }

    [RoutePattern(pattern: "/api/blogs/")]
    public async Task<GetBlogListByKeywordsResponse> GetBlogBriefListByKeywordsAsync(IEventBus eventBus,
        CancellationToken cancellationToken, [FromQuery] string? keywords = null, [FromQuery] int pageSize = 10,
        [FromQuery] int page = 1)
    {
        var queryEvent = new SearchBlogsByKeywordsQuery()
        {
            Keywords = keywords,
            PageSize = pageSize,
            Page = page
        };
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return new GetBlogListByKeywordsResponse(true, queryEvent.Result.Result,
            queryEvent.Result.Total, queryEvent.Result.TotalPages);
    }

    [RoutePattern(pattern: "/api/albums/{slug}/blogs")]
    public async Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(IEventBus eventBus,
        CancellationToken cancellationToken, [FromRoute] string slug, [FromQuery] int pageSize = 10,
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

    [RoutePattern(pattern: "/api/categories/{slug}/blogs")]
    public async Task<GetBlogListByCategorySlugResponse> GetBlogBriefListByCategorySlugAsync(IEventBus eventBus,
        CancellationToken cancellationToken, [FromRoute] string slug, [FromQuery] int pageSize = 10,
        [FromQuery] int page = 1)
    {
        var queryEvent = new SearchBlogsByCategoryQuery()
        {
            CategorySlug = slug,
            PageSize = pageSize,
            Page = page
        };
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return new GetBlogListByCategorySlugResponse(true, queryEvent.CategoryName, queryEvent.Result.Result,
            queryEvent.Result.Total, queryEvent.Result.TotalPages);
    }
}