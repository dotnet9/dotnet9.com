using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Blogs;

public class CreateBlogPostDto
{
    [Required]
    [StringLength(BlogPostConsts.MaxTitleLength)]
    public string Title { get; set; }

    [Required]
    [StringLength(BlogPostConsts.MaxSlugLength)]
    public string Slug { get; set; }

    [StringLength(BlogPostConsts.MaxShortDescriptionLength)]
    public string ShortDescription { get; set; }

    [Required]
    [StringLength(BlogPostConsts.MaxContentLength)]
    public string Content { get; set; }

    [StringLength(BlogPostConsts.MaxCoverImageUrlLength)]
    public string CoverImageUrl { get; set; }

    public CopyrightType CopyrightType { get; set; }

    [StringLength(BlogPostConsts.MaxOriginalLength)]
    public string Original { get; set; }

    [StringLength(BlogPostConsts.MaxOriginalTitleLength)]
    public string OriginalTitle { get; set; }

    [StringLength(BlogPostConsts.MaxOriginalLinkLength)]
    public string OriginalLink { get; set; }
}