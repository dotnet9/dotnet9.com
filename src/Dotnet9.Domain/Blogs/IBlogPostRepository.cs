using System.Linq.Expressions;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Blogs;

public interface IBlogPostRepository : IRepository<BlogPost>
{
    Task<BlogPostWithDetails?> GetAsync(Expression<Func<BlogPost, bool>> whereLambda);

    Task<List<BlogPostWithDetails>?> SelectAsync(Expression<Func<BlogPost, bool>> whereLambda);
}