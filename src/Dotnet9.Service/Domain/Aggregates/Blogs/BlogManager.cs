namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class BlogManager : IScopedDependency
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IBlogRepository _blogRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;

    public BlogManager(IBlogRepository blogRepository, IAlbumRepository albumRepository,
        ICategoryRepository categoryRepository, ITagRepository tagRepository)
    {
        _blogRepository = blogRepository;
        _albumRepository = albumRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }

    public async Task<Blog> CreateAsync(
        Guid? id,
        string title,
        string slug,
        string description,
        string cover,
        string content,
        CopyRightType copyrightType,
        string? original,
        string? lastModifyUser,
        string? originalAvatar,
        string? originalTitle,
        string? originalLink,
        bool draft,
        bool banner,
        bool visible,
        Guid[]? albumIds,
        Guid[]? categoryIds,
        Guid[]? tagIds)
    {
        bool isNew = id == null;
        Blog? oldData = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldData = await _blogRepository.FindByIdAsync(id!.Value);
            if (oldData == null)
            {
                throw new Exception($"不存在的文章: {id}");
            }
        }

        if (isNew)
        {
            oldData = new Blog(id.Value, title, slug, description, cover, content, copyrightType, original, lastModifyUser,
                originalAvatar, originalTitle, originalLink, draft, banner, visible);
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

    public Blog CreateForSeed(
        string title,
        string slug,
        string description,
        string cover,
        string content,
        CopyRightType copyrightType,
        string? original,
        string? lastModifyUser,
        string? originalAvatar,
        string? originalTitle,
        string? originalLink,
        bool draft,
        bool banner,
        bool visible,
        Guid[]? albumIds,
        Guid[]? categoryIds,
        Guid[]? tagIds,
        DateTime creationTime,
        DateTime? lastModifyDate)
    {
        Blog blog = new Blog(Guid.NewGuid(), title, slug, description, cover, content, copyrightType,
            original, lastModifyUser,
            originalAvatar, originalTitle, originalLink, draft, banner, visible);
        if (albumIds != null)
        {
            foreach (Guid albumId in albumIds)
            {
                blog.AddAlbum(albumId);
            }
        }

        if (categoryIds != null)
        {
            foreach (Guid categoryId in categoryIds)
            {
                blog.AddCategory(categoryId);
            }
        }

        if (tagIds != null)
        {
            foreach (Guid tagId in tagIds)
            {
                blog.AddTag(tagId);
            }
        }

        blog.SetCreationTime(creationTime);
        blog.SetLastModifyDate(lastModifyDate);

        return blog;
    }

    public async Task ChangeTitleAsync(bool isNew, Blog blog, string newTitle)
    {
        Check.NotNull(blog, nameof(blog));
        Check.NotNullOrWhiteSpace(newTitle, nameof(newTitle), BlogConsts.MaxTitleLength,
            BlogConsts.MinTitleLength);

        Blog? existData = await _blogRepository.FindByTitleAsync(newTitle);
        if (existData != null && existData.Id != blog.Id)
        {
            throw new Exception("存在相同标题的文章");
        }

        blog.ChangeTitle(newTitle);
    }

    public async Task ChangeSlugAsync(bool isNew, Blog blog, string newSlug)
    {
        Check.NotNull(blog, nameof(blog));
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug), BlogConsts.MaxSlugLength, BlogConsts.MinSlugLength);

        var existData = await _blogRepository.FindBySlugAsync(newSlug);
        if (existData != null && existData.Id != blog.Id)
        {
            throw new Exception("存在相同别名的文章");
        }

        blog.ChangeSlug(newSlug);
    }

    public async Task ChangeAlbumAsync(Blog blog, Guid[]? albumIds)
    {
        Check.NotNull(blog, nameof(blog));
        if (albumIds == null || !albumIds.Any())
        {
            return;
        }

        foreach (Guid id in albumIds)
        {
            Album? existData = await _albumRepository.FindByIdAsync(id);
            if (existData == null)
            {
                throw new Exception($"不存在的专辑: {id}");
            }
        }

        blog.RemoveAllAlbumsExceptGivenIds(albumIds.ToList());
        foreach (Guid id in albumIds)
        {
            blog.AddAlbum(id);
        }
    }

    public async Task ChangeCategoryAsync(Blog blog, Guid[]? categoryIds)
    {
        Check.NotNull(blog, nameof(blog));
        if (categoryIds == null || !categoryIds.Any())
        {
            return;
        }

        foreach (Guid id in categoryIds)
        {
            Category? existData = await _categoryRepository.FindByIdAsync(id);
            if (existData == null)
            {
                throw new Exception($"不存在的分类: {id}");
            }
        }

        blog.RemoveAllCategoriesExceptGivenIds(categoryIds.ToList());
        foreach (Guid id in categoryIds)
        {
            blog.AddCategory(id);
        }
    }

    public async Task ChangeTagAsync(Blog blog, Guid[]? tagIds)
    {
        Check.NotNull(blog, nameof(blog));
        if (tagIds == null || !tagIds.Any())
        {
            return;
        }

        foreach (Guid id in tagIds)
        {
            Tag? existData = await _tagRepository.FindByIdAsync(id);
            if (existData == null)
            {
                throw new Exception($"不存在的标签: {id}");
            }
        }

        blog.RemoveAllTagsExceptGivenIds(tagIds.ToList());
        foreach (Guid id in tagIds)
        {
            blog.AddTag(id);
        }
    }


    public async Task<Blog> ChangeVisible(Guid id, bool visible)
    {
        Blog? oldData = await _blogRepository.FindByIdAsync(id);
        if (oldData == null)
        {
            throw new Exception($"不存在的文章: {id}");
        }

        oldData.ChangeVisible(visible);
        return oldData;
    }
}