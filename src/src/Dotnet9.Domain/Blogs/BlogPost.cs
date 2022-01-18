using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Blogs;

public class BlogPost : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public string Title { get; set; }

    [NotNull] public string Slug { get; set; }

    public string ShortDescription { get; set; }

    [NotNull] public string Content { get; set; }

    public string CoverImageUrl { get; set; }

    public CopyrightType CopyrightType { get; set; }

    public string Original { get; set; }

    public string OriginalTitle { get; set; }

    public string OriginalLink { get; set; }

    public ICollection<BlogPostCategory> Categories { get; private set; }

    public ICollection<BlogPostAlbum> Albums { get; private set; }

    public ICollection<BlogPostTag> Tags { get; private set; }

    private BlogPost()
    {
    }

    internal BlogPost(
        Guid id,
        [NotNull] string title,
        [NotNull] string slug,
        string shortDescription,
        [NotNull] string content,
        string coverImageUrl,
        CopyrightType copyrightType,
        string original,
        string originalTitle,
        string originalLink,
        DateTime creationTime
    ) : base(id)
    {
        SetTitle(title);
        SetSlug(slug);
        ShortDescription = shortDescription;
        SetContent(content);
        CoverImageUrl = coverImageUrl;
        CopyrightType = copyrightType;
        Original = original;
        OriginalTitle = originalTitle;
        OriginalLink = originalLink;
        CreationTime = creationTime;

        Albums = new List<BlogPostAlbum>();
        Categories = new Collection<BlogPostCategory>();
        Tags = new List<BlogPostTag>();
    }

    internal BlogPost ChangeTitle([NotNull] string title)
    {
        SetTitle(title);
        return this;
    }

    private void SetTitle([NotNull] string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), BlogPostConsts.MaxTitleLength);
    }


    internal BlogPost ChangeSlug([NotNull] string slug)
    {
        SetSlug(slug);
        return this;
    }

    private void SetSlug([NotNull] string slug)
    {
        Check.NotNullOrWhiteSpace(slug, nameof(slug), BlogPostConsts.MaxSlugLength, BlogPostConsts.MinSlugLength);

        Slug = SlugNormalizer.Normalize(slug);
    }

    private void SetContent([NotNull] string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), BlogPostConsts.MaxContentLength);
    }

    #region album

    public void AddAlbum(Guid albumId)
    {
        Check.NotNull(albumId, nameof(albumId));

        if (IsInAlbum(albumId))
        {
            return;
        }

        Albums.Add(new BlogPostAlbum(blogPostId: Id, albumId));
    }

    public void RemoveAlbum(Guid albumId)
    {
        Check.NotNull(albumId, nameof(albumId));

        if (!IsInAlbum(albumId))
        {
            return;
        }

        Albums.RemoveAll(x => x.AlbumId == albumId);
    }

    public void RemoveAllAlbumsExceptGivenIds(List<Guid> albumIds)
    {
        Check.NotNullOrEmpty(albumIds, nameof(albumIds));

        Albums.RemoveAll(x => !albumIds.Contains(x.AlbumId));
    }

    public void RemoveAllAlbums()
    {
        Albums.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInAlbum(Guid categoryId)
    {
        return Albums.Any(x => x.AlbumId == categoryId);
    }

    #endregion algum

    #region category

    public void AddCategory(Guid categoryId)
    {
        Check.NotNull(categoryId, nameof(categoryId));

        if (IsInCategory(categoryId))
        {
            return;
        }

        Categories.Add(new BlogPostCategory(blogPostId: Id, categoryId));
        Console.WriteLine($"{Id}==={categoryId}");
    }

    public void RemoveCategory(Guid categoryId)
    {
        Check.NotNull(categoryId, nameof(categoryId));

        if (!IsInCategory(categoryId))
        {
            return;
        }

        Categories.RemoveAll(x => x.CategoryId == categoryId);
    }

    public void RemoveAllCategoriesExceptGivenIds(List<Guid> categoryIds)
    {
        Check.NotNullOrEmpty(categoryIds, nameof(categoryIds));

        Categories.RemoveAll(x => !categoryIds.Contains(x.CategoryId));
    }

    public void RemoveAllCategories()
    {
        Categories.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInCategory(Guid categoryId)
    {
        return Categories.Any(x => x.CategoryId == categoryId);
    }

    #endregion

    #region tag

    public void AddTag(Guid tagId)
    {
        Check.NotNull(tagId, nameof(tagId));

        if (IsInTag(tagId))
        {
            return;
        }

        Tags.Add(new BlogPostTag(blogPostId: Id, tagId));
    }

    public void RemoveTag(Guid tagId)
    {
        Check.NotNull(tagId, nameof(tagId));

        if (!IsInTag(tagId))
        {
            return;
        }

        Tags.RemoveAll(x => x.TagId == tagId);
    }

    public void RemoveAllTagsExceptGivenIds(List<Guid> tagIds)
    {
        Check.NotNullOrEmpty(tagIds, nameof(tagIds));

        Tags.RemoveAll(x => !tagIds.Contains(x.TagId));
    }

    public void RemoveAllTags()
    {
        Tags.RemoveAll(x => x.BlogPostId == Id);
    }

    private bool IsInTag(Guid tagId)
    {
        return Tags.Any(x => x.TagId == tagId);
    }

    #endregion
}