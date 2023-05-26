namespace Dotnet9.Service.Infrastructure.Repositories;

public class BlogRepository : Repository<Dotnet9DbContext, Blog, Guid>, IBlogRepository
{
    public BlogRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
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
        var blog = await Context.Blogs.AsNoTracking()
            .Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories)
            .Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Slug == slug);
        return blog == null ? null : await ToBlogDetails(blog);
    }

    public async Task<List<BlogBrief>?> GetBlogBriefListOfRecommendAsync()
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


    public async Task<List<BlogBrief>?> GetBlogBriefListOfWeekHotAsync()
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


    public async Task<List<BlogBrief>?> GetBlogBriefListOfHistoryHotAsync()
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

    public async Task<List<BlogArchive>?> GetBlogArchiveListAsync()
    {
        return await Context.Query<Blog>().Select(blog => new BlogArchive(blog.Title, blog.Slug, blog.CreationTime))
            .ToListAsync();
    }

    public async Task<GetBlogListByKeywordsResponse> GetBlogBriefListByKeywordsAsync(
        SearchBlogsByKeywordsQuery request)
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

    public async Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(SearchBlogsByAlbumQuery request)
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

        return default;
    }

    public async Task<GetBlogListByCategorySlugResponse> GetBlogBriefListByCategorySlugAsync(
        SearchBlogsByCategoryQuery request)
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


    public async Task<GetBlogListByTagNameResponse> GetBlogBriefListByTagNameAsync(SearchBlogsByTagQuery request)
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

    public async Task<List<BlogSearchCountDto>?> GetTopSearchKeywordsAsync()
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