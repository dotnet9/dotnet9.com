using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Categories;

public class EfCoreCategoryRepository : EfCoreRepository<Category>, ICategoryRepository
{
    private readonly Dotnet9DbContext _context;

    public EfCoreCategoryRepository(Dotnet9DbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Category?> FindByNameAsync(string name)
    {
        return await _context.Categories!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Category?> FindBySlugAsync(string slug)
    {
        return await _context.Categories!.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<List<CategoryCount>> GetListCountAsync()
    {
        var query = from category in _context.Categories
            join blogPostCategory in _context.Set<BlogPostCategory>()
                on category.Id equals blogPostCategory.CategoryId
            select new {category.Id, category.ParentId, category.Name, category.Slug, category.Cover}
            into x
            group x by new {x.Id, x.ParentId, x.Name, x.Slug, x.Cover}
            into g
            orderby g.Count() descending
            select new CategoryCount(g.Key.Id, g.Key.ParentId, g.Key.Name, g.Key.Slug, g.Key.Cover, g.Count());

        return await query.ToListAsync();
    }
}