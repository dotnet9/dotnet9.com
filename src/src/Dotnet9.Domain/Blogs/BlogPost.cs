using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Blogs;

public class BlogPost : FullAuditedAggregateRoot<Guid>
{
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


    [NotNull] public string Title { get; set; }

    [NotNull] public string Slug { get; set; }

    public string ShortDescription { get; set; }

    [NotNull] public string Content { get; set; }

    public string CoverImageUrl { get; set; }

    public CopyrightType CopyrightType { get; set; }

    public string Original { get; set; }

    public string OriginalTitle { get; set; }

    public string OriginalLink { get; set; }
}