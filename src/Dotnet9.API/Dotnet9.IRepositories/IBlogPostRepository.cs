using Dotnet9.IRepositories.Base;
using Dotnet9.Models.Models;

namespace Dotnet9.IRepositories;

public interface IBlogPostRepository : IBaseRepository<BlogPost>
{
    Task<BlogPost?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BlogPost?> GetUserInfoBlogPostByIdAsync(int id, CancellationToken cancellationToken = default);
}