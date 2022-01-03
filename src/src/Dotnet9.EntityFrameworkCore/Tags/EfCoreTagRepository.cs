using Dotnet9.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dotnet9.Tags;

public class EfCoreTagRepository : EfCoreRepository<Dotnet9DbContext, Tag, Guid>, ITagRepository
{
    public EfCoreTagRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<Tag> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(tag => tag.Name == name);
    }

    public async Task<List<Tag>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                tag => tag.Name.Contains(filter)
            )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}