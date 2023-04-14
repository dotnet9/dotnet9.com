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
}
