using Dotnet9.Core;
using Dotnet9.Domain.Shared.Blogs;

namespace Dotnet9.Domain.Blogs;

public class BlogPost : EntityBase
{
    private BlogPost()
    {
    }

    internal BlogPost(int id,
        string title,
        string slug,
        string? briefDescription,
        bool inBanner,
        string cover,
        string content,
        CopyrightType copyrightType,
        string? original,
        string? originalAvatar,
        string? originalTitle,
        string? originalLink,
        DateTimeOffset createDate) : base(id)
    {
        SetTitle(title);
        SetSlug(slug);
        BriefDescription = briefDescription;
        InBanner = inBanner;
        Cover = cover;
        SetContent(content);
        CopyrightType = copyrightType;
        Original = original;
        OriginalAvatar = originalAvatar;
        OriginalTitle = originalTitle;
        OriginalLink = originalLink;
        CreateDate = createDate;

        Albums = new List<BlogPostAlbum>();
        Categories = new List<BlogPostCategory>();
        Tags = new List<BlogPostTag>();
    }

    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? BriefDescription { get; set; }
    public bool InBanner { get; set; }
    public string Cover { get; set; } = null!;
    public string Content { get; set; } = null!;
    public CopyrightType CopyrightType { get; set; }
    public string? Original { get; set; }
    public string? OriginalAvatar { get; set; }
    public string? OriginalTitle { get; set; }
    public string? OriginalLink { get; set; }

    public List<BlogPostAlbum>? Albums { get; set; }
    public List<BlogPostCategory>? Categories { get; set; }
    public List<BlogPostTag>? Tags { get; set; }

    internal BlogPost ChangeTitle(string title)
    {
        SetTitle(title);
        return this;
    }

    private void SetTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), BlogPostConsts.MaxTitleLength);
    }

    internal BlogPost ChangeSlug(string slug)
    {
        SetSlug(slug);
        return this;
    }

    private void SetSlug(string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), BlogPostConsts.MaxSlugLength);
    }

    private void SetContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content));
    }

    #region album

    public void AddAlbum(int albumId)
    {
        Check.NotNull(albumId, nameof(albumId));

        if (IsInAlbum(albumId)) return;

        Albums!.Add(new BlogPostAlbum(Id, albumId));
    }

    public void RemoveAlbum(int albumId)
    {
        Check.NotNull(albumId, nameof(albumId));

        if (!IsInAlbum(albumId)) return;

        Albums!.RemoveAll(x => x.AlbumId == albumId);
    }

    public void RemoveAllAlbumsExceptGivenIds(List<int> albumIds)
    {
        Check.NotNullOrEmpty(albumIds, nameof(albumIds));

        Albums!.RemoveAll(x => !albumIds.Contains(x.AlbumId));
    }

    public void RemoveAllAlbums()
    {
        Albums!.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInAlbum(int categoryId)
    {
        return Albums!.Any(x => x.AlbumId == categoryId);
    }

    #endregion algum

    #region category

    public void AddCategory(int categoryId)
    {
        Check.NotNull(categoryId, nameof(categoryId));

        if (IsInCategory(categoryId)) return;

        Categories!.Add(new BlogPostCategory(Id, categoryId));
    }

    public void RemoveCategory(int categoryId)
    {
        Check.NotNull(categoryId, nameof(categoryId));

        if (!IsInCategory(categoryId)) return;

        Categories!.RemoveAll(x => x.CategoryId == categoryId);
    }

    public void RemoveAllCategoriesExceptGivenIds(List<int> categoryIds)
    {
        Check.NotNullOrEmpty(categoryIds, nameof(categoryIds));

        Categories!.RemoveAll(x => !categoryIds.Contains(x.CategoryId));
    }

    public void RemoveAllCategories()
    {
        Categories!.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInCategory(int categoryId)
    {
        return Categories!.Any(x => x.CategoryId == categoryId);
    }

    #endregion

    #region tag

    public void AddTag(int tagId)
    {
        Check.NotNull(tagId, nameof(tagId));

        if (IsInTag(tagId)) return;

        Tags!.Add(new BlogPostTag(Id, tagId));
    }

    public void RemoveTag(int tagId)
    {
        Check.NotNull(tagId, nameof(tagId));

        if (!IsInTag(tagId)) return;

        Tags!.RemoveAll(x => x.TagId == tagId);
    }

    public void RemoveAllTagsExceptGivenIds(List<int> tagIds)
    {
        Check.NotNullOrEmpty(tagIds, nameof(tagIds));

        Tags!.RemoveAll(x => !tagIds.Contains(x.TagId));
    }

    public void RemoveAllTags()
    {
        Tags!.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInTag(int tagId)
    {
        return Tags!.Any(x => x.TagId == tagId);
    }

    #endregion
}