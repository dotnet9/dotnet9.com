namespace Dotnet9.Service.Application.Blogs;

public class BlogHandler
{
    private readonly IBlogRepository _repository;

    public BlogHandler(IBlogRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(BlogsQuery query, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetBlogBriefListAsync();

        query.Result = new PaginatedListBase<BlogBrief>()
        {
            Result = categories
        };
    }

    [EventHandler]
    public async Task GetListByAlbumAsync(SearchBlogsByAlbumQuery query, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetBlogBriefListByAlbumSlugAsync(query);
        query.AlbumName = blog.AlbumName;
        query.Result = new PaginatedListBase<BlogBrief>()
        {
            Total = blog.Total, TotalPages = blog.TotalPage, Result = blog.Records
        };
    }
}