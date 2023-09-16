using Dotnet9Tools.Helper;
using Microsoft.Extensions.Caching.Distributed;

namespace Dotnet9.Services.Blogs;

public class PostCateService
{
    private readonly IDistributedCache _cache;
    private readonly DbContext _context;

    public PostCateService(DbContext context, IDistributedCache client)
    {
        _context = context;
        _cache = client;
    }

    public async Task<List<CateCountItem>> GetCateList(int top)
    {
        List<CateCountItem>? res = await _cache.GetModelAsync($"{nameof(PostCateService)}:list", async () =>
        {
            List<CateCountItem> list = await _context.Set<PostCates>().AsNoTracking().Take(top).Select(a =>
                new CateCountItem
                {
                    Id = a.Id,
                    CateName = a.CateName,
                    Count = _context.Set<PostCateRelation>().Count(x => x.PostCate == a)
                }).ToListAsync();
            return list;
        }, _cache.GetOption(TimeSpan.FromMinutes(5)));
        return res ?? new List<CateCountItem>();
    }

    public async Task<string?> GetCateNameById(Guid Id)
    {
        return await _context.Set<PostCates>().AsNoTracking().Where(a => a.Id == Id).Select(a => a.CateName)
            .FirstOrDefaultAsync();
    }
}