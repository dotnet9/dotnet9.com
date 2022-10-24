namespace Dotnet9.WebAPI.EventHandlers;

[EventName("Dotnet9.Web.BlogPosts.OnGet")]
public class ReadBlogPostEventHandler : JsonIntegrationEventHandler<ReadBlogPostEvent>
{
    private readonly ILogger<ReadBlogPostEventHandler> _logger;
    private readonly IBlogPostRepository _repository;

    public ReadBlogPostEventHandler(ILogger<ReadBlogPostEventHandler> logger, IBlogPostRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public override async Task HandleJson(string eventName, ReadBlogPostEvent? eventData)
    {
        if (eventData?.Slug.IsNullOrWhiteSpace() == true)
        {
            _logger.LogWarning("文章别名不能为空");
            return;
        }

        bool result = await _repository.IncreaseViewCountAsync(eventData!.Slug);
        _logger.LogInformation($"增加文章阅读量({eventData.Slug})：{result}");
    }
}