namespace Dotnet9.Contracts.Dto;

public record PaginationModel(List<BlogBrief>? Blogs, string UrlPrefix, int Current, int[]? Pages, long Total,
    int PageSize, int PageCount);