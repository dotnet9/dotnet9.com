using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Dotnet9.Albums;
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
        CopyrightType blogCopyrightType,
        string original,
        string originalTitle,
        string originalLink
    ) : base(id)
    {
        SetTitle(title);
        SetSlug(slug);
        ShortDescription = shortDescription;
        SetContent(content);
        CoverImageUrl = coverImageUrl;
        BlogCopyrightTypeEnum = blogCopyrightType;
        Original = original;
        OriginalTitle = originalTitle;
        OriginalLink = originalLink;
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
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), BlogPostConsts.MaxSlugLength);
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

    // Used for EF Core
    public int BlogCopyrightType { get; set; }

    // Used for code
    [NotMapped]
    public CopyrightType? BlogCopyrightTypeEnum
    {
        get => (CopyrightType?)Enum.Parse(typeof(CopyrightType), BlogCopyrightType.ToString());
        set
        {
            if (value.HasValue)
            {
                BlogCopyrightType = (int)value.Value;
            }
            else
            {
                BlogCopyrightType = (int)CopyrightType.Default;
            }
        }
    }

    public string Original { get; set; }

    public string OriginalTitle { get; set; }

    public string OriginalLink { get; set; }
}