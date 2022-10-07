using Dotnet9.Domain.Timelines;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Timelines;

public class EfCoreTimelineRepository : EfCoreRepository<Timeline>, ITimelineRepository
{
    public EfCoreTimelineRepository(Dotnet9DbContext context) : base(context)
    {
    }

    public async Task<List<Timeline>> GetListAsync()
    {
        return await DbContext.Timelines!.OrderByDescending(x => x.Time).ToListAsync();
    }
}