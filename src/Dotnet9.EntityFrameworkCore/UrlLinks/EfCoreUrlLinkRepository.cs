using Dotnet9.Domain.UrlLinks;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.UrlLinks;

public class EfCoreUrlLinkRepository : EfCoreRepository<UrlLink>, IUrlLinkRepository
{
    public EfCoreUrlLinkRepository(Dotnet9DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UrlLink?> FindByNameAsync(string name)
    {
        return await DbContext.UrlLinks!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<UrlLink?> FindByUrlAsync(string url)
    {
        return await DbContext.UrlLinks!.FirstOrDefaultAsync(x => x.Url == url);
    }

    public async Task<List<UrlLink>> GetListAsync()
    {
        return await DbContext.UrlLinks!.Where(x => x.Kind == UrlLinkKind.Friend || x.Kind == UrlLinkKind.Owner)
            .OrderBy(x => x.Index).ToListAsync();
    }
}