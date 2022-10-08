namespace Dotnet9.Web.Service.BlogPosts;

public interface IBlogPostService
{
    Task<GetBlogPostBriefListResponse> GetBlogPostBriefListAsync(GetBlogPostBriefListRequest request);
    Task<GetBlogPostBriefListByCategorySlugResponse> GetBlogPostBriefListByCategorySlugAsync(GetBlogPostBriefListByCategorySlugRequest request);
    Task<BlogPostDetails?> GetBlogPostDetailsBySlugAsync(string slug);
    Task<bool> IncreaseViewCountAsync(string slug);
}