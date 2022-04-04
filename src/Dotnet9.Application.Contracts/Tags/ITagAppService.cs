using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Application.Contracts.Tags;

public interface ITagAppService
{
    Task<List<TagCountDto>> GetListCountAsync();
    Task<List<BlogPostWithDetailsDto>?> GetBlogPostListAsync(string tagName);
}