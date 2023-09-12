using Dotnet9.Services.Cache;
using Dotnet9Tools.Helper;
using Microsoft.Extensions.Caching.Distributed;

namespace Dotnet9.Services;

public class FriendLinkService
{
    private readonly IDistributedCache _cache;
    private readonly DbContext _context;


    public FriendLinkService(DbContext context, IDistributedCache client)
    {
        _context = context;
        _cache = client;
    }

    public async Task<PageDto<FriendLinkItem>> GetLinkList(GetFriendLinkModel model)
    {
        PageDto<FriendLinkItem>? cache = await _cache.GetModelAsync<PageDto<FriendLinkItem>>(CacheKeys.FriendLinkKey);
        if (cache != null)
        {
            return cache;
        }

        List<FriendLinkItem> list = await _context.Set<FriendLink>().AsNoTracking().Where(a => a.IsPublish)
            .Skip(model.Skip)
            .Take(model.PageSize)
            .OrderByDescending(a => a.Order)
            .Select(a => new FriendLinkItem
            {
                Name = a.Name,
                Url = a.Url
            }).ToListAsync();
        PageDto<FriendLinkItem> dto =
            new PageDto<FriendLinkItem>(await _context.Set<FriendLink>().AsNoTracking().CountAsync(), list);
        await _cache.SetModelAsync(CacheKeys.FriendLinkKey, dto, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        });
        return dto;
    }
}