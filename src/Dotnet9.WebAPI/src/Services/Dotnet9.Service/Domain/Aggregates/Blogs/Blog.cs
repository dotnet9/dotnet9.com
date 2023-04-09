namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class Blog : FullAggregateRoot<Guid, int>
{
    private Blog()
    {
    }

    internal Blog(
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
        bool banner,
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
        ChangeBanner(banner);
        ChangeVisible(visible);

        Albums = new List<BlogAlbum>();
        Categories = new List<BlogCategory>();
        Tags = new List<BlogTag>();
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
    public bool Banner { get; private set; }
    public bool Visible { get; private set; }
    public int ViewCount { get; private set; }
    public int LikeCount { get; private set; }

    public List<BlogAlbum>? Albums { get; }
    public List<BlogCategory>? Categories { get; }
    public List<BlogTag>? Tags { get; }


    internal Blog ChangeTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), BlogConsts.MaxTitleLength,
            BlogConsts.MinTitleLength);
        return this;
    }

    internal Blog ChangeSlug(string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), BlogConsts.MaxSlugLength,
            BlogConsts.MinSlugLength);
        return this;
    }

    internal Blog ChangeDescription(string description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(description), BlogConsts.MaxDescriptionLength,
            BlogConsts.MinDescriptionLength);
        return this;
    }

    internal Blog ChangeCover(string cover)
    {
        Cover = Check.NotNullOrWhiteSpace(cover, nameof(cover), BlogConsts.MaxCoverLength,
            BlogConsts.MinCoverLength);
        return this;
    }

    internal Blog ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), BlogConsts.MaxContentLength,
            BlogConsts.MinContentLength);
        return this;
    }

    internal Blog ChangeCopyrightType(CopyRightType type)
    {
        CopyrightType = type;
        return this;
    }

    internal Blog ChangeOriginal(string? original)
    {
        Original = Check.Length(original, nameof(original), BlogConsts.MaxOriginalLength);
        return this;
    }

    internal Blog ChangeOriginalAvatar(string? originalAvatar)
    {
        OriginalAvatar = Check.Length(originalAvatar, nameof(originalAvatar),
            BlogConsts.MaxOriginalAvatarLength);
        return this;
    }

    internal Blog ChangeOriginalTitle(string? originalTitle)
    {
        OriginalTitle = Check.Length(originalTitle, nameof(originalTitle),
            BlogConsts.MaxOriginalTitleLength);
        return this;
    }

    internal Blog ChangeOriginalLink(string? originalLink)
    {
        OriginalLink = Check.Length(originalLink, nameof(originalLink),
            BlogConsts.MaxOriginalLinkLength);
        return this;
    }

    internal Blog ChangeBanner(bool banner)
    {
        Banner = banner;
        return this;
    }

    internal Blog ChangeVisible(bool visible)
    {
        Visible = visible;
        return this;
    }

    public Blog IncreaseViewCount()
    {
        ViewCount++;
        return this;
    }

    public Blog IncreaseLikeCount()
    {
        LikeCount++;
        return this;
    }

    internal Blog SetCreationTime(DateTime creationTime)
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

        Albums!.Add(new BlogAlbum(Id, albumId));
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
        Albums!.RemoveAll(x => x.BlogId == Id);
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

        Categories!.Add(new BlogCategory(Id, categoryId));
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
        Categories!.RemoveAll(x => x.BlogId == Id);
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

        Tags!.Add(new BlogTag(Id, tagId));
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
        Tags!.RemoveAll(x => x.BlogId == Id);
    }

    private bool IsInTag(Guid tagId)
    {
        return Tags!.Any(x => x.TagId == tagId);
    }

    #endregion
}
