using Dotnet9.EntityFramework;
using Dotnet9.IRepositories;
using Dotnet9.Models.Models;
using Dotnet9.Repositories.Base;

namespace Dotnet9.Repositories;

public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
{
    public BlogPostRepository(Dotnet9Context context) : base(context)
    {
    }
}