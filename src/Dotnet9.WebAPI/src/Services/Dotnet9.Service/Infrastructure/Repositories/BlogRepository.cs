namespace Dotnet9.Service.Infrastructure.Repositories;

public class BlogRepository : Repository<Dotnet9DbContext, Blog, Guid>, IBlogRepository
{
    public BlogRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public Task<Blog?> FindByIdAsync(Guid id)
    {
        return Context.Blogs.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Blog?> FindByTitleAsync(string title)
    {
        return Context.Blogs.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Title == title);
    }

    public Task<Blog?> FindBySlugAsync(string slug)
    {
        return Context.Blogs.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public Task<List<BlogBrief>> GetBlogBriefListAsync()
    {
        return Context.Blogs.Where(blog => blog.Banner).Take(10)
            .Select(blog => new BlogBrief(blog.Title, blog.Slug, blog.Description, blog.Cover, blog.CreationTime))
            .ToListAsync();
    }


    public async Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(SearchBlogsByAlbumQuery request)
    {
        var page = request.Page;
        var pageSize = request.PageSize;
        var album = await Context.Albums.FirstOrDefaultAsync(x => x.Slug == request.AlbumSlug);
        if (album == null)
        {
            return new GetBlogListByAlbumSlugResponse(false);
        }

        var query = Context.Blogs.AsQueryable();
        var dataListFromDb = query.OrderBy(x => x.CreationTime)
            .Include(x => x.Albums)
            .Where(x => x.Albums != null && x.Albums.Any(y => y.AlbumId == album.Id));
        var total = await dataListFromDb.CountAsync();
        var dataList = await dataListFromDb.Skip((page - 1) * pageSize)
            .Take(pageSize).Select(x => new BlogBrief(x.Title, x.Slug, x.Description, x.Cover, x.CreationTime))
            .ToListAsync();

        return new GetBlogListByAlbumSlugResponse(true, album.Name, dataList, total, (total + pageSize - 1) / pageSize,
            request.PageSize, request.Page);
    }
}