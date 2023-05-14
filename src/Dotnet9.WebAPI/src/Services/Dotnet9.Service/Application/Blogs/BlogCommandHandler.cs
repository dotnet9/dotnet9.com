namespace Dotnet9.Service.Application.Blogs;

public class BlogCommandHandler
{
    private readonly IBlogRepository _blogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BlogCommandHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork)
    {
        _blogRepository = blogRepository;
        _unitOfWork = unitOfWork;
    }

    [EventHandler(1)]
    public async Task VerifyBlog(IncreaseBlogViewCountCommand command)
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
}