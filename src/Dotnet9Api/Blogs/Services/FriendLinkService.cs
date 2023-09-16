using Dotnet9.Services.Cache;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;

namespace Dotnet9Api.Blogs.Services;

public class FriendLinkService
{
    private readonly IDistributedCache _cache;
    private readonly Dotnet9DbContext _context;

    public FriendLinkService(Dotnet9DbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<PageDto<FriendLinkModel>> GetList(FriendLinkRequestModel request)
    {
        List<FriendLinkModel> list = await _context.FriendLinks.Skip(request.Skip).Take(request.PageSize).Select(a =>
            new FriendLinkModel
            {
                Name = a.Name,
                Url = a.Url,
                Order = a.Order,
                Id = a.Id,
                IsPublish = a.IsPublish
            }).ToListAsync();
        return new PageDto<FriendLinkModel>(await _context.FriendLinks.CountAsync(), list);
    }

    public async Task Edit(FriendLinkModel model)
    {
        if (model.Id != null)
        {
            FriendLink? item = await _context.FriendLinks.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (item != null)
            {
                model.Adapt(item);
                await _context.SaveChangesAsync();
            }
        }
        else
        {
            FriendLink item = model.Adapt<FriendLink>();
            await _context.FriendLinks.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        await _cache.RemoveAsync(CacheKeys.FriendLinkKey);
    }
}