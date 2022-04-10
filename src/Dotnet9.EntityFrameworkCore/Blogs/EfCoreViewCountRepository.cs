using Dotnet9.Domain.Blogs;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Blogs;

public class EfCoreViewCountRepository : EfCoreRepository<ViewCount>, IViewCountRepository
{
    public EfCoreViewCountRepository(Dotnet9DbContext context) : base(context)
    {
    }
}