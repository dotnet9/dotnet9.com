using Dotnet9.Service.Infrastructure.Repositories;

namespace Dotnet9.Service.Application.Blogs;

public class BlogCommandHandler
{
    private readonly IBlogRepository _blogRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public BlogCommandHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _blogRepository = blogRepository;
        _unitOfWork = unitOfWork;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler(1)]
    public async Task VerifyBlogBySlug(IncreaseBlogViewCountCommand command)
    {
        if (await _blogRepository.FindBySlugAsync(command.Slug) == null)
        {
            throw new Exception("不存在的博文");
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
        
        await _multilevelCacheClient.RemoveAsync<BlogDetails>($"{nameof(BlogRepository)}_{nameof(BlogRepository.FindDetailsBySlugAsync)}_{command.Slug}");
    }
}