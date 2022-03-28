using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Blogs;

public interface IBlogPostRepository : IRepository<BlogPost>
{
    Task<BlogPostWithDetails?> FindByTitleAsync(string title);

    Task<BlogPostWithDetails?> FindBySlugAsync(string slug);
}