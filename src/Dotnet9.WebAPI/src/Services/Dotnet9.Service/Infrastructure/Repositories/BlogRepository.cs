namespace Dotnet9.Service.Infrastructure.Repositories;

public class BlogRepository : Repository<Dotnet9DbContext, Blog, Guid>, IBlogRepository
{
    public BlogRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
    public async Task<Blog?> FindByIdAsync(Guid id)
    {
        return await Context.Blogs!.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Blog?> FindByTitleAsync(string title)
    {
        return await Context.Blogs!.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Title == title);
    }

    public async Task<Blog?> FindBySlugAsync(string slug)
    {
        return await Context.Blogs!.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Slug == slug);
    }
}
