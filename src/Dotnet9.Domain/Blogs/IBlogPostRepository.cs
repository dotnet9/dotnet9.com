using System.Linq.Expressions;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Blogs;

public interface IBlogPostRepository : IRepository<BlogPost>
{
    Task<BlogPostWithDetails?> GetBlogPostAsync(Expression<Func<BlogPost, bool>> whereLambda);

    Task<List<BlogPostWithDetails>?> SelectBlogPostAsync<S>(Expression<Func<BlogPost, bool>> whereLambda,
        Expression<Func<BlogPost, S>> orderByLambda,
        SortDirectionKind sortDirection);

    Task<Tuple<List<BlogPostWithDetails>, int>> SelectBlogPostAsync<S>(int pageSize, int pageIndex,
        Expression<Func<BlogPost, bool>> whereLambda,
        Expression<Func<BlogPost, S>> orderByLambda, SortDirectionKind sortDirection);
}