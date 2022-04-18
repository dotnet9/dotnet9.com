using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Categories;

public class EfCoreCategoryRepository : EfCoreRepository<Category>, ICategoryRepository
{
    public EfCoreCategoryRepository(Dotnet9DbContext context) : base(context)
    {
    }

    public async Task<Category?> FindByNameAsync(string name)
    {
        return await DbContext.Categories!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Category?> FindBySlugAsync(string slug)
    {
        return await DbContext.Categories!.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<List<CategoryCount>> GetListCountAsync()
    {
        var query = from category in DbContext.Set<Category>()
            select new CategoryCount(category.Id, category.ParentId, category.Name, category.Slug, category.Cover,
                DbContext.Set<BlogPostCategory>().Count(d => d.CategoryId == category.Id));

        return await query.ToListAsync();
    }
}