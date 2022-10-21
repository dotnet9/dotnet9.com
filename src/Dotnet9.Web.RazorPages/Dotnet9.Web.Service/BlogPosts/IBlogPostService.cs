namespace Dotnet9.Web.Service.BlogPosts;

public interface IBlogPostService
{
    Task<List<BlogPostBriefForFront>?> BlogPostBriefListByBanner();
    Task<List<BlogPostBriefForFront>?> TopNewBlogPostBriefListAsync(int count);
    Task<GetBlogPostBriefListResponse> BlogPostBriefListAsync(GetBlogPostBriefListRequest request);

    Task<GetBlogPostBriefListByCategorySlugResponse> BlogPostBriefListByCategorySlugAsync(
        BlogPostBriefListByCategorySlugRequest request);

    Task<GetBlogPostBriefListByAlbumSlugResponse> BlogPostBriefListByAlbumSlugAsync(
        GetBlogPostBriefListByAlbumSlugRequest request);

    Task<GetBlogPostBriefListByTagNameResponse> BlogPostBriefListByTagNameAsync(
        GetBlogPostBriefListByTagNameRequest request);

    Task<BlogPostDetails?> BlogPostDetailsBySlugAsync(string slug);

    Task<List<BlogPostArchiveItem>?> ArchivesAsync();
}