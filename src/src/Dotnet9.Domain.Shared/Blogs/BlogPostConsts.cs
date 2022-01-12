using System;

namespace Dotnet9.Blogs;

public static class BlogPostConsts
{
    public const int MaxTitleLength = 64;

    public const int MaxSlugLength = 128;

    public const int MaxShortDescriptionLength = 256;

    public const int MaxContentLength = int.MaxValue;

    public const int MaxCoverImageUrlLength = 128;


    public const int MaxOriginalLength = 128;


    public const int MaxOriginalTitleLength = 128;


    public const int MaxOriginalLinkLength = 128;
}

public class BlogSeedDto
{
    public string Title { get; set; }
    public string BriefDescription { get; set; }
    public string Cover { get; set; }
    public bool InBanner { get; set; }
    public string[] Albums { get; set; }
    public string[] Categories { get; set; }
    public string[] Tags { get; set; }
    public string CopyrightType { get; set; }
    public string Original { get; set; }
    public string OriginalLink { get; set; }
    public DateTime CreateDate { get; set; }
    public string Content { get; set; }
}