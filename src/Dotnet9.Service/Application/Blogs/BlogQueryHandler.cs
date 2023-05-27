namespace Dotnet9.Service.Application.Blogs;

public class BlogQueryHandler
{
    private readonly IBlogRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public BlogQueryHandler(IBlogRepository repository,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task GetTopSearchKeywordsAsync(TopSearchKeywordsQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetTopSearchKeywordsAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        {
            var dataFromDb = _repository.GetTopSearchKeywordsAsync().Result;

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return Task.FromResult(new CacheEntry<List<BlogSearchCountDto>>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return Task.FromResult(new CacheEntry<List<BlogSearchCountDto>>(null));
        }, options => options.AbsoluteExpirationRelativeToNow = timeSpan);

        if (data != null)
        {
            query.Result = data;
        }
    }

    [EventHandler]
    public async Task GetListOfRecommendAsync(GetBlogsOfRecommendQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetListOfRecommendAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        {
            var dataFromDb = _repository.GetBlogBriefListOfRecommendAsync().Result;

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return Task.FromResult(new CacheEntry<List<BlogBrief>?>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return Task.FromResult(new CacheEntry<List<BlogBrief>?>(null));
        }, options => options.AbsoluteExpirationRelativeToNow = timeSpan);

        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Result = data
            };
        }
    }

    [EventHandler]
    public async Task GetListOfWeekHotAsync(GetBlogsOfWeekHotQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetListOfWeekHotAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        {
            var dataFromDb = _repository.GetBlogBriefListOfWeekHotAsync().Result;

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return Task.FromResult(new CacheEntry<List<BlogBrief>>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return Task.FromResult(new CacheEntry<List<BlogBrief>>(null));
        }, options => options.AbsoluteExpirationRelativeToNow = timeSpan);


        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Result = data
            };
        }
    }

    [EventHandler]
    public async Task GetListOfHistoryHotAsync(GetBlogsOfHistoryHotQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetListOfHistoryHotAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        {
            var dataFromDb = _repository.GetBlogBriefListOfHistoryHotAsync().Result;

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return Task.FromResult(new CacheEntry<List<BlogBrief>>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return Task.FromResult(new CacheEntry<List<BlogBrief>>(null));
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogBrief>()
            {
                Result = data
            };
        }
    }


    [EventHandler]
    public async Task GetListArchiveAsync(BlogArchivesQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogQueryHandler)}_{nameof(GetListArchiveAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetBlogArchiveListAsync().Result;

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<List<BlogArchive>>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<List<BlogArchive>>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        if (data != null)
        {
            query.Result = new PaginatedListBase<BlogArchive>()
            {
                Result = data
            };
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
        TimeSpan? timeSpan = null;

        var data = await _multilevelCacheClient.GetOrSetAsync(query.Slug, async () =>
        {
            var dataFromDb = await _repository.FindDetailsBySlugAsync(query.Slug);

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<BlogDetails>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<BlogDetails>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        if (data != null)
        {
            query.Result = data;
        }
    }

    [EventHandler]
    public async Task GetListByKeywordsAsync(SearchBlogsByKeywordsQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        string key =
            $"{nameof(BlogQueryHandler)}_{nameof(GetListByKeywordsAsync)}_{query.Keywords}_{query.Page}_{query.PageSize}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetBlogBriefListByKeywordsAsync(query).Result;

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetBlogListByKeywordsResponse>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetBlogListByKeywordsResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

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
        TimeSpan? timeSpan = null;
        var key =
            $"{nameof(BlogQueryHandler)}_{nameof(GetListByAlbumAsync)}_{query.AlbumSlug}_{query.Page}_{query.PageSize}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetBlogBriefListByAlbumSlugAsync(query).Result;

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetBlogListByAlbumSlugResponse>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetBlogListByAlbumSlugResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

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
        TimeSpan? timeSpan = null;
        var key =
            $"{nameof(BlogQueryHandler)}_{nameof(GetListByCategoryAsync)}_{query.CategorySlug}_{query.Page}_{query.PageSize}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetBlogBriefListByCategorySlugAsync(query).Result;

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetBlogListByCategorySlugResponse>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetBlogListByCategorySlugResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

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
        TimeSpan? timeSpan = null;
        var key =
            $"{nameof(BlogQueryHandler)}_{nameof(GetListByTagAsync)}_{query.TagName}_{query.Page}_{query.PageSize}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetBlogBriefListByTagNameAsync(query).Result;

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetBlogListByTagNameResponse>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetBlogListByTagNameResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

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