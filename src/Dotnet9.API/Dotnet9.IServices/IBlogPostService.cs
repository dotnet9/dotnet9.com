using Dotnet9.IServices.Base;
using Dotnet9.Models.Models;

namespace Dotnet9.IServices;

public interface IBlogPostService : IBaseService<BlogPost>
{
    Task<BlogPost> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BlogPost> GetBlogPostDetailsAsync(int id, CancellationToken cancellationToken = default);

    Task AddUserInfoBlogPostAsync(int id, int userId, CancellationToken cancellationToken = default);

    Task AddBlogPostComment(int id, int userId, string content, CancellationToken cancellationToken = default);
}