using Dotnet9.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dotnet9.Blogs;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dotnet9.Categories;

public class EfCoreCategoryRepository : EfCoreRepository<Dotnet9DbContext, Category, Guid>, ICategoryRepository
{
    private readonly Dotnet9DbContext _context;

    public EfCoreCategoryRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider, Dotnet9DbContext context) :
        base(dbContextProvider)
    {
        _context = context;
    }

    public async Task<Category> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(tag => tag.Name == name);
    }

    public async Task<List<CategoryCount>> GetListCountAsync()
    {
        var query = from category in _context.Categories
            join blogPostCategory in _context.Set<BlogPostCategory>()
                on category.Id equals blogPostCategory.CategoryId
            select new { category.Id, category.ParentId, category.Name, category.Description }
            into x
            group x by new { x.Id, x.ParentId, x.Name, x.Description }
            into g
            orderby g.Count() descending
            select new CategoryCount(g.Key.Id, g.Key.ParentId, g.Key.Name, g.Key.Description, g.Count());
        return await query.ToListAsync();
    }

    public async Task<List<Category>> GetListAsync(int skipCount, int maxResultCount, string sorting,
        string filter = null)
    {
        var categories = _context.Categories;
        return await categories
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