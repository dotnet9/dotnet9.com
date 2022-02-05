namespace Dotnet9.Models.ViewModels.BlogPosts;

public class UpdateBlogPostInputDto
{
    public string? Title { get; set; }

    public string? Slug { get; set; }

    public CopyrightType CopyrightType { get; set; }

    public string? CoverImageUrl { get; set; }

    public string? Original { get; set; }

    public string? OriginalTitle { get; set; }

    public string? OriginalLink { get; set; }

    public string? Categories { get; set; }

    public string? Albums { get; set; }

    public string? Tags { get; set; }

    public string? Content { get; set; }

    public string? Remark { get; set; }

    public bool? IsDeleted { get; set; }
}