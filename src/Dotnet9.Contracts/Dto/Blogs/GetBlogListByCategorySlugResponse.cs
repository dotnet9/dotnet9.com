namespace Dotnet9.Contracts.Dto.Blogs;

public record GetBlogListByCategorySlugResponse(bool Success, string? CategoryName = null,
    List<BlogBrief>? Records = null,
    long Total = 0,
    int TotalPage = 0,
    int PageSize = 20,
    int Page = 1);