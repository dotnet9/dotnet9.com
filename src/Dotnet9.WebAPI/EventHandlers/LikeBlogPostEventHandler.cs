namespace Dotnet9.WebAPI.EventHandlers;

public class LikeBlogPostEventHandler : INotificationHandler<LikeBlogPostEvent>
{
    private readonly ILogger<LikeBlogPostEventHandler> _logger;
    private readonly IDistributedCacheHelper _cacheHelper;

    public LikeBlogPostEventHandler(ILogger<LikeBlogPostEventHandler> logger, IDistributedCacheHelper cacheHelper)
    {
        _logger = logger;
        _cacheHelper = cacheHelper;
    }

    public async Task Handle(LikeBlogPostEvent notification, CancellationToken cancellationToken)
    {
        // 清空阅读文章详情页的缓存
        string cacheKey = $"BlogPost_{notification.Slug}";
        await _cacheHelper.RemoveAsync(cacheKey);
    }
}