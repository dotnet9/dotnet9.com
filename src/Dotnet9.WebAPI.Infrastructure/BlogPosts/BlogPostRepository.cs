namespace Dotnet9.WebAPI.Infrastructure.BlogPosts;

internal class BlogPostRepository : IBlogPostRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public BlogPostRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var logs = await _dbContext.BlogPosts!.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<BlogPost?> FindByIdAsync(Guid id)
    {
        return await _dbContext.BlogPosts!.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost?> FindByTitleAsync(string title)
    {
        return await _dbContext.BlogPosts!.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Title == title);
    }

    public async Task<BlogPost?> FindBySlugAsync(string slug)
    {
        return await _dbContext.BlogPosts!.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<(BlogPost[]? BlogPosts, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize)
    {
        var query = _dbContext.BlogPosts!.AsQueryable();
        if (!keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Title, $"%{keywords}%")
                || EF.Functions.Like(log.Slug, $"%{keywords}%")
                || (log.Original != null && EF.Functions.Like(log.Original!, $"%{keywords}%"))
                || (log.OriginalTitle != null && EF.Functions.Like(log.OriginalTitle!, $"%{keywords}%"))
                || EF.Functions.Like(log.Description!, $"%{keywords}%")
                || EF.Functions.Like(log.Content!, $"%{keywords}%"));
        }

        var datasFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags);
        return (await datasFromDb.ToArrayAsync(), await query.LongCountAsync());
    }


    public async Task<(BlogPost[]? BlogPosts, long Count)> GetListByCategoryIdAsync(Guid categoryId, int pageIndex,
        int pageSize)
    {
        var blogPostIds = await _dbContext.Set<BlogPostCategory>().Where(x => x.CategoryId == categoryId)
            .Select(x => x.BlogPostId).ToArrayAsync();
        var query = _dbContext.BlogPosts!.Where(blogPost =>
            blogPostIds.Contains(blogPost.Id));
        var datasFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags);
        return (await datasFromDb.ToArrayAsync(), await query.LongCountAsync());
    }


    public async Task<(BlogPost[]? BlogPosts, long Count)> GetListByAlbumIdAsync(Guid albumId, int pageIndex,
        int pageSize)
    {
        var blogPostIds = await _dbContext.Set<BlogPostAlbum>().Where(x => x.AlbumId == albumId)
            .Select(x => x.BlogPostId).ToArrayAsync();
        var query = _dbContext.BlogPosts!.Where(blogPost =>
            blogPostIds.Contains(blogPost.Id));
        var datasFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags);
        return (await datasFromDb.ToArrayAsync(), await query.LongCountAsync());
    }


    public async Task<(BlogPost[]? BlogPosts, long Count)> GetListByTagIdAsync(Guid tagId, int pageIndex, int pageSize)
    {
        var blogPostIds = await _dbContext.Set<BlogPostTag>().Where(x => x.TagId == tagId)
            .Select(x => x.BlogPostId).ToArrayAsync();
        var query = _dbContext.BlogPosts!.Where(blogPost =>
            blogPostIds.Contains(blogPost.Id));
        var datasFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags);
        return (await datasFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}