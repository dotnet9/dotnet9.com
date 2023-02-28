namespace Dotnet9.WebAPI.ViewModels;

public class AboutSiteViewModel
{
    public AboutSiteViewModel(int blogPostCount, int commentCount, int albumCount, int categoryCount, int tagCount,
        int viewCount,
        SiteOptions details)
    {
        BlogPostCount = blogPostCount;
        CommentCount = commentCount;
        AlbumCount = albumCount;
        CategoryCount = categoryCount;
        TagCount = tagCount;
        ViewCount = viewCount;
        Details = details;
    }

    public int BlogPostCount { get; set; }
    public int CommentCount { get; set; }
    public int AlbumCount { get; set; }
    public int CategoryCount { get; set; }
    public int TagCount { get; set; }
    public int ViewCount { get; set; }
    public SiteOptions Details { get; set; }
}