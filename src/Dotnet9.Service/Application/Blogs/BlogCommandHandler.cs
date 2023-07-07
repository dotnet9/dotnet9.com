namespace Dotnet9.Service.Application.Blogs;

public class BlogCommandHandler
{
    private readonly IBlogRepository _blogRepository;
    private readonly RedisClient _redisClient;

    public BlogCommandHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork,
        RedisClient redisClient)
    {
        _blogRepository = blogRepository;
        _redisClient = redisClient;
    }

    [EventHandler(1)]
    public async Task VerifyBlogBySlug(IncreaseBlogViewCountCommand command)
    {
        if (await _blogRepository.FindBySlugAsync(command.Slug) == null)
        {
            throw new Exception($"不存在的博文: {command.Slug}");
        }
    }

    [EventHandler(2)]
    public async Task IncreaseBlogViewCount(IncreaseBlogViewCountCommand command, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.FindBySlugAsync(command.Slug);
        blog!.IncreaseViewCount();
        await _blogRepository.UpdateAsync(blog, cancellationToken);
    }

    [EventHandler(3)]
    public async Task ClearBlogViewCountCache(IncreaseBlogViewCountCommand command, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.FindBySlugAsync(command.Slug);
        blog!.IncreaseViewCount();
        await _blogRepository.UpdateAsync(blog, cancellationToken);
        await _redisClient.DelAsync(command.Slug);
    }

    [EventHandler(1)]
    public async Task CreateBlogViewCount(CreateBlogViewCountCommand command, CancellationToken cancellationToken)
    {
        await _blogRepository.CreateBlogViewCount(command.Slug, command.Ip, command.CreationTime);
    }

    [EventHandler(1)]
    public async Task CreateBlogSearchCount(CreateBlogSearchCountCommand command, CancellationToken cancellationToken)
    {
        await _blogRepository.CreateBlogSearchCount(command.Keywords, command.IsEmpty, command.Ip,
            command.CreationTime);
    }
}