namespace Dotnet9.Service.Application.Blogs;

public class BlogQueryHandler
{
    private readonly IBlogRepository _repository;
    private readonly IDistributedCacheHelper _redisClient;

    public BlogQueryHandler(IBlogRepository repository,
        IDistributedCacheHelper redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetCountBriefAsync(CountBriefQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetCountBriefAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetCountBriefAsync());

        if (data != null)
        {
            query.Result = data;
        }
    }

    [EventHandler]
    public async Task GetTopSearchKeywordsAsync(TopSearchKeywordsQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetTopSearchKeywordsAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetTopSearchKeywordsAsync());

        if (data != null)
        {
            query.Result = data;
        }
    }

    [EventHandler]
    public async Task GetListOfRecommendAsync(GetBlogsOfRecommendQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetListOfRecommendAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetBlogBriefListOfRecommendAsync());

        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Result = data
            };
        }
        else
        {
            query.Result = new PaginatedListBase<BlogBrief>();
        }
    }

    [EventHandler]
    public async Task GetListOfWeekHotAsync(GetBlogsOfWeekHotQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetListOfWeekHotAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetBlogBriefListOfWeekHotAsync());

        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Result = data
            };
        }
        else
        {
            query.Result = new PaginatedListBase<BlogBrief>();
        }
    }

    [EventHandler]
    public async Task GetListOfHistoryHotAsync(GetBlogsOfHistoryHotQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetListOfHistoryHotAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetBlogBriefListOfHistoryHotAsync());
        
        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Result = data
            };
        }
        else
        {
            query.Result = new PaginatedListBase<BlogBrief>();
        }
    }


    [EventHandler]
    public async Task GetListArchiveAsync(BlogArchivesQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetListArchiveAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetBlogArchiveListAsync());

        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogArchive>()
            {
                Result = data
            };
        }
        else
        {
            query.Result = new PaginatedListBase<BlogArchive>();
        }
    }

    /// <summary>
    /// 查找博客文章的详细信息
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [EventHandler]
    public async Task GetItemDetailsBySlugAsync(SearchBlogDetailsBySlugQuery query, CancellationToken cancellationToken)
    {
        var key = query.Slug;
        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.FindDetailsBySlugAsync(query.Slug));

        if (data != null)
        {
            query.Result = data;
        }
    }

    [EventHandler]
    public async Task GetListByKeywordsAsync(SearchBlogsByKeywordsQuery query, CancellationToken cancellationToken)
    {
        string key =
            $"{nameof(BlogQueryHandler)}_{nameof(GetListByKeywordsAsync)}_{query.Keywords}_{query.Page}_{query.PageSize}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetBlogBriefListByKeywordsAsync(query));
        
        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Total = data.Total, TotalPages = data.TotalPage, Result = data.Records!
            };
        }
        else
        {
            query.Result = new PaginatedListBase<BlogBrief>();
        }
    }

    [EventHandler]
    public async Task GetListByAlbumAsync(SearchBlogsByAlbumQuery query, CancellationToken cancellationToken)
    {
        var key =
            $"{nameof(BlogQueryHandler)}_{nameof(GetListByAlbumAsync)}_{query.AlbumSlug}_{query.Page}_{query.PageSize}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetBlogBriefListByAlbumSlugAsync(query));

        if (data != null)
        {
            query.AlbumName = data.AlbumName;
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Total = data.Total, TotalPages = data.TotalPage, Result = data.Records!
            };
        }
        else
        {
            query.Result = new PaginatedListBase<BlogBrief>();
        }
    }

    [EventHandler]
    public async Task GetListByCategoryAsync(SearchBlogsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var key =
            $"{nameof(BlogQueryHandler)}_{nameof(GetListByCategoryAsync)}_{query.CategorySlug}_{query.Page}_{query.PageSize}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetBlogBriefListByCategorySlugAsync(query));
        
        if (data != null)
        {
            query.CategoryName = data.CategoryName;
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Total = data.Total,
                TotalPages = data.TotalPage,
                Result = data.Records!
            };
        }
        else
        {
            query.Result = new PaginatedListBase<BlogBrief>();
        }
    }

    [EventHandler]
    public async Task GetListByTagAsync(SearchBlogsByTagQuery query, CancellationToken cancellationToken)
    {
        var key =
            $"{nameof(BlogQueryHandler)}_{nameof(GetListByTagAsync)}_{query.TagName}_{query.Page}_{query.PageSize}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetBlogBriefListByTagNameAsync(query));
       
        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Total = data.Total,
                TotalPages = data.TotalPage,
                Result = data.Records!
            };
        }
        else
        {
            query.Result = new PaginatedListBase<BlogBrief>();
        }
    }
}