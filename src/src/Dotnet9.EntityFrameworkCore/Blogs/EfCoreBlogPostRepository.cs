using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Dotnet9.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dotnet9.Blogs;

public class EfCoreBlogPostRepository : EfCoreRepository<Dotnet9DbContext, BlogPost, Guid>, IBlogPostRepository
{
    public EfCoreBlogPostRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<BlogPost> FindByTitleAsync(string title)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(blogPost => blogPost.Title == title);
    }

    public async Task<BlogPost> FindBySlugAsync(string slug)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(blogPost => blogPost.Slug == slug);
    }

    public async Task<List<BlogPost>> GetListAsync(int skipCount, int maxResultCount, string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                blogPost => blogPost.Title.Contains(filter)
                            || blogPost.Slug.Contains(filter) ||
                            blogPost.Content.Contains(filter)
            )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}