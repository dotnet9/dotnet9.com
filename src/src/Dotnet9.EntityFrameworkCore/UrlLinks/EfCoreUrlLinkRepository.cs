using Dotnet9.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace Dotnet9.UrlLinks;

public class EfCoreUrlLinkRepository : EfCoreRepository<Dotnet9DbContext, UrlLink, Guid>, IUrlLinkRepository
{
    public EfCoreUrlLinkRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<UrlLink> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(urlLink => urlLink.Name == name);
    }

    public async Task<UrlLink> FindByUrlAsync(string url)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(urlLink => urlLink.Url == url);
    }

    public async Task<List<UrlLink>> GetListAsync(int skipCount, int maxResultCount, string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                blogPost => blogPost.Name.Contains(filter)
                            || blogPost.Url.Contains(filter) ||
                            blogPost.Description.Contains(filter)
            )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}