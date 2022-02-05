using Dotnet9.IRepositories;
using Dotnet9.IServices;
using Dotnet9.Models.Models;
using Dotnet9.Services.Base;

namespace Dotnet9.Services;

public class BlogPostService : BaseService<BlogPost>, IBlogPostService
{
    public BlogPostService(IBlogPostRepository repository) : base(repository)
    {
    }
}