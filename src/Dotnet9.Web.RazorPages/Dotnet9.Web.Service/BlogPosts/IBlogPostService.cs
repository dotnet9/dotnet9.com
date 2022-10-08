namespace Dotnet9.Web.Service.BlogPosts;

public interface IBlogPostService
{
    Task<GetBlogPostBriefListResponse> GetBlogPostBriefListAsync(GetBlogPostBriefListRequest request);
    Task<GetBlogPostBriefListByCategorySlugResponse> GetBlogPostBriefListByCategorySlugAsync(GetBlogPostBriefListByCategorySlugRequest request);
}