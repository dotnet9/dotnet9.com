namespace Dotnet9.Web.Service.BlogPosts;

public interface IBlogPostService
{
    Task<List<BlogPostBriefForFront>?> GetTop10NewBlogPostBriefListAsync();
    Task<GetBlogPostBriefListResponse> GetBlogPostBriefListAsync(GetBlogPostBriefListRequest request);

    Task<GetBlogPostBriefListByCategorySlugResponse> GetBlogPostBriefListByCategorySlugAsync(
        GetBlogPostBriefListByCategorySlugRequest request);

    Task<GetBlogPostBriefListByAlbumSlugResponse> GetBlogPostBriefListByAlbumSlugAsync(
        GetBlogPostBriefListByAlbumSlugRequest request);

    Task<GetBlogPostBriefListByTagNameResponse> GetBlogPostBriefListByTagNameAsync(
        GetBlogPostBriefListByTagNameRequest request);

    Task<BlogPostDetails?> GetBlogPostDetailsBySlugAsync(string slug);

    Task<List<BlogPostArchiveItem>?> GetArchivesAsync();
}