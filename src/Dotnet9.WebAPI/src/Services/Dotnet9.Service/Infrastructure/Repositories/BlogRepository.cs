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
}
