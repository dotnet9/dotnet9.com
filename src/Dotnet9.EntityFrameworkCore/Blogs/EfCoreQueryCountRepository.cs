using Dotnet9.Domain.Blogs;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Blogs;

public class EfCoreQueryCountRepository : EfCoreRepository<QueryCount>, IQueryCountRepository
{
    public EfCoreQueryCountRepository(Dotnet9DbContext context) : base(context)
    {
    }
}