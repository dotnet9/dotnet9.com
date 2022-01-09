using Dotnet9.Albums;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Dotnet9.Blogs;

public class BlogPostManager : DomainService
{
    private readonly IBlogPostRepository _blogPostRepository;

    public BlogPostManager(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<BlogPost> CreateAsync(
        [NotNull] string title,
        [NotNull] string slug,
        string shortDescription,
        [NotNull] string content,
        string coverImageUrl,
        CopyrightType blogCopyrightType,
        string original,
        string originalTitle,
        string originalLink)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.NotNullOrWhiteSpace(content, nameof(content));

        var existingBlogPost = await _blogPostRepository.FindByTitleAsync(title);
        if (existingBlogPost != null)
        {
            throw new BlogPostTitleAlreadyExistsException(title);
        }

        existingBlogPost = await _blogPostRepository.FindBySlugAsync(slug);
        if (existingBlogPost != null)
        {
            throw new BlogPostSlugAlreadyExistsException(title);
        }

        return new BlogPost(GuidGenerator.Create(), title, slug, shortDescription, content, coverImageUrl,
            blogCopyrightType,
            original, originalTitle, originalLink);
    }

    public async Task ChangeTitleAsync([NotNull] BlogPost blogPost, [NotNull] string newTitle)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        Check.NotNullOrWhiteSpace(newTitle, nameof(newTitle));

        var existingBlogPost = await _blogPostRepository.FindByTitleAsync(newTitle);
        if (existingBlogPost != null && existingBlogPost.Id != blogPost.Id)
        {
            throw new BlogPostTitleAlreadyExistsException(newTitle);
        }

        blogPost.ChangeTitle(newTitle);
    }

    public async Task ChangeSlugAsync([NotNull] BlogPost blogPost, [NotNull] string newSlug)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug));

        var existingBlogPost = await _blogPostRepository.FindBySlugAsync(newSlug);
        if (existingBlogPost != null && existingBlogPost.Id != blogPost.Id)
        {
            throw new BlogPostSlugAlreadyExistsException(newSlug);
        }

        blogPost.ChangeSlug(newSlug);
    }
}