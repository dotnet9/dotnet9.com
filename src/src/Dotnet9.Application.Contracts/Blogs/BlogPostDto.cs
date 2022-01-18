using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Blogs;

public class BlogPostDto : EntityDto<Guid>
{
    public string Title { get; set; }

    public string Slug { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public string CoverImageUrl { get; set; }

    public CopyrightType CopyrightType { get; set; }

    public string Original { get; set; }

    public string OriginalTitle { get; set; }

    public string OriginalLink { get; set; }

    public string[] AlbumNames { get; set; }

    public string[] CategoryNames { get; set; }

    public string[] TagNames { get; set; }

    public DateTime CreationTime { get; set; }
}