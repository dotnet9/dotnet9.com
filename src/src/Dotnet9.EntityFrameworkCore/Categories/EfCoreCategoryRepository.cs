using Dotnet9.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dotnet9.Categories;

public class EfCoreCategoryRepository : EfCoreRepository<Dotnet9DbContext, Category, Guid>, ICategoryRepository
{
    private readonly Dotnet9DbContext _context;

    public EfCoreCategoryRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider, Dotnet9DbContext context) : base(dbContextProvider)
    {
        _context = context;
    }

    public async Task<Category> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(tag => tag.Name == name);
    }

    public async Task<List<Category>> GetListAsync(int skipCount, int maxResultCount, string sorting,
        string filter = null)
    {
        var dbSet = _context.Categories;
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                category => category.Name.Contains(filter)
                            || category.Description.Contains(filter)
            )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}