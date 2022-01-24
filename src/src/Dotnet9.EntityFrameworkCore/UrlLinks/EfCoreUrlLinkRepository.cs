using Dotnet9.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace Dotnet9.UrlLinks;

public class EfCoreUrlLinkRepository : EfCoreRepository<Dotnet9DbContext, UrlLink, Guid>, IUrlLinkRepository
{
    private readonly Dotnet9DbContext _context;

    public EfCoreUrlLinkRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider, Dotnet9DbContext context) : base(dbContextProvider)
    {
        _context = context;
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
        var dbSet = _context.UrlLinks;
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

    public async Task<int> CountAsync(string filter)
    {
        var dbSet = _context.UrlLinks;
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                blogPost => blogPost.Name.Contains(filter)
                            || blogPost.Url.Contains(filter) ||
                            blogPost.Description.Contains(filter)
            )
            .CountAsync();
    }
}