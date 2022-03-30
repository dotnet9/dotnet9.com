using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Application.Contracts.Categories;

public interface ICategoryAppService
{
    Task<CategoryDto?> GetCategoryAsync(string slug);

    Task<List<CategoryCountDto>> GetListCountAsync();

    Task<List<CategoryCountDto>> ListAllAsync();

    Task<List<BlogPostWithDetailsDto>?> GetBlogPostListAsync(string categorySlug);
}