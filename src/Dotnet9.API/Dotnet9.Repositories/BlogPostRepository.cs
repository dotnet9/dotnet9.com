using Dotnet9.EntityFramework;
using Dotnet9.IRepositories;
using Dotnet9.Models.Models;
using Dotnet9.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.Repositories;

public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
{
    public BlogPostRepository(Dotnet9Context context) : base(context)
    {
    }

    public async Task<BlogPost?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await DbContext().BlogPosts!.Where(x => x.Id == id)
            .Include(x => x.BlogPostComments)
            .ThenInclude(x => x.CreateUser)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<BlogPost?> GetUserInfoBlogPostByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await DbContext().BlogPosts!.Where(x => x.Id == id)
            .Include(x => x.UserInfoBlogPosts)
            .SingleOrDefaultAsync(cancellationToken);
    }
}