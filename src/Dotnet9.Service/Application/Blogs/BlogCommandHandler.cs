namespace Dotnet9.Service.Application.Blogs;

public class BlogCommandHandler
{
    private readonly IBlogRepository _blogRepository;
    private BlogManager _blogManager;
    private readonly IDistributedCacheHelper _redisClient;

    public BlogCommandHandler(IBlogRepository blogRepository, BlogManager blogManager, IUnitOfWork unitOfWork,
        IDistributedCacheHelper redisClient)
    {
        _blogRepository = blogRepository;
        _blogManager = blogManager;
        _redisClient = redisClient;
    }

    [EventHandler(1)]
    public async Task CreateBlogCount(CreateBlogCountCommand command, CancellationToken cancellationToken)
    {
        var blog = await _blogManager.CreateBlogCount(command.BlogId, command.Ip, command.Kind);
        await _blogRepository.UpdateAsync(blog, cancellationToken);
    }

    [EventHandler(1)]
    public async Task CreateBlogSearchCount(CreateBlogSearchCountCommand command, CancellationToken cancellationToken)
    {
        await _blogRepository.CreateBlogSearchCount(command.Keywords, command.IsEmpty, command.Ip,
            command.CreationTime);
    }
}