using Dotnet9.WebAPI.Domain.BlogPosts;

namespace Dotnet9.Web.Service.BlogPosts;

internal class BlogPostService : IBlogPostService
{
    private readonly Dotnet9DbContext _dbContext;

    public BlogPostService(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BlogPostBriefForFront>?> BlogPostBriefListByBanner(int count)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        List<BlogPostBriefForFront> datasFromDb = await
            _dbContext.BlogPosts!.Where(x => x.Banner).Take(count).Select(x => new BlogPostBriefForFront(
                x.Title,
                x.Slug,
                x.Cover,
                x.Description,
                x.Original,
                (from blogPostCategory in x.Categories
                    join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                    select new CategoryBrief(category.Slug, category.Name,
                        category.Description, 0, null)).ToList(),
                x.CreationTime,
                x.ViewCount)).ToListAsync();
        return datasFromDb;
    }

    public async Task<List<BlogPostBriefForFront>?> TopNewBlogPostBriefListAsync(int count)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        List<BlogPostBriefForFront> datasFromDb = await
            _dbContext.BlogPosts!.OrderByDescending(x => x.CreationTime).Take(count)
                .Select(x => new BlogPostBriefForFront(
                    x.Title,
                    x.Slug,
                    x.Cover,
                    x.Description,
                    x.Original,
                    (from blogPostCategory in x.Categories
                        join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                        select new CategoryBrief(category.Slug, category.Name,
                            category.Description, 0, null)).ToList(),
                    x.CreationTime,
                    x.ViewCount))
                .ToListAsync();
        return datasFromDb;
    }


    public async Task<List<BlogPostBriefForFront>?> TopLikeBlogPostBriefListAsync(int count)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        List<BlogPostBriefForFront> datasFromDb = await
            _dbContext.BlogPosts!.OrderByDescending(x => x.ViewCount).Take(count)
                .Select(x => new BlogPostBriefForFront(
                    x.Title,
                    x.Slug,
                    x.Cover,
                    x.Description,
                    x.Original,
                    (from blogPostCategory in x.Categories
                        join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                        select new CategoryBrief(category.Slug, category.Name,
                            category.Description, 0, null)).ToList(),
                    x.CreationTime,
                    x.ViewCount))
                .ToListAsync();
        return datasFromDb;
    }

    public async Task<GetBlogPostBriefListResponse> BlogPostBriefListAsync(GetBlogPostBriefListRequest request)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        if (!request.Keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Title, $"%{request.Keywords}%")
                || EF.Functions.Like(log.Slug, $"%{request.Keywords}%")
                || (log.Original != null && EF.Functions.Like(log.Original!, $"%{request.Keywords}%"))
                || (log.OriginalTitle != null && EF.Functions.Like(log.OriginalTitle!, $"%{request.Keywords}%"))
                || EF.Functions.Like(log.Description!, $"%{request.Keywords}%")
                || EF.Functions.Like(log.Content!, $"%{request.Keywords}%"));
        }

        int total = await query.CountAsync();
        IIncludableQueryable<BlogPost, List<BlogPostTag>?> datasFromDb =
            query.OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.CreationTime)
                .Skip((request.Current - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags);
        List<BlogPostBriefForFront> data = await datasFromDb.Select(x => new BlogPostBriefForFront(
            x.Title,
            x.Slug,
            x.Cover,
            x.Description,
            x.Original,
            (from blogPostCategory in x.Categories
                join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                select new CategoryBrief(category.Slug, category.Name,
                    category.Description, 0, null)).ToList(),
            x.CreationTime,
            x.ViewCount)).ToListAsync();
        return new GetBlogPostBriefListResponse(data, total, true, request.PageSize, request.Current);
    }

    public async Task<GetBlogPostBriefListByCategorySlugResponse> BlogPostBriefListByCategorySlugAsync(
        BlogPostBriefListByCategorySlugRequest request)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        Category? category = await _dbContext.Categories!.FirstOrDefaultAsync(x => x.Slug == request.Slug);
        if (category == null)
        {
            return new GetBlogPostBriefListByCategorySlugResponse(null, null, 0, false, request.PageSize,
                request.Current);
        }

        IQueryable<BlogPost> datasFromDb =
            query.OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.CreationTime)
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags)
                .Where(x => x.Categories != null && x.Categories.Any(y => y.CategoryId == category.Id) == true);

        int total = await datasFromDb.CountAsync();

        List<BlogPostBriefForFront> data = await datasFromDb.Skip((request.Current - 1) * request.PageSize)
            .Take(request.PageSize).Select(x => new BlogPostBriefForFront(
                x.Title,
                x.Slug,
                x.Cover,
                x.Description,
                x.Original,
                (from blogPostCategory in x.Categories
                    join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                    select new CategoryBrief(category.Slug, category.Name,
                        category.Description, 0, null)).ToList(),
                x.CreationTime,
                x.ViewCount)).ToListAsync();
        return new GetBlogPostBriefListByCategorySlugResponse(category.Name, data, total, true, request.PageSize,
            request.Current);
    }


    public async Task<GetBlogPostBriefListByAlbumSlugResponse> BlogPostBriefListByAlbumSlugAsync(
        GetBlogPostBriefListByAlbumSlugRequest request)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        Album? album = await _dbContext.Albums!.FirstOrDefaultAsync(x => x.Slug == request.Slug);
        if (album == null)
        {
            return new GetBlogPostBriefListByAlbumSlugResponse(null, null, 0, false, request.PageSize,
                request.Current);
        }

        IQueryable<BlogPost> datasFromDb =
            query.OrderBy(x => x.CreationTime)
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags)
                .Where(x => x.Albums != null && x.Albums.Any(y => y.AlbumId == album.Id) == true);

        int total = await datasFromDb.CountAsync();

        List<BlogPostBriefForFront> data = await datasFromDb.Skip((request.Current - 1) * request.PageSize)
            .Take(request.PageSize).Select(x => new BlogPostBriefForFront(
                x.Title,
                x.Slug,
                x.Cover,
                x.Description,
                x.Original,
                (from blogPostCategory in x.Categories
                    join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                    select new CategoryBrief(category.Slug, category.Name,
                        category.Description, 0, null)).ToList(),
                x.CreationTime,
                x.ViewCount)).ToListAsync();
        return new GetBlogPostBriefListByAlbumSlugResponse(album.Name, data, total, true, request.PageSize,
            request.Current);
    }


    public async Task<GetBlogPostBriefListByTagNameResponse> BlogPostBriefListByTagNameAsync(
        GetBlogPostBriefListByTagNameRequest request)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        Tag? tag = await _dbContext.Tags!.FirstOrDefaultAsync(x => x.Name == request.Name);
        if (tag == null)
        {
            return new GetBlogPostBriefListByTagNameResponse(null, 0, false, request.PageSize,
                request.Current);
        }

        IQueryable<BlogPost> datasFromDb =
            query.OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.CreationTime)
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags)
                .Where(x => x.Tags != null && x.Tags.Any(y => y.TagId == tag.Id) == true);

        int total = await datasFromDb.CountAsync();

        List<BlogPostBriefForFront> data = await datasFromDb.Skip((request.Current - 1) * request.PageSize)
            .Take(request.PageSize).Select(x => new BlogPostBriefForFront(
                x.Title,
                x.Slug,
                x.Cover,
                x.Description,
                x.Original,
                (from blogPostCategory in x.Categories
                    join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                    select new CategoryBrief(category.Slug, category.Name,
                        category.Description, 0, null)).ToList(),
                x.CreationTime,
                x.ViewCount)).ToListAsync();
        return new GetBlogPostBriefListByTagNameResponse(data, total, true, request.PageSize,
            request.Current);
    }

    public async Task<BlogPostDetails?> BlogPostDetailsBySlugAsync(string slug)
    {
        BlogPost? blogPost = await _dbContext.BlogPosts!.Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Slug == slug);
        if (blogPost == null)
        {
            return null;
        }

        var albums = (from blogPostAlbum in blogPost.Albums
            join album in _dbContext.Albums! on blogPostAlbum.BlogPostId equals album.Id
            select new AlbumBrief(album.SequenceNumber, album.Slug, album.Name,
                album.Description)).ToList();
        var categories = (from blogPostCategory in blogPost.Categories
            join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
            select new CategoryBrief(category.Slug, category.Name,
                category.Description, 0, category.Id)).ToList();
        var tags = (from blogPostTag in blogPost.Tags
            join tag in _dbContext.Tags! on blogPostTag.TagId equals tag.Id
            select tag.Name).ToList();
        var preview = await _dbContext.BlogPosts!.AsNoTracking().OrderBy(x => x.CreationTime)
            .Where(x => x.CreationTime < blogPost.CreationTime)
            .Select(x => new BlogPostNear(x.Title, x.Slug, x.Cover, x.Description, x.CreationTime))
            .FirstOrDefaultAsync();
        var next = await _dbContext.BlogPosts!.AsNoTracking().OrderBy(x => x.CreationTime)
            .Where(x => x.CreationTime > blogPost.CreationTime)
            .Select(x => new BlogPostNear(x.Title, x.Slug, x.Cover, x.Description, x.CreationTime))
            .FirstOrDefaultAsync();
        var near = await (from post in _dbContext.BlogPosts!
            join blogPostCategory in _dbContext.Set<BlogPostCategory>() on post.Id equals blogPostCategory
                .BlogPostId
            where blogPostCategory.BlogPostId != blogPost.Id &&
                  categories.Select(x => x.Id).Contains(blogPostCategory.CategoryId)
            select new BlogPostNear(post.Title, post.Slug, post.Cover, post.Description,
                post.CreationTime)).ToListAsync();

        return new BlogPostDetails(
            blogPost.Title,
            blogPost.Slug,
            blogPost.Description,
            blogPost.Content,
            blogPost.CopyrightType,
            blogPost.Original,
            blogPost.OriginalTitle,
            blogPost.OriginalLink,
            albums,
            categories,
            tags,
            blogPost.CreationTime,
            blogPost.ViewCount,
            preview,
            next,
            near);
    }

    public async Task<List<BlogPostArchiveItem>?> ArchivesAsync()
    {
        return await _dbContext.BlogPosts!.Select(x => new BlogPostArchiveItem(x.Title, x.Slug, x.CreationTime))
            .ToListAsync();
    }
}