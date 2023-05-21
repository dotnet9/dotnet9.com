namespace Dotnet9.Service.Services;

public class BlogService : ServiceBase
{
    private IEventBus EventBus => GetRequiredService<IEventBus>();

    public BlogService() : base("/api/blogs")
    {
    }

    [RoutePattern(pattern: "/api/blogs/recommend")]
    public async Task<List<BlogBrief>> GetRecommendAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new GetBlogsOfRecommendQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }

    [RoutePattern(pattern: "/api/blogs/historyhot")]
    public async Task<List<BlogBrief>> GetHistoryHotAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new GetBlogsOfHistoryHotQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }

    [RoutePattern(pattern: "/api/blogs/archives")]
    public async Task<List<BlogArchive>> GetArchivesAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new BlogArchivesQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }

    [RoutePattern(pattern: "/api/blogs/{slug}")]
    public async Task<BlogDetails> GetBlogDetailsBySlugAsync(CancellationToken cancellationToken,
        [FromRoute] string slug)
    {
        var queryEvent = new SearchBlogDetailsBySlugQuery
        {
            Slug = slug
        };
        await EventBus.PublishAsync(queryEvent, cancellationToken);

        var increaseViewCountCommand = new IncreaseBlogViewCountCommand(slug);
        await EventBus.PublishAsync(increaseViewCountCommand, cancellationToken);

        return queryEvent.Result;
    }

    [RoutePattern(pattern: "/api/blogs/search")]
    public async Task<GetBlogListByKeywordsResponse> GetBlogBriefListByKeywordsAsync(
        CancellationToken cancellationToken, [FromQuery] string? keywords = null, [FromQuery] int pageSize = 10,
        [FromQuery] int page = 1)
    {
        var queryEvent = new SearchBlogsByKeywordsQuery()
        {
            Keywords = keywords,
            PageSize = pageSize,
            Page = page
        };
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return new GetBlogListByKeywordsResponse(true, queryEvent.Result.Result,
            queryEvent.Result.Total, queryEvent.Result.TotalPages);
    }

    [RoutePattern(pattern: "/api/albums/{slug}/blogs")]
    public async Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(
        CancellationToken cancellationToken, [FromRoute] string slug, [FromQuery] int pageSize = 10,
        [FromQuery] int page = 1)
    {
        var queryEvent = new SearchBlogsByAlbumQuery()
        {
            AlbumSlug = slug,
            PageSize = pageSize,
            Page = page
        };
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return new GetBlogListByAlbumSlugResponse(true, queryEvent.AlbumName, queryEvent.Result.Result,
            queryEvent.Result.Total, queryEvent.Result.TotalPages);
    }

    [RoutePattern(pattern: "/api/categories/{slug}/blogs")]
    public async Task<GetBlogListByCategorySlugResponse> GetBlogBriefListByCategorySlugAsync(
        CancellationToken cancellationToken, [FromRoute] string slug, [FromQuery] int pageSize = 10,
        [FromQuery] int page = 1)
    {
        var queryEvent = new SearchBlogsByCategoryQuery()
        {
            CategorySlug = slug,
            PageSize = pageSize,
            Page = page
        };
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return new GetBlogListByCategorySlugResponse(true, queryEvent.CategoryName, queryEvent.Result.Result,
            queryEvent.Result.Total, queryEvent.Result.TotalPages);
    }

    [RoutePattern(pattern: "/api/tags/{name}/blogs")]
    public async Task<GetBlogListByTagNameResponse> GetBlogBriefListByTagNameAsync(CancellationToken cancellationToken,
        [FromRoute] string name, [FromQuery] int pageSize = 10,
        [FromQuery] int page = 1)
    {
        var queryEvent = new SearchBlogsByTagQuery()
        {
            TagName = WebUtility.UrlDecode(name),
            PageSize = pageSize,
            Page = page
        };
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return new GetBlogListByTagNameResponse(true, queryEvent.Result.Result,
            queryEvent.Result.Total, queryEvent.Result.TotalPages);
    }
}