using System;
using System.Linq;
using System.Threading.Tasks;
using Dotnet9.Albums;
using Dotnet9.Categories;
using Dotnet9.Tags;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Dotnet9.Blogs;

public class BlogPostManager : DomainService
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;

    public BlogPostManager(IBlogPostRepository blogPostRepository,IAlbumRepository albumRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
    {
        _blogPostRepository = blogPostRepository;
        _albumRepository = albumRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }

    public async Task<BlogPost> CreateAsync(
        [System.Diagnostics.CodeAnalysis.NotNull]
        string title,
        [System.Diagnostics.CodeAnalysis.NotNull]
        string slug,
        string shortDescription,
        [System.Diagnostics.CodeAnalysis.NotNull]
        string content,
        string coverImageUrl,
        CopyrightType blogCopyrightType,
        string original = default,
        string originalTitle = default,
        string originalLink = default,
        [CanBeNull] string[] albumNames = default,
        [CanBeNull] string[] categoryNames = default,
        [CanBeNull] string[] tagNames = default,
        DateTime creationTime = default)
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

        var blogPost = new BlogPost(GuidGenerator.Create(), title, slug, shortDescription, content, coverImageUrl,
            blogCopyrightType,
            original, originalTitle, originalLink, creationTime);

        await SetAlbumsAsync(blogPost, albumNames);
        await SetCategoriesAsync(blogPost, categoryNames);
        await SetTagsAsync(blogPost, tagNames);

        return blogPost;
    }

    public async Task SetAlbumsAsync(BlogPost blogPost, [CanBeNull] string[] albumNames)
    {
        if (albumNames == null || !albumNames.Any())
        {
            blogPost.RemoveAllAlbums();
            return;
        }

        var query = (await _albumRepository.GetQueryableAsync())
            .Where(x => albumNames.Contains(x.Name))
            .Select(x => x.Id)
            .Distinct();

        var categoryIds = await AsyncExecuter.ToListAsync(query);
        if (!categoryIds.Any())
        {
            return;
        }

        blogPost.RemoveAllCategoriesExceptGivenIds(categoryIds);

        foreach (var categoryId in categoryIds)
        {
            blogPost.AddCategory(categoryId);
        }
    }

    public async Task SetCategoriesAsync(BlogPost blogPost, [CanBeNull] string[] categoryNames)
    {
        if (categoryNames == null || !categoryNames.Any())
        {
            blogPost.RemoveAllCategories();
            return;
        }

        var query = (await _categoryRepository.GetQueryableAsync())
            .Where(x => categoryNames.Contains(x.Name))
            .Select(x => x.Id)
            .Distinct();

        var categoryIds = await AsyncExecuter.ToListAsync(query);
        if (!categoryIds.Any())
        {
            return;
        }

        blogPost.RemoveAllCategoriesExceptGivenIds(categoryIds);

        foreach (var categoryId in categoryIds)
        {
            blogPost.AddCategory(categoryId);
        }
    }

    public async Task SetTagsAsync(BlogPost blogPost, [CanBeNull] string[] tagNames)
    {
        if (tagNames == null || !tagNames.Any())
        {
            blogPost.RemoveAllCategories();
            return;
        }

        var query = (await _categoryRepository.GetQueryableAsync())
            .Where(x => tagNames.Contains(x.Name))
            .Select(x => x.Id)
            .Distinct();

        var categoryIds = await AsyncExecuter.ToListAsync(query);
        if (!categoryIds.Any())
        {
            return;
        }

        blogPost.RemoveAllCategoriesExceptGivenIds(categoryIds);

        foreach (var categoryId in categoryIds)
        {
            blogPost.AddCategory(categoryId);
        }
    }

    public async Task ChangeTitleAsync([System.Diagnostics.CodeAnalysis.NotNull] BlogPost blogPost,
        [System.Diagnostics.CodeAnalysis.NotNull]
        string newTitle)
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

    public async Task ChangeSlugAsync([System.Diagnostics.CodeAnalysis.NotNull] BlogPost blogPost,
        [System.Diagnostics.CodeAnalysis.NotNull]
        string newSlug)
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