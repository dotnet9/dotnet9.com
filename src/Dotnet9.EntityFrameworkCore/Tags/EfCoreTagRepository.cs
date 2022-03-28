using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Tags;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Tags;

public class EfCoreTagRepository : EfCoreRepository<Tag>, ITagRepository
{
    private readonly Dotnet9DbContext _context;

    public EfCoreTagRepository(Dotnet9DbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Tag?> FindByNameAsync(string name)
    {
        return await _context.Tags!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<TagCount>> GetListCountAsync()
    {
        var query = from tag in _context.Tags
            join blogPostTag in _context.Set<BlogPostTag>()
                on tag.Id equals blogPostTag.TagId
            select new {tag.Id, tag.Name}
            into x
            group x by new {x.Id, x.Name}
            into g
            orderby g.Count() descending
            select new TagCount(g.Key.Id, g.Key.Name, g.Count());

        return await query.ToListAsync();
    }
}