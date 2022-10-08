namespace Dotnet9.Web.Service.BlogPosts;

internal class BlogPostService : IBlogPostService
{
    private readonly Dotnet9DbContext _dbContext;

    public BlogPostService(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBlogPostBriefListResponse> GetBlogPostBriefListAsync(GetBlogPostBriefListRequest request)
    {
        var query = _dbContext.BlogPosts!.AsQueryable();
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

        var total = await query.CountAsync();
        var datasFromDb =
            query.OrderByDescending(x => x.CreationTime)
                .Skip((request.Current - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags);
        var data = await datasFromDb.Select(x => new BlogPostBrief(
            x.Title,
            x.Slug,
            x.Description,
            x.Original,
            (from blogPostCategory in x.Categories
                join category in _dbContext.Categories on blogPostCategory.CategoryId equals category.Id
                select new CategoryBrief(category.Slug, category.Name, category.Description, 0)).ToList(),
            x.CreationTime,
            x.ViewCount)).ToListAsync();
        return new GetBlogPostBriefListResponse(data, total, true, request.PageSize, request.Current);
    }

    public async Task<GetBlogPostBriefListByCategorySlugResponse> GetBlogPostBriefListByCategorySlugAsync(
        GetBlogPostBriefListByCategorySlugRequest request)
    {
        var query = _dbContext.BlogPosts!.AsQueryable();
        var category = await _dbContext.Categories!.FirstOrDefaultAsync(x => x.Slug == request.Slug);
        if (category == null)
        {
            return new GetBlogPostBriefListByCategorySlugResponse(null, null, 0, false, request.PageSize,
                request.Current);
        }

        var datasFromDb =
            query.OrderByDescending(x => x.CreationTime)
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags)
                .Where(x => x.Categories != null && x.Categories.Any(y => y.CategoryId == category.Id) == true);

        var total = await datasFromDb.CountAsync();
        datasFromDb.Skip((request.Current - 1) * request.PageSize).Take(request.PageSize);
        var data = await datasFromDb.Select(x => new BlogPostBrief(
            x.Title,
            x.Slug,
            x.Description,
            x.Original,
            (from blogPostCategory in x.Categories
                join category in _dbContext.Categories on blogPostCategory.CategoryId equals category.Id
                select new CategoryBrief(category.Slug, category.Name, category.Description, 0)).ToList(),
            x.CreationTime,
            x.ViewCount)).ToListAsync();
        return new GetBlogPostBriefListByCategorySlugResponse(category.Name, data, total, true, request.PageSize,
            request.Current);
    }
}