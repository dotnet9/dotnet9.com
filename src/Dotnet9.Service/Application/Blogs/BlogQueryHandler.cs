namespace Dotnet9.Service.Application.Blogs;

public class BlogQueryHandler
{
    private readonly IBlogRepository _repository;

    public BlogQueryHandler(IBlogRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListOfRecommendAsync(GetBlogsOfRecommendQuery query, CancellationToken cancellationToken)
    {
        var dataList = await _repository.GetBlogBriefListOfRecommendAsync();

        query.Result = new PaginatedListBase<BlogBrief>()
        {
            Result = dataList!
        };
    }

    [EventHandler]
    public async Task GetListOfHistoryHotAsync(GetBlogsOfHistoryHotQuery query, CancellationToken cancellationToken)
    {
        var dataList = await _repository.GetBlogBriefListOfHistoryHotAsync();

        query.Result = new PaginatedListBase<BlogBrief>()
        {
            Result = dataList!
        };
    }


    [EventHandler]
    public async Task GetListArchiveAsync(BlogArchivesQuery query, CancellationToken cancellationToken)
    {
        var dataList = await _repository.GetBlogArchiveListAsync();

        query.Result = new PaginatedListBase<BlogArchive>()
        {
            Result = dataList!
        };
    }

    [EventHandler]
    public async Task GetListByKeywordsAsync(SearchBlogsByKeywordsQuery query, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetBlogBriefListByKeywordsAsync(query);
        query.Result = new PaginatedListBase<BlogBrief>()
        {
            Total = blog.Total, TotalPages = blog.TotalPage, Result = blog.Records!
        };
    }

    [EventHandler]
    public async Task GetListByAlbumAsync(SearchBlogsByAlbumQuery query, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetBlogBriefListByAlbumSlugAsync(query);
        query.AlbumName = blog.AlbumName;
        query.Result = new PaginatedListBase<BlogBrief>()
        {
            Total = blog.Total, TotalPages = blog.TotalPage, Result = blog.Records!
        };
    }

    [EventHandler]
    public async Task GetListByCategoryAsync(SearchBlogsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetBlogBriefListByCategorySlugAsync(query);
        query.CategoryName = blog.CategoryName;
        query.Result = new PaginatedListBase<BlogBrief>()
        {
            Total = blog.Total,
            TotalPages = blog.TotalPage,
            Result = blog.Records!
        };
    }

    [EventHandler]
    public async Task GetListByTagAsync(SearchBlogsByTagQuery query, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetBlogBriefListByTagNameAsync(query);
        query.Result = new PaginatedListBase<BlogBrief>()
        {
            Total = blog.Total,
            TotalPages = blog.TotalPage,
            Result = blog.Records!
        };
    }

    [EventHandler]
    public async Task GetItemDetailsBySlugAsync(SearchBlogDetailsBySlugQuery query, CancellationToken cancellationToken)
    {
        var blog = await _repository.FindDetailsBySlugAsync(query.Slug);
        if (blog != null)
        {
            query.Result = blog;
        }
    }
}