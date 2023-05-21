namespace Dotnet9.RazorPages.Models;

public record PaginationModel(List<BlogBrief>? Blogs, string UrlPrefix, int Current, int[]? Pages, long Total,
    int PageSize, int PageCount);