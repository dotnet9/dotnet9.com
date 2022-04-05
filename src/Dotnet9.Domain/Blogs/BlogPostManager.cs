using Dotnet9.Core;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Shared.Blogs;
using Dotnet9.Domain.Tags;

namespace Dotnet9.Domain.Blogs;

public class BlogPostManager
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;

    public BlogPostManager(
        IBlogPostRepository blogPostRepository,
        IAlbumRepository albumRepository,
        ICategoryRepository categoryRepository,
        ITagRepository tagRepository)
    {
        _blogPostRepository = blogPostRepository;
        _albumRepository = albumRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }

    public async Task<BlogPost> CreateAsync(
        string title,
        string slug,
        string? briefDescription,
        bool inBanner,
        string cover,
        string content,
        CopyrightType blogCopyrightType,
        string? original = default,
        string? originalAvatar = default,
        string? originalTitle = default,
        string? originalLink = default,
        string[]? albumNames = default,
        string[]? categoryNames = default,
        string[]? tagNames = default,
        DateTime createDate = default)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.NotNullOrWhiteSpace(content, nameof(content));

        var existingBlogPost = await _blogPostRepository.GetAsync(x => x.Title == title);
        if (existingBlogPost != null) throw new Exception($"存在相同标题的博文，标题：{title}");

        existingBlogPost = await _blogPostRepository.GetAsync(x => x.Slug == slug);
        if (existingBlogPost != null) throw new Exception($"存在相同别名的博文，别名：{slug}");

        var maxId = await _blogPostRepository.GetMaxIdAsync();
        var blogPost = new BlogPost(maxId + 1, title, slug, briefDescription, inBanner, cover, content,
            blogCopyrightType,
            original, originalAvatar, originalTitle, originalLink, createDate);

        await SetAlbumsAsync(blogPost, albumNames);
        await SetCategoriesAsync(blogPost, categoryNames);
        await SetTagsAsync(blogPost, tagNames);

        return blogPost;
    }

    public async Task SetAlbumsAsync(BlogPost blogPost, string[]? albumNames)
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

        var albumIds = query.ToList();
        if (!albumIds.Any()) return;

        blogPost.RemoveAllAlbumsExceptGivenIds(albumIds);

        foreach (var albumId in albumIds) blogPost.AddAlbum(albumId);
    }

    public async Task SetCategoriesAsync(BlogPost blogPost, string[]? categoryNames)
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

        var categoryIds = query.ToList();
        if (!categoryIds.Any()) return;

        blogPost.RemoveAllCategoriesExceptGivenIds(categoryIds);

        foreach (var categoryId in categoryIds) blogPost.AddCategory(categoryId);
    }

    public async Task SetTagsAsync(BlogPost blogPost, string[]? tagNames)
    {
        if (tagNames == null || !tagNames.Any())
        {
            blogPost.RemoveAllTags();
            return;
        }

        var query = (await _tagRepository.GetQueryableAsync())
            .Where(x => tagNames.Contains(x.Name))
            .Select(x => x.Id)
            .Distinct();

        var tagIds = query.ToList();
        if (!tagIds.Any()) return;

        blogPost.RemoveAllTagsExceptGivenIds(tagIds);

        foreach (var tagId in tagIds) blogPost.AddTag(tagId);
    }

    public async Task ChangeTitleAsync(BlogPost blogPost, string newTitle)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        Check.NotNullOrWhiteSpace(newTitle, nameof(newTitle));

        var existingBlogPost = await _blogPostRepository.GetAsync(x => x.Title == newTitle);
        if (existingBlogPost != null && existingBlogPost.Id != blogPost.Id)
            throw new Exception($"存在相同标题的博文：{newTitle}");

        blogPost.ChangeTitle(newTitle);
    }

    public async Task ChangeSlugAsync(BlogPost blogPost, string newSlug)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug));

        var existingBlogPost = await _blogPostRepository.GetAsync(x => x.Slug == newSlug);
        if (existingBlogPost != null && existingBlogPost.Id != blogPost.Id) throw new Exception($"存在相同别名的博文：{newSlug}");

        blogPost.ChangeSlug(newSlug);
    }
}