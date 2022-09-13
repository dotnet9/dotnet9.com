namespace Dotnet9.WebAPI.Domain.BlogPosts;

public class BlogPostManager
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;

    public BlogPostManager(IBlogPostRepository blogPostRepository, IAlbumRepository albumRepository,
        ICategoryRepository categoryRepository, ITagRepository tagRepository)
    {
        _blogPostRepository = blogPostRepository;
        _albumRepository = albumRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }

    public async Task<BlogPost> CreateAsync(
        Guid? id,
        string title,
        string slug,
        string description,
        string cover,
        string content,
        CopyRightType copyrightType,
        string? original,
        string? originalAvatar,
        string? originalTitle,
        string? originalLink,
        bool visible,
        Guid[]? albumIds,
        Guid[]? categoryIds,
        Guid[]? tagIds)
    {
        var isNew = id == null;
        BlogPost? oldData = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldData = await _blogPostRepository.FindByIdAsync(id!.Value);
            if (oldData == null)
            {
                throw new Exception($"不存在的文章: {id}");
            }
        }

        if (isNew)
        {
            oldData = new BlogPost(id.Value, title, slug, description, cover, content, copyrightType, original,
                originalAvatar, originalTitle, originalLink, visible);
        }
        else
        {
            oldData!.ChangeDescription(description);
            oldData.ChangeCover(cover);
            oldData.ChangeContent(content);
            oldData.ChangeCopyrightType(copyrightType);
            oldData.ChangeOriginal(original);
            oldData.ChangeOriginalAvatar(originalAvatar);
            oldData.ChangeOriginalTitle(originalTitle);
            oldData.ChangeOriginalLink(originalLink);
            oldData.ChangeVisible(visible);
        }

        await ChangeTitleAsync(isNew, oldData, title);
        await ChangeSlugAsync(isNew, oldData, slug);
        await ChangeAlbumAsync(oldData, albumIds);
        await ChangeCategoryAsync(oldData, categoryIds);
        await ChangeTagAsync(oldData, tagIds);
        return oldData;
    }

    public BlogPost CreateForSeed(
        string title,
        string slug,
        string description,
        string cover,
        string content,
        CopyRightType copyrightType,
        string? original,
        string? originalAvatar,
        string? originalTitle,
        string? originalLink,
        bool visible,
        Guid[]? albumIds,
        Guid[]? categoryIds,
        Guid[]? tagIds)
    {
        var blogPost = new BlogPost(Guid.NewGuid(), title, slug, description, cover, content, copyrightType, original,
            originalAvatar, originalTitle, originalLink, visible);
        if (albumIds != null)
        {
            foreach (var albumId in albumIds)
            {
                blogPost.AddAlbum(albumId);
            }
        }

        if (categoryIds != null)
        {
            foreach (var categoryId in categoryIds)
            {
                blogPost.AddCategory(categoryId);
            }
        }

        if (tagIds != null)
        {
            foreach (var tagId in tagIds)
            {
                blogPost.AddTag(tagId);
            }
        }

        return blogPost;
    }

    public async Task ChangeTitleAsync(bool isNew, BlogPost blogPost, string newTitle)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        Check.NotNullOrWhiteSpace(newTitle, nameof(newTitle), BlogPostConsts.MaxTitleLength,
            BlogPostConsts.MinTitleLength);

        var existData = await _blogPostRepository.FindByTitleAsync(newTitle);
        if (existData != null && existData.Id != blogPost.Id)
        {
            throw new Exception("存在相同标题的文章");
        }

        blogPost.ChangeTitle(newTitle);
    }

    public async Task ChangeSlugAsync(bool isNew, BlogPost blogPost, string newSlug)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug), BlogPostConsts.MaxSlugLength, BlogPostConsts.MinSlugLength);

        var existData = await _blogPostRepository.FindBySlugAsync(newSlug);
        if (existData != null && existData.Id != blogPost.Id)
        {
            throw new Exception("存在相同别名的文章");
        }

        blogPost.ChangeSlug(newSlug);
    }

    public async Task ChangeAlbumAsync(BlogPost blogPost, Guid[]? albumIds)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        if (albumIds == null || !albumIds.Any())
        {
            return;
        }

        foreach (var id in albumIds)
        {
            var existData = await _albumRepository.FindByIdAsync(id);
            if (existData == null)
            {
                throw new Exception($"不存在的专辑: {id}");
            }
        }

        blogPost.RemoveAllAlbumsExceptGivenIds(albumIds.ToList());
        foreach (var id in albumIds)
        {
            blogPost.AddAlbum(id);
        }
    }

    public async Task ChangeCategoryAsync(BlogPost blogPost, Guid[]? categoryIds)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        if (categoryIds == null || !categoryIds.Any())
        {
            return;
        }

        foreach (var id in categoryIds)
        {
            var existData = await _categoryRepository.FindByIdAsync(id);
            if (existData == null)
            {
                throw new Exception($"不存在的分类: {id}");
            }
        }

        blogPost.RemoveAllCategoriesExceptGivenIds(categoryIds.ToList());
        foreach (var id in categoryIds)
        {
            blogPost.AddCategory(id);
        }
    }

    public async Task ChangeTagAsync(BlogPost blogPost, Guid[]? tagIds)
    {
        Check.NotNull(blogPost, nameof(blogPost));
        if (tagIds == null || !tagIds.Any())
        {
            return;
        }

        foreach (var id in tagIds)
        {
            var existData = await _tagRepository.FindByIdAsync(id);
            if (existData == null)
            {
                throw new Exception($"不存在的标签: {id}");
            }
        }

        blogPost.RemoveAllTagsExceptGivenIds(tagIds.ToList());
        foreach (var id in tagIds)
        {
            blogPost.AddTag(id);
        }
    }
}