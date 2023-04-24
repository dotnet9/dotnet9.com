namespace Dotnet9.Service.Infrastructure.Repositories;

public class BlogRepository : Repository<Dotnet9DbContext, Blog, Guid>, IBlogRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public BlogRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
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

    public async Task<BlogDetails?> FindBySlugAsync(string slug)
    {
        async Task<BlogDetails?> ReadDataFromDb()
        {
            var blog = await Context.Blogs
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags)
                .FirstOrDefaultAsync(x => x.Slug == slug);
            return blog == null ? null : ToBlogDetails(blog);
        }

        TimeSpan? timeSpan = null;
        var key = $"{nameof(BlogRepository)}_{nameof(FindBySlugAsync)}_{slug}";

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

    public async Task<List<BlogBrief>?> GetBlogBriefListAsync()
    {
        async Task<List<BlogBrief>> ReadDataFromDb()
        {
            var query = Context.Query<Blog>();
            var dataFromDb = query
                .Include(x => x.Categories)
                .Include(x => x.Albums)
                .Include(x => x.Tags)
                .Where(blog => blog.Banner && !blog.Draft);

            var dataList = dataFromDb.Take(10)
                .ToList()
                .Select(ToBlogBrief).ToList();
            return dataList;
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb.Any())
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<List<BlogBrief>>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<List<BlogBrief>>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }

    public async Task<GetBlogListByKeywordsResponse> GetBlogBriefListByKeywordsAsync(
        SearchBlogsByKeywordsQuery request)
    {
        async Task<GetBlogListByKeywordsResponse?> ReadDataFromDb()
        {
            var keywords = request.Keywords;
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
            var dataListFromDb = query.OrderBy(x => x.CreationTime)
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
            var dataListFromDb = query.OrderBy(x => x.CreationTime)
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

    private List<CategoryBrief>? GetCategoryBriefs(Blog blog)
    {
        if (blog.Categories?.Any() != true)
        {
            return null;
        }

        return (from blogCategory in blog.Categories
                join category in Context.Categories on blogCategory.CategoryId equals category.Id
                select new CategoryBrief(category.Name, category.Slug, category.Cover, category.Description, 0))
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
            blog.CopyrightType.ToString(), blog.Original, blog.OriginalTitle, blog.OriginalLink, blog.Banner,
            GetCategoryBriefs(blog),
            GetAlbumBriefs(blog),
            GetTagBriefs(blog),
            blog.ViewCount,
            blog.CreationTime);
    }

    private BlogDetails ToBlogDetails(Blog blog)
    {
        return new BlogDetails(blog.Id, blog.Title, blog.Slug, blog.Description, blog.Cover, blog.Content,
            blog.CopyrightType.ToString(), blog.Original, blog.OriginalTitle, blog.OriginalLink, blog.Banner,
            GetCategoryBriefs(blog),
            GetAlbumBriefs(blog),
            GetTagBriefs(blog),
            blog.ViewCount,
            blog.CreationTime);
    }
}