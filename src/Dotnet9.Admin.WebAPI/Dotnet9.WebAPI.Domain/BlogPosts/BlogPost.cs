namespace Dotnet9.WebAPI.Domain.BlogPosts;

public record BlogPost : AggregateRootEntity
{
    private BlogPost()
    {
    }

    internal BlogPost(
        Guid id,
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
        bool visible)
    {
        Id = id;
        ChangeTitle(title);
        ChangeSlug(slug);
        ChangeDescription(description);
        ChangeCover(cover);
        ChangeContent(content);
        ChangeCopyrightType(copyrightType);
        ChangeOriginal(original);
        ChangeOriginalAvatar(originalAvatar);
        ChangeOriginalTitle(originalTitle);
        ChangeOriginalLink(originalLink);
        ChangeVisible(visible);

        Albums = new List<BlogPostAlbum>();
        Categories = new List<BlogPostCategory>();
        Tags = new List<BlogPostTag>();
    }

    public new DateTime CreationTime { get; private set; }
    public string Title { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Cover { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public CopyRightType CopyrightType { get; private set; }
    public string? Original { get; set; }
    public string? OriginalAvatar { get; set; }
    public string? OriginalTitle { get; set; }
    public string? OriginalLink { get; set; }
    public bool Visible { get; private set; }

    public List<BlogPostAlbum>? Albums { get; }
    public List<BlogPostCategory>? Categories { get; }
    public List<BlogPostTag>? Tags { get; }


    internal BlogPost ChangeTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), BlogPostConsts.MaxTitleLength,
            BlogPostConsts.MinTitleLength);
        return this;
    }

    internal BlogPost ChangeSlug(string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), BlogPostConsts.MaxSlugLength,
            BlogPostConsts.MinSlugLength);
        return this;
    }

    internal BlogPost ChangeDescription(string description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(description), BlogPostConsts.MaxDescriptionLength,
            BlogPostConsts.MinDescriptionLength);
        return this;
    }

    internal BlogPost ChangeCover(string cover)
    {
        Cover = Check.NotNullOrWhiteSpace(cover, nameof(cover), BlogPostConsts.MaxCoverLength,
            BlogPostConsts.MinCoverLength);
        return this;
    }

    internal BlogPost ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), BlogPostConsts.MaxContentLength,
            BlogPostConsts.MinContentLength);
        return this;
    }

    internal BlogPost ChangeCopyrightType(CopyRightType type)
    {
        CopyrightType = type;
        return this;
    }

    internal BlogPost ChangeOriginal(string? original)
    {
        Original = Check.Length(original, nameof(original), BlogPostConsts.MaxOriginalLength);
        return this;
    }

    internal BlogPost ChangeOriginalAvatar(string? originalAvatar)
    {
        OriginalAvatar = Check.Length(originalAvatar, nameof(originalAvatar),
            BlogPostConsts.MaxOriginalAvatarLength);
        return this;
    }

    internal BlogPost ChangeOriginalTitle(string? originalTitle)
    {
        OriginalTitle = Check.Length(originalTitle, nameof(originalTitle),
            BlogPostConsts.MaxOriginalTitleLength);
        return this;
    }

    internal BlogPost ChangeOriginalLink(string? originalLink)
    {
        OriginalLink = Check.Length(originalLink, nameof(originalLink),
            BlogPostConsts.MaxOriginalLinkLength);
        return this;
    }

    internal BlogPost ChangeVisible(bool visible)
    {
        Visible = visible;
        return this;
    }

    internal BlogPost SetCreationTime(DateTime creationTime)
    {
        CreationTime = creationTime;
        return this;
    }

    #region album

    public void AddAlbum(Guid albumId)
    {
        if (IsInAlbum(albumId))
        {
            return;
        }

        Albums!.Add(new BlogPostAlbum(Id, albumId));
    }

    public void RemoveAlbum(Guid albumId)
    {
        if (!IsInAlbum(albumId))
        {
            return;
        }

        Albums!.RemoveAll(x => x.AlbumId == albumId);
    }

    public void RemoveAllAlbumsExceptGivenIds(List<Guid> albumIds)
    {
        Check.NotNullOrEmpty(albumIds, nameof(albumIds));

        Albums!.RemoveAll(x => !albumIds.Contains(x.AlbumId));
    }

    public void RemoveAllAlbums()
    {
        Albums!.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInAlbum(Guid albumId)
    {
        return Albums!.Any(x => x.AlbumId == albumId);
    }

    #endregion algum

    #region category

    public void AddCategory(Guid categoryId)
    {
        if (IsInCategory(categoryId))
        {
            return;
        }

        Categories!.Add(new BlogPostCategory(Id, categoryId));
    }

    public void RemoveCategory(Guid categoryId)
    {
        if (!IsInCategory(categoryId))
        {
            return;
        }

        Categories!.RemoveAll(x => x.CategoryId == categoryId);
    }

    public void RemoveAllCategoriesExceptGivenIds(List<Guid> categoryIds)
    {
        Check.NotNullOrEmpty(categoryIds, nameof(categoryIds));

        Categories!.RemoveAll(x => !categoryIds.Contains(x.CategoryId));
    }

    public void RemoveAllCategories()
    {
        Categories!.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInCategory(Guid categoryId)
    {
        return Categories!.Any(x => x.CategoryId == categoryId);
    }

    #endregion

    #region tag

    public void AddTag(Guid tagId)
    {
        if (IsInTag(tagId))
        {
            return;
        }

        Tags!.Add(new BlogPostTag(Id, tagId));
    }

    public void RemoveTag(Guid tagId)
    {
        if (!IsInTag(tagId))
        {
            return;
        }

        Tags!.RemoveAll(x => x.TagId == tagId);
    }

    public void RemoveAllTagsExceptGivenIds(List<Guid> tagIds)
    {
        Check.NotNullOrEmpty(tagIds, nameof(tagIds));

        Tags!.RemoveAll(x => !tagIds.Contains(x.TagId));
    }

    public void RemoveAllTags()
    {
        Tags!.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInTag(Guid tagId)
    {
        return Tags!.Any(x => x.TagId == tagId);
    }

    #endregion
}