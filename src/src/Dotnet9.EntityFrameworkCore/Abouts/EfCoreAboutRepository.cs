using Dotnet9.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dotnet9.Abouts;

public class EfCoreAboutRepository : EfCoreRepository<Dotnet9DbContext, About, Guid>, IAboutRepository
{
    public EfCoreAboutRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    [ItemCanBeNull]
    public async Task<About> GetAsync()
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync();
    }
}