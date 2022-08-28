using System.Linq.Expressions;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

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

    public async Task<List<CategoryCount>> GetListCountAsync(Expression<Func<Category, bool>> whereLambda)
    {
        var query = from category in DbContext.Set<Category>().Where(whereLambda)
            select new CategoryCount(category.Id, category.ParentId, category.Name, category.Slug, category.Cover,
                DbContext.Set<BlogPostCategory>().Count(d => d.CategoryId == category.Id));

        return await query.ToListAsync();
    }
}