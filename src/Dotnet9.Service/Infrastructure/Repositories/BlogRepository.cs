namespace Dotnet9.Service.Infrastructure.Repositories;

public class BlogRepository : Repository<Dotnet9DbContext, Blog, Guid>, IBlogRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public BlogRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public async Task CreateBlogViewCount(string slug, string ip, DateTime creationTime)
    {
        await Context.BlogsViewCounts.AddAsync(new BlogViewCount(slug, ip, creationTime));
        await Context.SaveChangesAsync();
    }

    public async Task CreateBlogSearchCount(string keywords, string ip, DateTime creationTime)
    {
        await Context.BlogsSearchCounts.AddAsync(new BlogSearchCount(keywords, ip, creationTime));
        await Context.SaveChangesAsync();
    }

    public Task<Blog?> FindByIdAsync(Guid id)
    {
        return Context.Blogs
            .Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories)
            .Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Blog?> FindByTitleAsync(string title)
    {
        return Context.Blogs
            .Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories)
            .Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Title == title);
    }


    public Task<Blog?> FindBySlugAsync(string slug)
    {
        return Context.Blogs.AsTracking().FirstOrDefaultAsync(blog => blog.Slug == slug);
    }

    public async Task<BlogDetails?> FindDetailsBySlugAsync(string slug)
    {
        async Task<BlogDetails?> ReadDataFromDb()
        {
            var blog = await Context.Blogs.AsNoTracking()
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags)
                .FirstOrDefaultAsync(x => x.Slug == slug);
            return blog == null ? null : await ToBlogDetails(blog);
        }

        TimeSpan? timeSpan = null;
        var key = $"{nameof(BlogRepository)}_{nameof(FindDetailsBySlugAsync)}_{slug}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

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

        return data;
    }

    public async Task<List<BlogBrief>?> GetBlogBriefListOfRecommendAsync()
    {
        async Task<List<BlogBrief>?> ReadDataFromDbAsync()
        {
            var query = Context.Query<Blog>();
            var dataFromDb = query
                .Include(x => x.Categories)
                .Include(x => x.Albums)
                .Include(x => x.Tags)
                .Where(blog => blog.Banner && !blog.Draft)
                .OrderByDescending(blog => blog.ViewCount);

            var dataList = dataFromDb.Take(30).AsEnumerable().OrderBy(_ => Guid.NewGuid())
                .ToList().Take(6)
                .Select(ToBlogBrief).ToList();
            return await Task.FromResult(dataList);
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListOfRecommendAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        {
            var dataFromDb = ReadDataFromDbAsync().Result;

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

        return data;
    }


    public async Task<List<BlogBrief>?> GetBlogBriefListOfWeekHotAsync()
    {
        async Task<List<BlogBrief>?> ReadDataFromDbAsync()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);

            var weekReadCount = await Context.Set<BlogViewCount>()
                .Where(count => count.CreationTime >= startOfWeek && count.CreationTime <= endOfWeek)
                .GroupBy(count => count.Slug)
                .Select(group => new { Slug = group.Key, Count = group.Count() })
                .OrderByDescending(group => group.Count)
                .Take(6).ToListAsync();

            var dataList = weekReadCount.Join(Context.Blogs,
                    count => count.Slug,
                    blog => blog.Slug,
                    (count, blog) =>
                        new BlogBrief(blog.Id, blog.Title, blog.Slug, default, default, default, default, default,
                            default,
                            default, default, default, default, count.Count, blog.CreationTime))
                .ToList();

            return dataList;
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListOfWeekHotAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        {
            var dataFromDb = ReadDataFromDbAsync().Result;

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

        return data;
    }


    public async Task<List<BlogBrief>?> GetBlogBriefListOfHistoryHotAsync()
    {
        List<BlogBrief> ReadDataFromDb()
        {
            var dataFromDb = Context.Set<Blog>()
                .Where(blog => !blog.Draft)
                .OrderByDescending(blog => blog.ViewCount)
                .Take(6).Select(blog => new BlogBrief(blog.Id, blog.Title, blog.Slug, blog.Description, blog.Cover,
                    (int)blog.CopyrightType, blog.Original, blog.OriginalTitle, blog.OriginalLink, blog.Banner, default,
                    default, default, blog.ViewCount, blog.CreationTime)).ToList();
            ;

            return dataFromDb;
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListOfHistoryHotAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        {
            var dataFromDb = ReadDataFromDb();

            if (dataFromDb.Any())
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

        return await Task.FromResult(data);
    }

    public async Task<List<BlogArchive>?> GetBlogArchiveListAsync()
    {
        async Task<List<BlogArchive>> ReadDataFromDb()
        {
            return await Context.Query<Blog>().Select(blog => new BlogArchive(blog.Title, blog.Slug, blog.CreationTime))
                .ToListAsync();
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogRepository)}_{nameof(GetBlogArchiveListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb.Any())
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

        return data;
    }

    public async Task<GetBlogListByKeywordsResponse> GetBlogBriefListByKeywordsAsync(
        SearchBlogsByKeywordsQuery request)
    {
        async Task<GetBlogListByKeywordsResponse?> ReadDataFromDb()
        {
            var keywords = request.Keywords?.ToLower();
            var page = request.Page;
            var pageSize = request.PageSize;

            var query = Context.Blogs.AsQueryable();
            var isKeywordsEmpty = request.Keywords.IsNullOrWhiteSpace();
            var dataListFromDb = query.OrderByDescending(x => x.CreationTime)
                .Include(x => x.Categories)
                .Include(x => x.Albums)
                .Include(x => x.Tags)
                .Where(blog => !blog.Draft && (isKeywordsEmpty ||
                                               (EF.Functions.Like(blog.Title.ToLower(), $"%{keywords}%")
                                                || EF.Functions.Like(blog.Description.ToLower(), $"%{keywords}%"))));

            var total = await dataListFromDb.CountAsync();
            var dataList = dataListFromDb.Skip((page - 1) * pageSize)
                .Take(pageSize).ToList().Select(ToBlogBrief)
                .ToList();

            if (dataList.Any())
            {
                return new GetBlogListByKeywordsResponse(true, dataList, total,
                    (total + pageSize - 1) / pageSize,
                    request.PageSize, request.Page);
            }

            return null;
        }

        TimeSpan? timeSpan = null;
        string key =
            $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListByKeywordsAsync)}_{request.Keywords}_{request.Page}_{request.PageSize}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

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

        return data ?? new GetBlogListByKeywordsResponse(false);
    }

    public async Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(SearchBlogsByAlbumQuery request)
    {
        async Task<GetBlogListByAlbumSlugResponse?> ReadDataFromDb()
        {
            var page = request.Page;
            var pageSize = request.PageSize;
            var album = await Context.Albums.FirstOrDefaultAsync(x => x.Slug == request.AlbumSlug);
            if (album == null)
            {
                return null;
            }

            var query = Context.Blogs.AsQueryable();
            var dataListFromDb = query.OrderBy(x => x.CreationTime)
                .Include(x => x.Categories)
                .Include(x => x.Albums)
                .Include(x => x.Tags)
                .Where(x => !x.Draft && x.Albums != null && x.Albums.Any(y => y.AlbumId == album.Id));
            var total = await dataListFromDb.CountAsync();
            var dataList = dataListFromDb.Skip((page - 1) * pageSize)
                .Take(pageSize).ToList().Select(ToBlogBrief)
                .ToList();

            if (dataList.Any() == true)
            {
                return new GetBlogListByAlbumSlugResponse(true, album.Name, dataList, total,
                    (total + pageSize - 1) / pageSize,
                    request.PageSize, request.Page);
            }

            return null;
        }

        TimeSpan? timeSpan = null;
        var key =
            $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListByAlbumSlugAsync)}_{request.AlbumSlug}_{request.Page}_{request.PageSize}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

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

        return data ?? new GetBlogListByAlbumSlugResponse(false);
    }

    public async Task<GetBlogListByCategorySlugResponse> GetBlogBriefListByCategorySlugAsync(
        SearchBlogsByCategoryQuery request)
    {
        async Task<GetBlogListByCategorySlugResponse?> ReadDataFromDb()
        {
            var page = request.Page;
            var pageSize = request.PageSize;
            var category = await Context.Categories.FirstOrDefaultAsync(x => x.Slug == request.CategorySlug);
            if (category == null)
            {
                return null;
            }

            var query = Context.Blogs.AsQueryable();
            var dataListFromDb = query.OrderByDescending(x => x.CreationTime)
                .Include(x => x.Categories)
                .Include(x => x.Albums)
                .Include(x => x.Tags)
                .Where(x => !x.Draft && x.Categories != null && x.Categories.Any(y => y.CategoryId == category.Id));
            var total = await dataListFromDb.CountAsync();
            var dataList = dataListFromDb.Skip((page - 1) * pageSize)
                .Take(pageSize).ToList().Select(ToBlogBrief)
                .ToList();


            if (dataList.Any())
            {
                return new GetBlogListByCategorySlugResponse(true, category.Name, dataList, total,
                    (total + pageSize - 1) / pageSize,
                    request.PageSize, request.Page);
            }

            return null;
        }

        TimeSpan? timeSpan = null;
        var key =
            $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListByCategorySlugAsync)}_{request.CategorySlug}_{request.Page}_{request.PageSize}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

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

        return data ?? new GetBlogListByCategorySlugResponse(false);
    }


    public async Task<GetBlogListByTagNameResponse> GetBlogBriefListByTagNameAsync(SearchBlogsByTagQuery request)
    {
        async Task<GetBlogListByTagNameResponse?> ReadDataFromDb()
        {
            var page = request.Page;
            var pageSize = request.PageSize;
            var tag = await Context.Tags.FirstOrDefaultAsync(x => x.Name == request.TagName);
            if (tag == null)
            {
                return null;
            }

            var query = Context.Blogs.AsQueryable();
            var dataListFromDb = query.OrderByDescending(x => x.CreationTime)
                .Include(x => x.Categories)
                .Include(x => x.Albums)
                .Include(x => x.Tags)
                .Where(x => !x.Draft && x.Tags != null && x.Tags.Any(y => y.TagId == tag.Id));
            var total = await dataListFromDb.CountAsync();
            var dataList = dataListFromDb.Skip((page - 1) * pageSize)
                .Take(pageSize).ToList().Select(ToBlogBrief)
                .ToList();


            if (dataList.Any())
            {
                return new GetBlogListByTagNameResponse(true, dataList, total,
                    (total + pageSize - 1) / pageSize,
                    request.PageSize, request.Page);
            }

            return null;
        }

        TimeSpan? timeSpan = null;
        var key =
            $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListByTagNameAsync)}_{request.TagName}_{request.Page}_{request.PageSize}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

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

        return data ?? new GetBlogListByTagNameResponse(false);
    }

    public async Task<List<BlogSearchCountDto>?> GetTopSearchKeywordsAsync()
    {
        async Task<List<BlogSearchCountDto>> ReadDataFromDbAsync()
        {
            var countFromDb = Context.Set<BlogSearchCount>().AsNoTracking()
                .GroupBy(count => count.Keywords)
                .Select(group => new BlogSearchCountDto(group.Key, group.Count()))
                .AsEnumerable()
                .OrderByDescending(count => count.Count)
                .Take(10)
                .ToList();

            return await Task.FromResult(countFromDb);
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogRepository)}_{nameof(GetTopSearchKeywordsAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        {
            var dataFromDb = ReadDataFromDbAsync().Result;

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

        return data;
    }

    private List<CategoryBrief>? GetCategoryBriefs(Blog blog)
    {
        if (blog.Categories?.Any() != true)
        {
            return null;
        }

        return (from blogCategory in blog.Categories
                join category in Context.Categories on blogCategory.CategoryId equals category.Id
                select new CategoryBrief(category.Name, category.Slug, category.Cover, category.Description, 0,
                    category.Id))
            .ToList();
    }

    private List<AlbumBrief>? GetAlbumBriefs(Blog blog)
    {
        if (blog.Albums?.Any() != true)
        {
            return null;
        }

        return (from blogAlbum in blog.Albums
            join album in Context.Albums! on blogAlbum.AlbumId equals album.Id
            select new AlbumBrief(album.Name, album.Slug, album.Cover, album.Description, 0)).ToList();
    }

    private List<TagBrief>? GetTagBriefs(Blog blog)
    {
        if (blog.Tags?.Any() != true)
        {
            return null;
        }

        return (from blogTag in blog.Tags
            join tag in Context.Tags! on blogTag.TagId equals tag.Id
            select new TagBrief(tag.Name, 0)).ToList();
    }

    private BlogBrief ToBlogBrief(Blog blog)
    {
        return new BlogBrief(blog.Id, blog.Title, blog.Slug, blog.Description, blog.Cover,
            (int)blog.CopyrightType, blog.Original, blog.OriginalTitle, blog.OriginalLink, blog.Banner,
            GetCategoryBriefs(blog),
            GetAlbumBriefs(blog),
            GetTagBriefs(blog),
            blog.ViewCount,
            blog.CreationTime);
    }

    private async Task<BlogDetails> ToBlogDetails(Blog blog)
    {
        var categories = (from blogPostCategory in blog.Categories
            join category in Context.Categories! on blogPostCategory.CategoryId equals category.Id
            select new CategoryBrief(category.Name, category.Slug, category.Cover,
                category.Description, 0, category.Id)).ToList();
        var preview = await Context.Blogs.AsNoTracking().OrderBy(x => x.CreationTime)
            .Where(x => x.CreationTime < blog.CreationTime)
            .Select(x => new BlogPostNear(x.Title, x.Slug, x.Cover, x.Description, x.CreationTime))
            .FirstOrDefaultAsync();
        var next = await Context.Blogs!.AsNoTracking().OrderBy(x => x.CreationTime)
            .Where(x => x.CreationTime > blog.CreationTime)
            .Select(x => new BlogPostNear(x.Title, x.Slug, x.Cover, x.Description, x.CreationTime))
            .FirstOrDefaultAsync();
        //var near = await (from post in Context.Blogs.AsNoTracking()
        //                  join blogPostCategory in Context.Set<BlogCategory>().AsNoTracking() on post.Id equals blogPostCategory
        //                      .BlogId
        //                  where blogPostCategory.BlogId != blog.Id &&
        //                        categories.Select(x => x.Id).Contains(blogPostCategory.CategoryId)
        //                  select new BlogPostNear(post.Title, post.Slug, post.Cover, post.Description,
        //                      post.CreationTime)).Take(5).ToListAsync();

        return new BlogDetails(blog.Id, blog.Title, blog.Slug, blog.Description, blog.Cover, blog.Content,
            blog.CopyrightType, blog.Original, blog.OriginalTitle, blog.OriginalLink, blog.Banner,
            GetCategoryBriefs(blog),
            GetAlbumBriefs(blog),
            GetTagBriefs(blog),
            blog.ViewCount,
            blog.LikeCount,
            preview,
            next,
            default,
            blog.CreationTime);
    }
}