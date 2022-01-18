using System;
using JetBrains.Annotations;
using Volo.Abp.Auditing;

namespace Dotnet9.Blogs;

public class BlogPostWithDetails : IHasCreationTime
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public string CoverImageUrl { get; set; }

    public CopyrightType CopyrightType { get; set; }

    public string Original { get; set; }

    public string OriginalTitle { get; set; }

    public string OriginalLink { get; set; }

    [CanBeNull] public string[] AlbumNames { get; set; }

    [CanBeNull] public string[] CategoryNames { get; set; }

    [CanBeNull] public string[] TagNames { get; set; }

    public DateTime CreationTime { get; set; }
}