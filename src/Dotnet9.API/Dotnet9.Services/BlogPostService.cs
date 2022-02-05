using System.Runtime.InteropServices;
using Dotnet9.IRepositories;
using Dotnet9.IServices;
using Dotnet9.Models.Models;
using Dotnet9.Services.Base;

namespace Dotnet9.Services;

public class BlogPostService : BaseService<BlogPost>, IBlogPostService
{
    private readonly IBlogPostRepository _blogPostRepository;

    public BlogPostService(IBlogPostRepository blogPostRepository) : base(blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }


    public async Task<BlogPost> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _blogPostRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<BlogPost> GetBlogPostDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _blogPostRepository.GetByIdAsync(id, cancellationToken);
        entity.Traffic += 1;

        await _blogPostRepository.UpdateAsync(entity, true, cancellationToken);

        return entity;
    }

    public async Task AddUserInfoBlogPostAsync(int id, int userId, CancellationToken cancellationToken = default)
    {
        var entity = await _blogPostRepository.GetUserInfoBlogPostByIdAsync(id, cancellationToken);
        entity.UserInfoBlogPosts.Add(new UserInfoBlogPost()
        {
            UserId = userId,
            BlogPostId = id
        });
        await _blogPostRepository.UpdateAsync(entity, true, cancellationToken);
    }

    public async Task AddBlogPostComment(int id, int userId, string content,
        CancellationToken cancellationToken = default)
    {
        var entity = await _blogPostRepository.GetByIdAsync(id, cancellationToken);
        entity.BlogPostComments.Add(new BlogPostComment
        {
            Content = content,
            CreateTime = DateTime.UtcNow,
            CreateUserId = userId
        });

        await _blogPostRepository.UpdateAsync(entity, true, cancellationToken);
    }
}