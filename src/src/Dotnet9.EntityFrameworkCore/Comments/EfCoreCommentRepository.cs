using Dotnet9.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dotnet9.Comments;

public class EfCoreCommentRepository : EfCoreRepository<Dotnet9DbContext, Comment, Guid>, ICommentRepository
{
    public EfCoreCommentRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Comment>> GetListAsync(int skipCount, int maxResultCount, string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                album => album.Text.Contains(filter)
            )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}